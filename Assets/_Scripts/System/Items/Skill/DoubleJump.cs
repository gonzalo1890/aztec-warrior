using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : SkillExtra
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void ActiveSkill()
    {
        base.ActiveSkill();
        Debug.Log("SKILL EXTRA: " + itemName);
        //GameManager.Instance.playerWeapon.actualDamage = damageCalculated;
    }
}
