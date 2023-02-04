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

    //Da�o base del arma
    public int damage = 10;

    public AmmoType ammoType = AmmoType.None;
    //Rango de aleatoreidad de da�o que puede hacer un arma a partir de su da�o base
    public int RandomRange = 5;


    //Cadencia del arma (en segundos)
    public float cadence = 1f;
    float nextCheck;

    //Probabilidad de da�o critico
    [SerializeField]
    public float criticalHitProbability = 3f;

    //Rango de aleatoreidad de da�o c que puede hacer un arma a partir de su da�o base
    public int RandomRangeCritic = 5;

    //Tipo de da�o elemental
    [SerializeField]
    public DamageElement damageElement = DamageElement.None;

    //Probabilidad de da�o elemental
    [SerializeField]
    private float elementHitProbability = 30f;

    //Rango de aleatoreidad de da�o elemental que puede hacer un arma a partir de su da�o base
    public int RandomRangeElemental = 5;

    public Damage damageCalculated;


    public GameObject impact;
    protected virtual void Update()
    {
        if (Input.GetButton("Fire1") && isEquiped && GameManager.Instance.playerStats.GetActualAmmo() > 0)
        {
            if (Time.time > nextCheck)
            {
                Shoot();
                nextCheck = Time.time + cadence;
            }
        }
    }

    public virtual void Shoot()
    {
        damageCalculated = CalculeDamage();
        GameManager.Instance.playerStats.SetActualAmmo(-1);
        GameManager.Instance.playerWeapon.GetAnimatorWeapon().SetTrigger("Shoot");
        GameManager.Instance.playerWeapon.StartParticleShoot();


    }

    public override void LevelApply()
    {
        base.LevelApply();
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
