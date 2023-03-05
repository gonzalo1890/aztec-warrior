using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageElement { None, Fire, Ice, Electricity, Poison }
public enum AmmoType { None, Bullet, Shell, Misil, Granade }
public abstract class Weapon : Item
{
    //Con esto elegimos el modelo3d del arma alojado en la camara
    public int weaponIndex;

    public bool isEquiped = false;

    //Daño base del arma
    public int damage = 10;
    private int damageBase;

    public AmmoType ammoType = AmmoType.None;
    //Rango de aleatoreidad de daño que puede hacer un arma a partir de su daño base
    public int RandomRange = 5;


    //Cadencia del arma (en segundos)
    public float cadence = 1f;
    private float cadenceBase;
    float nextCheck;

    //Probabilidad de daño critico
    [SerializeField]
    public float criticalHitProbability = 3f;
    private float criticalHitProbabilityBase;
    //Rango de aleatoreidad de daño c que puede hacer un arma a partir de su daño base
    public int RandomRangeCritic = 5;

    //Tipo de daño elemental
    [SerializeField]
    public DamageElement damageElement = DamageElement.None;

    //Probabilidad de daño elemental
    [SerializeField]
    private float elementHitProbability = 30f;

    //Rango de aleatoreidad de daño elemental que puede hacer un arma a partir de su daño base
    public int RandomRangeElemental = 5;

    public Damage damageCalculated;

    public GameObject impact;
    private void Awake()
    {
        damageBase = damage;
        cadenceBase = cadence;
        criticalHitProbabilityBase = criticalHitProbability;
    }
    protected virtual void Update()
    {
        if (GameManager.Instance.menuView && !GameManager.Instance.pauseOn)
        {
            return;
        }

        if (Input.GetAxisRaw("Fire1") != 0)
        {
            if (isEquiped)
            {

                if (Time.time > nextCheck)
                {
                    if (GameManager.Instance.playerStats.GetActualAmmo() > 0)
                    {
                        Shoot();
                    }
                    else
                    {
                        GameManager.Instance.playerWeapon.WeaponNoAmmo();
                    }
                    nextCheck = Time.time + cadence;
                }

            }
        }
    }

    public virtual void Shoot()
    {
        damageCalculated = CalculeDamage();
        GameManager.Instance.playerStats.SetActualAmmo(-1);
        GameManager.Instance.playerWeapon.SetAnimatorTrigger("Shoot");
        GameManager.Instance.playerWeapon.WeaponSound();
        GameManager.Instance.playerWeapon.StartParticleShoot();


    }

    public override void LevelApply()
    {
        base.LevelApply();

        damage = damageBase;
        cadence = cadenceBase;
        criticalHitProbability = criticalHitProbabilityBase;

        if (itemLevel != ItemLevel.Common)
        {
            damage = damage * (int)itemLevel;
            for (int i = 0; i < (int)itemLevel; i++)
            {
                float aux = (10 * cadence) / 100;
                cadence = cadence - aux;
            }
            cadence = Mathf.Round(cadence * 100.0f) * 0.01f;
            criticalHitProbability = criticalHitProbability * (int)itemLevel;
        }

        damage = damage + GameManager.Instance.playerStats.brutality;

    }

    public void UpgradeLevel(int index)
    {
        int actualLevel = (int)itemLevel;
        if(actualLevel < 4)
        {
            actualLevel += 1;
            itemLevel = (ItemLevel)actualLevel;
        }
        LevelApply();
        GameManager.Instance.canvasManager.WeaponEquiped(this, index);
    }

    public Damage CalculeDamage()
    {
        int value = Random.Range(damage - RandomRange, damage + RandomRange);
        DamageElement sendElement = DamageElement.None;
        bool sendIsCritic = false;

        if (CalculateElementDamage())
        {
            sendElement = damageElement;
            int valueElementDamage = Random.Range(RandomRangeElemental, RandomRangeElemental * 2);
            value = value + valueElementDamage;
        }

        if (CalculateCritic())
        {
            sendIsCritic = true;
            int criticDamage = Random.Range(RandomRangeCritic, RandomRangeCritic + RandomRangeCritic * 2);
            value = value + criticDamage;
        }


        Damage damageObject = new Damage();
        damageObject.damageValue = value;
        damageObject.damageElement = sendElement;
        damageObject.isCritic = sendIsCritic;
        return damageObject;
    }


    bool CalculateCritic()
    {
        bool result = false;

        int randomCritic = Random.Range(0, 100);

        if (randomCritic < criticalHitProbability)
        {
            result = true;
        }
        return result;
    }

    bool CalculateElementDamage()
    {
        bool result = false;

        int randomCritic = Random.Range(0, 100);

        if (randomCritic < elementHitProbability)
        {
            result = true;
        }
        return result;
    }



}
