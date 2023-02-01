using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public int damageValue = 25;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<Player_Stats>() != null)
        {
            //damageSave.pointDamage = other.ClosestPoint(transform.position);
            other.transform.GetComponent<Player_Stats>().ChangeHealth(-damageValue);
        }
    }
}
