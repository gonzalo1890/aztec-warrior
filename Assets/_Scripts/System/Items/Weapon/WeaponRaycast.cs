using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRaycast : Weapon
{
    public LayerMask shootLayer;
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

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0f));
        if (Physics.Raycast(ray, out RaycastHit hit, 1000, shootLayer))
        {
            if (hit.transform != null)
            {
                Debug.Log(hit.transform.name);
            }


            if (hit.transform.GetComponent<Idamage>() != null)
            {
                damageCalculated.pointDamage = hit.point;
                GameObject particle = Instantiate(impact, hit.point, Quaternion.identity) as GameObject;
                particle.GetComponent<Impact>().damageElement = damageElement;
                particle.GetComponent<Impact>().CreateElementParticle(hit.point, hit.transform, true);
                particle.GetComponent<Impact>().CreateBloodParticle(hit.point, hit.transform, true);
                hit.transform.GetComponent<Idamage>().SetDamage(damageCalculated);
            }
        }
        else
        {
            Debug.Log("NO CHOCO EN NADA");
        }

        //Debug.Log(itemName);
    }
}
