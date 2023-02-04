using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectile : MonoBehaviour
{
    public Transform target;
    public Rigidbody rb;
    public GameObject damageArea;
    public Damage actualDamage;
    public GameObject particleExplode;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 2000 * Time.fixedDeltaTime, ForceMode.Impulse);
    }

    public void Explode()
    {
        GameObject damageA = Instantiate(damageArea, transform.position, transform.rotation) as GameObject;
        if (damageA.GetComponent<DamageArea>() != null)
        {
            damageA.GetComponent<DamageArea>().damageRange = 2;
            damageA.GetComponent<DamageArea>().damageElement = actualDamage.damageElement;
            damageA.GetComponent<DamageArea>().damageSave = actualDamage;
        }

        if (damageA.GetComponent<DamagePlayer>() != null)
        {
            damageA.GetComponent<DamagePlayer>().damageValue = 50;
        }

        GameObject particle = Instantiate(particleExplode, transform.position, transform.rotation) as GameObject;
        Destroy(particle, 2);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collision)
    {
        Explode();
    }
}
