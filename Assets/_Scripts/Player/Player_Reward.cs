using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Reward : MonoBehaviour
{
    public List<GameObject> WeaponsMelee = new List<GameObject>();
    public List<GameObject> WeaponsDistance = new List<GameObject>();

    public List<GameObject> skillAttacks = new List<GameObject>();
    public List<GameObject> skillExtras = new List<GameObject>();

    public List<ItemSaved> RewardItems = new List<ItemSaved>();

    // Start is called before the first frame update
    void Start()
    {
        //GenerateReward();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateReward()
    {
        RewardItems[0].SetItem(GetWeapon().GetComponent<Item>());
        RewardItems[1].SetItem(GetWeapon().GetComponent<Item>());
        RewardItems[2].SetItem(GetSkill().GetComponent<Item>());

        GameManager.Instance.canvasManager.OpenReward(true);
    }


    public GameObject GetWeapon()
    {
        GameObject objectWeapon = null;

        int random = Random.Range(0, 100);
        if (random > 50)
        {
            objectWeapon = WeaponsMelee[Random.Range(0, WeaponsMelee.Count)];
        }
        else
        {
            objectWeapon = WeaponsDistance[Random.Range(0, WeaponsDistance.Count)];
        }

        GameObject newItem = Instantiate(objectWeapon, Vector3.zero, Quaternion.identity);

        Item item = newItem.GetComponent<Item>();
        int level = Random.Range(0, 5);
        item.itemLevel = (ItemLevel)level;

        int damageElement = Random.Range(0, 5);
        item.GetComponent<Weapon>().damageElement = (DamageElement)damageElement;

        item.LevelApply();

        return newItem;
    }

    public GameObject GetSkill()
    {
        GameObject objectSkill = null;

        int random = Random.Range(0, 100);
        if (random > 50)
        {
            objectSkill = skillAttacks[Random.Range(0, skillAttacks.Count)];
        }
        else
        {
            objectSkill = skillExtras[Random.Range(0, skillExtras.Count)];
        }

        GameObject newItem = Instantiate(objectSkill, Vector3.zero, Quaternion.identity);

        Item item = newItem.GetComponent<Item>();
        int level = Random.Range(0, 5);
        item.itemLevel = (ItemLevel)level;

        item.LevelApply();

        return newItem;
    }
}
