using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Test : Entity, Idamage
{
    public void SetDamage(Damage damageCalculated)
    {
        SetHealthDamage(damageCalculated);
        Debug.Log("DA�O RECIBIDO: " + damageCalculated.damageValue);

    }
}
