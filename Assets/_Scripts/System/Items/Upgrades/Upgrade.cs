using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpgradeType { Weapon, SkillAttack, SkillExtra}
public class Upgrade : Item
{
    public UpgradeType upgradeType;
    public string upgradeName;
    public virtual void UpgradeItem()
    {
        switch (upgradeType)
        {
            case UpgradeType.Weapon:
                GameManager.Instance.playerInventory.UpgradeWeapon(upgradeName);
                break;
            case UpgradeType.SkillAttack:
                GameManager.Instance.playerInventory.UpgradeSkillAttack(upgradeName);
                break;
            case UpgradeType.SkillExtra:
                GameManager.Instance.playerInventory.UpgradeSkillExtra(upgradeName);
                break;
            default:
                break;
        }

        Destroy(gameObject, 0.2f);
    }
}
