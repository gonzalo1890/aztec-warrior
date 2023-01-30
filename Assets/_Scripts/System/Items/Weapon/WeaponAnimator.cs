using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimator : MonoBehaviour
{
    public void ShootEvent()
    {
        GameManager.Instance.playerWeapon.CreateDamageArea();
    }
}
