using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Weapon : MonoBehaviour
{
    public List<GameObject> weapons;

    public GameObject damageArea;

    public Damage actualDamage;
    private int actualWeapon;
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

        if(isActive)
        {
            actualWeapon = weapon;
        }
    }

    public Animator GetAnimatorWeapon()
    {
        if (weapons[actualWeapon].GetComponent<Animator>() != null)
        {
            return weapons[actualWeapon].GetComponent<Animator>();
        }else
        {
            return null;
        }
    }


    public void CreateDamageArea()
    {
        GameObject damageA = Instantiate(damageArea, weapons[actualWeapon].transform.position, transform.rotation) as GameObject;
        damageA.GetComponent<DamageArea>().damageSave = actualDamage;
    }
}
