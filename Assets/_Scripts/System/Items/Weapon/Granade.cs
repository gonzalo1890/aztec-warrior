using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granade : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject damageArea;
    public Damage actualDamage;
    public float ExplodeTime = 3f;
    public GameObject particleExplode;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 5000 * Time.deltaTime, ForceMode.Impulse);
        Invoke(nameof(Explode), ExplodeTime);
    }

    public void Explode()
    {
        GameObject damageA = Instantiate(damageArea, transform.position, transform.rotation) as GameObject;
        damageA.GetComponent<DamageArea>().damageRange = 3;
        damageA.GetComponent<DamageArea>().damageElement = actualDamage.damageElement;
        damageA.GetComponent<DamageArea>().damageSave = actualDamage;

        GameObject particle = Instantiate(particleExplode, transform.position, transform.rotation) as GameObject;
        Destroy(particle, 2);

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.transform.GetComponent<TakeDamage>() != null)
        {
            Explode();
        }
    }
}
