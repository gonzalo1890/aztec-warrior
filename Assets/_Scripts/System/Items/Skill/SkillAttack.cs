using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAttack : Skill
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (Input.GetButton("Fire2") && isEquiped)
        {
            if (isPrepared)
            {
                GameManager.Instance.canvasManager.ColdDownSkill(cadence, true);
                ActiveSkill();
            }
        }
    }

    public override void ActiveSkill()
    {
        base.ActiveSkill();
        //GameManager.Instance.playerWeapon.actualDamage = damageCalculated;
    }
}
