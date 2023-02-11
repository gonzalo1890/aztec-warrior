using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : SkillExtra
{

    public bool isJump = false;

    public float forceJump = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected override void Update()
    {
        base.Update();

        if(GameManager.Instance.playerStats.Grounded)
        {
            isJump = false;
        }


        if (Input.GetButtonDown("Jump") && isEquiped && !GameManager.Instance.playerStats.Grounded)
        {
            Debug.Log("ENTRA EN APRETAR JUMP");
            if (!isJump)
            {
                ActiveSkill();
            }
        }
    }

    public override void LevelApply()
    {
        base.LevelApply();
        if (itemLevel != ItemLevel.Common)
        {
            for (int i = 0; i < (int)itemLevel; i++)
            {
                forceJump = forceJump + 1;
            }
        }
    }

    public override void ActiveSkill()
    {
        base.ActiveSkill();
        Debug.Log("SKILL EXTRA: " + itemName);

        Player_Skill playerSkill = GameManager.Instance.playerSkill;
        playerSkill.PlayerJump(forceJump);

        isJump = true;
        //GameManager.Instance.playerWeapon.actualDamage = damageCalculated;
    }
}
