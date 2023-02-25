using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeWeapon : Upgrade
{
    public override void UpgradeItem()
    {
        base.UpgradeItem();
        GameManager.Instance.playerInventory.UpgradeWeapon(upgradeName);
    }

}
