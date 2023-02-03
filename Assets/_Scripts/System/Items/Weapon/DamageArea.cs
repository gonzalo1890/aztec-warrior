using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    public Damage damageSave;
    public float damageRange = 0.5f;

    public DamageElement damageElement;
    public GameObject impact;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SphereCollider>().radius = damageRange;
        Destroy(gameObject, 0.1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<Idamage>() != null)
        {
            Vector3 hitPoint = other.ClosestPoint(transform.position);
            damageSave.pointDamage = hitPoint;
            GameObject particle = Instantiate(impact, hitPoint, Quaternion.identity) as GameObject;
            particle.GetComponent<Impact>().damageElement = damageElement;
            particle.GetComponent<Impact>().CreateElementParticle(hitPoint, other.transform, true);
            particle.GetComponent<Impact>().CreateBloodParticle(hitPoint, other.transform, true);


            other.transform.GetComponent<Idamage>().SetDamage(damageSave);
            Destroy(gameObject);
        }
    }
}
