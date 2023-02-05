using RootMotion.FinalIK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EntityTargetState {Follow, MeleeAttack, DistanceAttack, CalculeLoop, OffAI}

public class EntityTarget : Entity
{
    [Header("Parameters")]
    [SerializeField] EntityTargetState entityState;
    [SerializeField] Transform target;
    public Transform headTarget;
    public Transform head;
    public LayerMask layer;
    //Rotation
    [SerializeField] float turnSpeed = 5f;
    //Loop
    [SerializeField] float loopRate = 1f;
    float nextCheck;

    [Header("Melee Attack")]
    [SerializeField] bool useMeleeAttack = false;
    [SerializeField] float distanceToMelee = 2f;
    [SerializeField] float cadenceMelee = 1f;
    float meleeNextCheck;

    [Header("Distance Attack")]
    [SerializeField] bool useDistanceAttack = false;
    [SerializeField] float distanceAttackRate = 20f;
    [SerializeField] float cadenceDistance = 1f;

    float distanceNextCheck;

    public GameObject attackArea;
    public Transform attackPosition;

    public GameObject proyectileShoot;
    public Transform shootPosition;
    Coroutine coroutineAIM;

    public Battle battle;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        if (target != null)
        {
            headTarget = target.GetComponent<TargetInfo>().targetHead;
            GetAgentDestination().NewTargetPath(target, DestinationType.Follow);
        }else
        {
            GetTarget();
        }
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (target != null)
        {
            switch (entityState)
            {
                case EntityTargetState.Follow:
                    FollowState();
                    break;
                case EntityTargetState.MeleeAttack:
                    MeleeAttackState();
                    break;
                case EntityTargetState.DistanceAttack:
                    DistanceAttackState();
                    break;
                case EntityTargetState.CalculeLoop:
                    CalculeLoopState();
                    break;
                default:
                    break;
            }
        }
    }

    public override void Death()
    {
        base.Death();
        battle.EnemyDeath();
    }

    void GetTarget()
    {
        target = GameManager.Instance.targetInfo;
        headTarget = target.GetComponent<TargetInfo>().targetHead;
        if (entityState != EntityTargetState.OffAI)
        {
            transform.GetChild(0).GetComponent<AimIK>().solver.target = headTarget;
            GetAgentDestination().NewTargetPath(target, DestinationType.Follow);
        }
    }

    float DistanceCheck()
    {
        return Vector3.Distance(transform.position, target.position);
    }

    EntityTargetState Priority()
    {
        EntityTargetState result = EntityTargetState.CalculeLoop;

        if (useMeleeAttack)
        {
            if (DistanceCheck() < distanceToMelee)
            {
                result = EntityTargetState.MeleeAttack;
                return result;
            }
        }

        float random = Random.Range(0f, 100f);

        if (useDistanceAttack)
        {
            if (random < distanceAttackRate)
            {
                result = EntityTargetState.DistanceAttack;
                return result;
            }
        }


        if (!useMeleeAttack)
        {
            result = EntityTargetState.CalculeLoop;
        }
        else
        {
            result = EntityTargetState.Follow;
        }
        return result;
    }

    //Persigue al target
    void FollowState()
    {
        GetAgentDestination().NewTargetPath(target, DestinationType.Follow);
        entityState = EntityTargetState.CalculeLoop;
    }

    //Ataca cuerpo a cuerpo
    void MeleeAttackState()
    {
        if (Time.time > meleeNextCheck)
        {
            //Attack Melee
            anim.SetTrigger("Melee");
            //Debug.Log("Melee Attack");
            meleeNextCheck = Time.time + cadenceMelee;
        }

        if (DistanceCheck() > distanceToMelee)
        {
            GetAgentDestination().NewTargetPath(target, DestinationType.Follow);
            entityState = EntityTargetState.CalculeLoop;
        }else
        {
            GetAgentDestination().NewTargetPath(this.transform, DestinationType.Still);
            FaceTarget();
        }
    }


    public void Attack()
    {
        GameObject damageA = Instantiate(attackArea, attackPosition.position, transform.rotation) as GameObject;
        Damage actualDamage = new Damage();
        actualDamage.damageValue = 5;
        actualDamage.damageElement = DamageElement.None;
        damageA.GetComponent<DamagePlayer>().damageValue = actualDamage.damageValue;
        //damageA.GetComponent<DamageArea>().damageElement = actualDamage.damageElement;
        Destroy(damageA, 0.1f);
    }

    //Ataca a distancia
    void DistanceAttackState()
    {
        //Debug.Log("ENTRO EN ATACK");
        RaycastHit hit;
        if (Physics.Linecast(head.position, headTarget.position, out hit, layer))
        {
            if (hit.transform.GetComponent<TargetInfo>() != null)
            {
                //Debug.Log("ENCONTRO TARGET");
                if (Time.time > distanceNextCheck)
                {
                    FaceTarget();
                    GetAgentDestination().NewTargetPath(this.transform, DestinationType.Still);
                    Debug.Log("Distance Attack");
                    if (coroutineAIM != null)
                    {
                        StopCoroutine(coroutineAIM);
                    }
                    coroutineAIM = StartCoroutine(AIMtime(0.5f));
                    entityState = EntityTargetState.CalculeLoop;
                    distanceNextCheck = Time.time + cadenceDistance;
                }
            }else
            {
                entityState = EntityTargetState.CalculeLoop;
            }
        }
    }
    Coroutine coroutine;
    //Calcula el siguiente movimiento
    void CalculeLoopState()
    {
        if(isDeath)
        {
            return;
        }

        if (Time.time > nextCheck)
        {
            entityState = Priority();
            nextCheck = Time.time + loopRate;
        }
    }

    private IEnumerator AIMtime(float waitTime)
    {
        float elapsedTime = 0;
        float start = aimIK.solver.GetIKPositionWeight();
        while (elapsedTime < waitTime)
        {

            aimIK.solver.SetIKPositionWeight(Mathf.Lerp(start, 1f, (elapsedTime / waitTime)));
            fullBody.solver.SetIKPositionWeight(Mathf.Lerp(start, 1f, (elapsedTime / waitTime)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(0.3f);
        GetRecoil().Fire(1f);
        GameObject shoot = Instantiate(proyectileShoot, shootPosition.position, transform.rotation) as GameObject;
        yield return new WaitForSeconds(0.3f);
        elapsedTime = 0;
        start = aimIK.solver.GetIKPositionWeight();
        while (elapsedTime < waitTime)
        {

            aimIK.solver.SetIKPositionWeight(Mathf.Lerp(start, 0f, (elapsedTime / waitTime)));
            fullBody.solver.SetIKPositionWeight(Mathf.Lerp(start, 0f, (elapsedTime / waitTime)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        coroutineAIM = null;
    }

    //Rotacion de la entidad hacia su objetivo
    private void FaceTarget()
    {
        //Debug.Log("FACETARGET");
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(RotateToLook(lookRotation, 1f));
        //transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
        //transform.rotation = lookRotation;
    }


    IEnumerator RotateToLook(Quaternion look, float duration)
    {
        float elapsedTime = 0;
        Quaternion startingRotation = transform.rotation;
        if(elapsedTime < duration) {
            transform.rotation = Quaternion.Lerp(startingRotation, look, elapsedTime / duration);
            elapsedTime = elapsedTime + Time.deltaTime; // <- move elapsedTime increment here
            //Debug.Log(elapsedTime);
            yield return null;
        }
        transform.rotation = look;
    }
}
