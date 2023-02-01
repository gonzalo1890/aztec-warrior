using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : Item
{
    public int skillIndex;
    public bool isEquiped = false;

    public bool isPrepared = false;

    public float cadence = 1f;
    public float nextCheck;

    void Start()
    {
        
    }


    protected virtual void Update()
    {
        if (Time.time > nextCheck)
        {
            isPrepared = true;
            nextCheck = Time.time + cadence;
        }
    }

    public virtual void ActiveSkill()
    {
        isPrepared = false;
        //damageCalculated = CalculeDamage();

        //GameManager.Instance.playerWeapon.GetAnimatorWeapon().SetTrigger("Shoot");


    }
}
