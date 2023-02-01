using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : SkillAttack
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void ActiveSkill()
    {
        base.ActiveSkill();
        Player_Skill playerSkill = GameManager.Instance.playerSkill;
        playerSkill.PlayerMove(playerSkill.cameraMain.transform.forward, 100);
        Debug.Log("SKILL ATTACK: " + itemName);
        //GameManager.Instance.playerWeapon.actualDamage = damageCalculated;
    }
}
