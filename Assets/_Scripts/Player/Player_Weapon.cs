using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Weapon : MonoBehaviour
{
    public List<GameObject> weapons;


    public GameObject damageArea;

    public Damage actualDamage;
    private int actualWeapon;

    private ParticleSystem ShootParticle;

    public List<GameObject> Sounds = new List<GameObject>();
    public GameObject noAmmoSound;

    public Transform damagePoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    } 

    public void ActiveWeapon(int weapon, bool isActive)
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            weapons[i].SetActive(false);
        }

        weapons[weapon].SetActive(isActive);

        if (weapons[weapon].transform.childCount > 0)
        {
            if (weapons[weapon].transform.GetChild(0).transform.GetChild(0) != null)
            {
                ShootParticle = weapons[weapon].transform.GetChild(0).transform.GetChild(0).GetComponent<ParticleSystem>();
            }
        }

        if (isActive)
        {
            actualWeapon = weapon;
        }
    }

    public void SetAnimatorTrigger(string animTrigger)
    {
        if (weapons[actualWeapon].transform.GetChild(0).GetComponent<Animator>() != null)
        {
            weapons[actualWeapon].transform.GetChild(0).GetComponent<Animator>().SetTrigger(animTrigger);
        }
        else
        {
            for (int i = 0; i < weapons[actualWeapon].transform.GetChild(0).childCount; i++)
            {
                if (weapons[actualWeapon].transform.GetChild(0).GetChild(i).GetComponent<Animator>() != null)
                {
                    weapons[actualWeapon].transform.GetChild(0).GetChild(i).GetComponent<Animator>().SetTrigger(animTrigger);
                }
            }
        }
    }
    
    public void WeaponSound()
    {
        GameObject sound = Instantiate(Sounds[actualWeapon], transform.position, transform.rotation) as GameObject;
    }

    public void WeaponNoAmmo()
    {
        GameObject sound = Instantiate(noAmmoSound, transform.position, transform.rotation) as GameObject;
    }
    public void CreateDamageArea()
    {
        GameObject damageA = Instantiate(damageArea, damagePoint.position, transform.rotation) as GameObject;
        damageA.GetComponent<DamageArea>().damageEmitter = GameManager.Instance.player;
        damageA.GetComponent<DamageArea>().damageSave = actualDamage;
        damageA.GetComponent<DamageArea>().damageElement = actualDamage.damageElement;
        damageA.GetComponent<DamageArea>().damageSave.isMelee = true;
    }

    public void StartParticleShoot()
    {
        if (ShootParticle != null)
        {
            ShootParticle.Play();
        }
    }
    public Transform GetShootPoint()
    {
        return weapons[actualWeapon].transform.GetChild(0).transform.GetChild(1).transform;
    }
}
