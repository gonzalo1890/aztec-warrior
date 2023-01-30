using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMelee : Weapon
{
    // Start is called before the first frame update
    void Start()
    {

    }
    protected override void Update()
    {
        base.Update();
    }

    public override void Shoot()
    {
        base.Shoot();
        
        GameManager.Instance.playerWeapon.actualDamage = damageCalculated;
    }
}
