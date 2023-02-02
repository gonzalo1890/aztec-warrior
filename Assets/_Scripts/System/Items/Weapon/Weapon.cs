using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageElement { None, Fire, Ice, Electricity, Poison }
public enum AmmoType { Bullet, Shell, Misil, Granade }
public abstract class Weapon : Item
{
    //Con esto elegimos el modelo3d del arma alojado en la camara
    public int weaponIndex;

    public bool isEquiped = false;

    //Daño base del arma
    public int damage = 10;

    public AmmoType ammoType = AmmoType.Bullet;
    //Rango de aleatoreidad de daño que puede hacer un arma a partir de su daño base
    public int RandomRange = 5;


    //Cadencia del arma (en segundos)
    [SerializeField]
    private float cadence = 1f;
    float nextCheck;

    //Probabilidad de daño critico
    [SerializeField]
    public float criticalHitProbability = 3f;

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

    //Posicion de disparo
    public Transform firePosition;

    public AudioClip[] sounds;
    public ParticleSystem[] particles;

    private Animator anim;
    private AudioSource au;

    public Damage damageCalculated;

    void Start()
    {
        anim = GetComponent<Animator>();
        au = GetComponent<AudioSource>();
    }


    protected virtual void Update()
    {
        if(Input.GetButton("Fire1") && isEquiped)
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

        GameManager.Instance.playerWeapon.GetAnimatorWeapon().SetTrigger("Shoot");


    }

    public Damage CalculeDamage()
    {
        int value = Random.Range(damage - RandomRange, damage + RandomRange);
        DamageElement sendElement = DamageElement.None;
        bool sendIsCritic = false;

        if(CalculateElementDamage())
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
