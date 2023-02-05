using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Stats : MonoBehaviour
{
    public int health = 1000;
    public int maxHealth = 1000;

    private bool isDead = false;
    //Lineage
    public int spirit = 0;

    public int bloodlust = 0;
    public int rage = 0;
    public int agony = 0;
    public int brutality = 0;

    //Weapons
    public int bullet = 0;
    public int shell = 0;
    public int misil = 0;
    public int granade = 0;


    public void InitStats()
    {
        ChangeMaxHealth(agony);
        Invoke(nameof(UpdateStats), 1);
    }


    public void UpdateStats()
    {
        GameManager.Instance.canvasManager.UpdateStats(0, health);
        GameManager.Instance.canvasManager.UpdateStats(3, spirit);
    }

    public void ConsumableProcess(consumableType consumableType, int value)
    {
        if(consumableType == consumableType.Health)
        {
            ChangeHealth(value);
        }
        
        if (consumableType == consumableType.Spirit)
        {
            ChangeSpirit(value);
        }


        if (consumableType == consumableType.Bullet)
        {
            ChangeBullet(value);
        }
        if (consumableType == consumableType.Shell)
        {
            ChangeShell(value);
        }
        if (consumableType == consumableType.Misil)
        {
            ChangeMisil(value);
        }
        if (consumableType == consumableType.Granade)
        {
            ChangeGranade(value);
        }
    }

    public void ChangeHealth(int value)
    {
        if(isDead)
        {
            return;
        }
        if (value < 0)
        {
            health += (value + rage);
        }else
        {
            health += value;
        }

        if(health > maxHealth)
        {
            health = maxHealth;
        }
        if(health <= 0)
        {
            GameManager.Instance.roguelite.Death();
            isDead = true;
            health = 0;
        }
        GameManager.Instance.canvasManager.UpdateStats(0, health);
    }

    public void ChangeMaxHealth(int value)
    {
        maxHealth += value;
        health = maxHealth;
        GameManager.Instance.canvasManager.UpdateHealthMaxBar(health);
        GameManager.Instance.canvasManager.UpdateStats(0, health);
    }

    public void ChangeSpirit(int value)
    {
        spirit += value;
        if (spirit < 0)
        {
            spirit = 0;
        }
        GameManager.Instance.canvasManager.UpdateStats(1, spirit);
        GameManager.Instance.canvasManager.UpdateStats(3, spirit);
    }

    public void ChangeBullet(int value)
    {
        bullet += value;
        if (bullet < 0)
        {
            bullet = 0;
        }
        UpdateAmmo();
    }

    public void ChangeShell(int value)
    {
        shell += value;
        if (shell < 0)
        {
            shell = 0;
        }
        UpdateAmmo();
    }

    public void ChangeMisil(int value)
    {
        misil += value;
        if (misil < 0)
        {
            misil = 0;
        }
        UpdateAmmo();
    }

    public void ChangeGranade(int value)
    {
        granade += value;
        if (granade < 0)
        {
            granade = 0;
        }
        UpdateAmmo();
    }

    public void UpdateAmmo()
    {
        Weapon actualWeapon = GameManager.Instance.playerInventory.GetActualWeapon();

        int ammo = 0;

        if (actualWeapon.ammoType == AmmoType.None)
        {
            ammo = -1;
        }
        if (actualWeapon.ammoType == AmmoType.Bullet)
        {
            ammo = bullet;
        }
        if (actualWeapon.ammoType == AmmoType.Shell)
        {
            ammo = shell;
        }
        if (actualWeapon.ammoType == AmmoType.Misil)
        {
            ammo = misil;
        }
        if (actualWeapon.ammoType == AmmoType.Granade)
        {
            ammo = granade;
        }
        GameManager.Instance.canvasManager.UpdateAmmo(ammo);
    }

    public int GetActualAmmo()
    {
        Weapon actualWeapon = GameManager.Instance.playerInventory.GetActualWeapon();

        int ammo = 0;

        if (actualWeapon.ammoType == AmmoType.None)
        {
            ammo = 99999;
        }
        if (actualWeapon.ammoType == AmmoType.Bullet)
        {
            ammo = bullet;
        }
        if (actualWeapon.ammoType == AmmoType.Shell)
        {
            ammo = shell;
        }
        if (actualWeapon.ammoType == AmmoType.Misil)
        {
            ammo = misil;
        }
        if (actualWeapon.ammoType == AmmoType.Granade)
        {
            ammo = granade;
        }

        return ammo;
    }

    public void SetActualAmmo(int value)
    {
        Weapon actualWeapon = GameManager.Instance.playerInventory.GetActualWeapon();

        if (actualWeapon.ammoType == AmmoType.None)
        {
            
        }
        if (actualWeapon.ammoType == AmmoType.Bullet)
        {
            ChangeBullet(value);
        }
        if (actualWeapon.ammoType == AmmoType.Shell)
        {
            ChangeShell(value);
        }
        if (actualWeapon.ammoType == AmmoType.Misil)
        {
            ChangeMisil(value);
        }
        if (actualWeapon.ammoType == AmmoType.Granade)
        {
            ChangeGranade(value);
        }
    }

    public void ResetAmmo()
    {
        bullet = 0;
        shell = 0;
        misil = 0;
        granade = 0;

        GameManager.Instance.canvasManager.UpdateStats(2, bullet);
        GameManager.Instance.canvasManager.UpdateStats(3, shell);
        GameManager.Instance.canvasManager.UpdateStats(4, misil);
        GameManager.Instance.canvasManager.UpdateStats(5, granade);
    }


    public List<int> SaveStats()
    {
        List<int> stats = new List<int>();
        stats.Add(health);
        stats.Add(spirit);
        stats.Add(bloodlust);
        stats.Add(rage);
        stats.Add(agony);
        stats.Add(brutality);
        return stats;
    }

    public void LoadStats(List<int> stats)
    {
        health = stats[0];
        spirit = stats[1];
        bloodlust = stats[2];
        rage = stats[3];
        agony = stats[4];
        brutality = stats[5];
        UpdateStats();
    }

    public void BloodlustApply(int value)
    {
        int result = ((value + bloodlust) * 3) / 100;
        ChangeHealth(result);
    }
}
