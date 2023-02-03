using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAnimation : MonoBehaviour
{
    public EntityTarget entityTarget;

    public void AttackOn()
    {
        entityTarget.Attack();
    }
}
