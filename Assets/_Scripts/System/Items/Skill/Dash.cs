using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : SkillAttack
{
    public AudioEffect DashSoundEffect;
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

        DashSoundEffect.Stop();
        DashSoundEffect.Play();

        Player_Skill playerSkill = GameManager.Instance.playerSkill;
        playerSkill.PlayerMove(50, 0.1f);
        Debug.Log("SKILL ATTACK: " + itemName);
        //GameManager.Instance.playerWeapon.actualDamage = damageCalculated;
    }
}
