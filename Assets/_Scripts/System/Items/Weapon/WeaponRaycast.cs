using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRaycast : Weapon
{
    public LayerMask shootLayer;

    public float precision = 0.01f;

    public int shootCount = 1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
    public override void Shoot()
    {
        base.Shoot();

        Transform ShootPoint = GameManager.Instance.playerWeapon.GetShootPoint();

        for (int i = 0; i < shootCount; i++)
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0f));
            ray.direction = ray.direction + new Vector3(Random.Range(-precision, precision), Random.Range(-precision, precision), Random.Range(-precision, precision));
            if (Physics.Raycast(ray, out RaycastHit hit, 1000, shootLayer))
            {



                if (hit.transform.GetComponent<Idamage>() != null)
                {
                    damageCalculated.pointDamage = hit.point;
                    GameObject particle = Instantiate(impact, hit.point, Quaternion.identity) as GameObject;
                    particle.GetComponent<Impact>().damageElement = damageElement;
                    particle.GetComponent<Impact>().CreateElementParticle(hit.point, hit.transform, true);
                    particle.GetComponent<Impact>().CreateBloodParticle(hit.point, hit.transform, true);
                    particle.GetComponent<Impact>().SetLine(GameManager.Instance.playerWeapon.GetShootPoint().position, hit.point, ShootPoint);
                    hit.transform.GetComponent<Idamage>().SetDamage(damageCalculated);
                    GameManager.Instance.playerStats.BloodlustApply(damageCalculated.damageValue);
                    return;
                }

                if (hit.transform != null)
                {
                    GameObject particle = Instantiate(impact, ShootPoint.position, Quaternion.identity) as GameObject;

                    particle.GetComponent<Impact>().SetLine(ShootPoint.position, ShootPoint.position + (ray.direction * 10), ShootPoint);
                }
            }
            else
            {
                GameObject particle = Instantiate(impact, ShootPoint.position, Quaternion.identity) as GameObject;

                particle.GetComponent<Impact>().SetLine(ShootPoint.position, ShootPoint.position + (ShootPoint.forward * 10), ShootPoint);
                Debug.Log("NO CHOCO EN NADA");
            }
        }



        //Debug.Log(itemName);
    }
}
