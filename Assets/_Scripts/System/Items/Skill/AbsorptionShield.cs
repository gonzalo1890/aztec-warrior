using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsorptionShield : SkillExtra
{
    float absorption = 0.02f;
    float absorptionBase = 0.02f;
    public override void LevelApply()
    {
        base.LevelApply();
        absorption = absorptionBase;
        if (itemLevel != ItemLevel.Common)
        {
            for (int i = 0; i < (int)itemLevel; i++)
            {
                absorption = absorption + 0.02f;
            }
        }
        GameManager.Instance.playerStats.SetAbsorption(absorption);
    }
}
