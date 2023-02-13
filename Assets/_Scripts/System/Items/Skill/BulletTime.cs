using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTime : SkillAttack
{
    TimeManager timeManager;

    private float timeEffect = 0.02f;
    protected override void Update()
    {
        base.Update();
    }

    public override void LevelApply()
    {
        base.LevelApply();
        if (itemLevel != ItemLevel.Common)
        {
            for (int i = 0; i < (int)itemLevel; i++)
            {
                timeEffect = timeEffect + 0.02f;
            }
        }
    }

    public override void ActiveSkill()
    {
        base.ActiveSkill();
        timeManager = GameManager.Instance.timeManager;
        timeManager.DoSlowmotionTime(timeEffect);
        Debug.Log("SKILL ATTACK: " + itemName);

    }

}
