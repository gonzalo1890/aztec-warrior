using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour, Idamage
{
    public Entity entityState;
    public int damageMultiplier = 1;
    private Rigidbody rb;
    private Collider col;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    public void SetEntityState(Entity _entityState)
    {
        entityState = _entityState;
    }
    public void SetDamage(Damage damage)
    {
        if (entityState != null && entityState.health > 0)
        {
            int damageBody = damage.damageValue * damageMultiplier;
            damage.damageValue = damageBody;
            entityState.SetHealthDamage(damage);
        }
    }

    public void ActiveRagdoll()
    {
        rb.useGravity = true;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        col.isTrigger = false;
        col.enabled = true;
        rb.isKinematic = false;
    }
}

public interface Idamage
{
    public void SetDamage(Damage damage);
}
