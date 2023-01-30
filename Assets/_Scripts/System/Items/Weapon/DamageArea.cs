using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    public Damage damageSave;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 0.1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<Idamage>() != null)
        {
            damageSave.pointDamage = other.ClosestPoint(transform.position);
            other.transform.GetComponent<Idamage>().SetDamage(damageSave);
            Destroy(gameObject);
        }
    }
}
