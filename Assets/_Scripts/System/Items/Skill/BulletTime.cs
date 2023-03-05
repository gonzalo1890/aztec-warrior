using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTime : SkillAttack
{
    TimeManager timeManager;
    public AudioEffect SlowSoundEffect;
    public AudioEffect SlowSoundEnter;
    private float timeEffect = 0.04f;
    private float timeEffectBase;
    private void Awake()
    {
        timeEffectBase = timeEffect;
    }
    protected override void Update()
    {
        base.Update();
    }

    public override void LevelApply()
    {
        base.LevelApply();
        timeEffect = timeEffectBase;
        if (itemLevel != ItemLevel.Common)
        {
            for (int i = 0; i < (int)itemLevel; i++)
            {
                timeEffect = timeEffect + 0.04f;
            }
        }
    }

    public override void ActiveSkill()
    {
        base.ActiveSkill();
        SlowSound();
        timeManager = GameManager.Instance.timeManager;
        timeManager.DoSlowmotionTime(timeEffect);
        Debug.Log("SKILL ATTACK: " + itemName);

    }

    void SlowSound()
    {
        SlowSoundEnter.Stop();
        SlowSoundEnter.Play(0, "Slow time");
        SlowSoundEffect.Stop();
        SlowSoundEffect.Play(1, "Slow time");
        GameManager.Instance.roguelite.SlowTime(timeEffect);
        Invoke(nameof(EndSlowSound), timeEffect);
    }

    void EndSlowSound()
    {
        SlowSoundEffect.SetParameter(0, "Slow time");
        SlowSoundEffect.Play(0, "Slow time");
        SlowSoundEffect.Stop();
    }
}
