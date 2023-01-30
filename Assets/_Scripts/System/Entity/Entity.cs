
using RootMotion.FinalIK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Una entidad es algo que tiene nombre y una barra de vida por lo que puede ser destruido.
public abstract class Entity : MonoBehaviour
{
    [Header("Entity")]

    [SerializeField]
    private string nameID = "Entity";

    public int health = 100;
    public int healthMax = 100;

    public TakeDamage[] takeDamages;

    //3dModel Elements
    public GameObject model;
    private Animator anim;
    private AimIK aimIK;
    private FullBodyBipedIK fullBody;
    //Character Elements
    //private Rigidbody rb;
    private NavMeshAgent navMeshAgent;
    private Collider col;
    private Agent_Destination agent_Destination;
    private Recoil recoil;
    // Start is called before the first frame update
    public virtual void Start()
    {
        //rb = GetComponent<Rigidbody>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        col = GetComponent<Collider>();
        agent_Destination = GetComponent<Agent_Destination>();

        anim = model.GetComponent<Animator>();
        aimIK = model.GetComponent<AimIK>();
        fullBody = model.GetComponent<FullBodyBipedIK>();
        recoil = model.GetComponent<Recoil>();
        SetTakeDamageObjects();
    }

    // Update is called once per frame
    public virtual void Update()
    {

    }

    void SetTakeDamageObjects()
    {
        for (int i = 0; i < takeDamages.Length; i++)
        {
            takeDamages[i].SetEntityState(this);
        }
    }

    void ActiveRagdolls()
    {
        if (anim != null)
        {
            anim.enabled = false;
        }

        if (aimIK != null)
        {
            aimIK.enabled = false;
        }

        if (fullBody != null)
        {
            fullBody.enabled = false;
        }

        if (col != null)
        {
            col.enabled = false;
        }

        if (agent_Destination != null)
        {
            agent_Destination.enabled = false;
        }
        /*
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        */
        if (navMeshAgent != null)
        {
            //navMeshAgent.SetDestination(transform.position);
            navMeshAgent.enabled = false;
        }

        for (int i = 0; i < takeDamages.Length; i++)
        {
            takeDamages[i].ActiveRagdoll();
        }
    }
    public void SetHealthDamage(Damage damageCalculated)
    {
        health -= damageCalculated.damageValue;
        if (health > healthMax)
        {
            health = healthMax;
        }


        if (health < 1)
        {
            if (recoil != null)
            {
                //recoil.Fire(0.01f);
            }
            health = 0;
            ActiveRagdolls();
            Destroy(gameObject, 5);
        }else
        {
            if (recoil != null)
            {
                recoil.Fire(5);
            }
        }

        GameManager.Instance.canvasManager.SetDamageInfo(damageCalculated.damageValue, damageCalculated.damageElement, damageCalculated.isCritic, damageCalculated.pointDamage);
    }

    public Agent_Destination GetAgentDestination()
    {
        return agent_Destination;
    }

    public Recoil GetRecoil()
    {
        return recoil;
    }
}
