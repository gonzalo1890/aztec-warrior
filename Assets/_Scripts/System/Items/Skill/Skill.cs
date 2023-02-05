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

    public string description;
    void Start()
    {
        
    }


    protected virtual void Update()
    {
        if (GameManager.Instance.menuView)
        {
            return;
        }

        if (Time.time > nextCheck)
        {
            isPrepared = true;
            nextCheck = Time.time + cadence;
        }
    }
    public override void LevelApply()
    {
        base.LevelApply();
        if (itemLevel != ItemLevel.Common)
        {
            for (int i = 0; i < (int)itemLevel; i++)
            {
                cadence = cadence * 0.6f;
            }
            cadence = Mathf.Round(cadence * 100.0f) * 0.01f;
        }
    }
    public virtual void ActiveSkill()
    {
        
        isPrepared = false;
        //damageCalculated = CalculeDamage();

        //GameManager.Instance.playerWeapon.GetAnimatorWeapon().SetTrigger("Shoot");


    }
}
