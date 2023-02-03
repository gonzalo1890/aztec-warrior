using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponProyectile : Weapon
{
    public GameObject proyectile;
    protected override void Update()
    {
        base.Update();
    }
    public override void Shoot()
    {
        base.Shoot();
        Transform shootPoint = GameManager.Instance.playerWeapon.GetShootPoint();
        GameObject shooting = Instantiate(proyectile, shootPoint.position, shootPoint.rotation) as GameObject;

        if(shooting.GetComponent<Granade>() != null)
        {
            shooting.GetComponent<Granade>().actualDamage = damageCalculated;
        }

        if (shooting.GetComponent<Proyectile>() != null)
        {
            shooting.GetComponent<Proyectile>().actualDamage = damageCalculated;
        }
        /*Ray ray = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0f));
        if (Physics.Raycast(ray, out RaycastHit hit, 1000, shootLayer))
        {
            if (hit.transform != null)
            {
                Debug.Log(hit.transform.name);
            }


            if (hit.transform.GetComponent<Idamage>() != null)
            {
                damageCalculated.pointDamage = hit.point;
                hit.transform.GetComponent<Idamage>().SetDamage(damageCalculated);
            }
        }
        else
        {
            Debug.Log("NO CHOCO EN NADA");
        }*/

        //Debug.Log(itemName);
    }
}
