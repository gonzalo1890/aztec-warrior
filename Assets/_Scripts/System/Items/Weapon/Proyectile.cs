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
        rb.AddForce(transform.forward * 10000 * Time.deltaTime, ForceMode.Impulse);
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            // Replaces your look at code
            transform.LookAt(target);

            // Get the actual scalar velocity
            var scalarVelocity = rb.velocity.magnitude;
            // Change rigidbody velocity to match the direction and magnitude
            rb.velocity = transform.forward * scalarVelocity;
        }else
        {
            var scalarVelocity = rb.velocity.magnitude;
            // Change rigidbody velocity to match the direction and magnitude
            rb.velocity = transform.forward * scalarVelocity;
        }
    }

    public void Explode()
    {
        GameObject damageA = Instantiate(damageArea, transform.position, transform.rotation) as GameObject;
        damageA.GetComponent<DamageArea>().damageRange = 2;
        damageA.GetComponent<DamageArea>().damageElement = actualDamage.damageElement;
        damageA.GetComponent<DamageArea>().damageSave = actualDamage;

        GameObject particle = Instantiate(particleExplode, transform.position, transform.rotation) as GameObject;
        Destroy(particle, 2);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collision)
    {
        Explode();
    }
}
