using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : Item
{
    public int skillIndex;
    public bool isEquiped = false;

    public bool isPrepared = false;

    public float cadence = 1f;
    public float cadenceBase = 1f;
    public float nextCheck;

    public string description;

    protected virtual void Update()
    {
        if (GameManager.Instance.menuView && !GameManager.Instance.pauseOn)
        {
            return;
        }

        if (Time.time > nextCheck)
        {
            isPrepared = true;
        }
    }
    public override void LevelApply()
    {
        base.LevelApply();
        cadence = cadenceBase;
        if (itemLevel != ItemLevel.Common)
        {
            for (int i = 0; i < (int)itemLevel; i++)
            {
                cadence = cadence * 0.6f;
            }
            cadence = Mathf.Round(cadence * 100.0f) * 0.01f;
        }
    }

    public virtual void UpgradeLevel()
    {
        int actualLevel = (int)itemLevel;
        if (actualLevel < 4)
        {
            actualLevel += 1;
            itemLevel = (ItemLevel)actualLevel;
        }
        LevelApply();
        //GameManager.Instance.canvasManager.WeaponEquiped(this);
    }

    public virtual void ActiveSkill()
    {
        
        isPrepared = false;
        nextCheck = Time.time + cadence;
        //damageCalculated = CalculeDamage();

        //GameManager.Instance.playerWeapon.GetAnimatorWeapon().SetTrigger("Shoot");


    }
}
