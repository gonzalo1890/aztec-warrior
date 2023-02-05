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

    public List<Item> RewardItemsStock = new List<Item>();
    // Start is called before the first frame update
    void Start()
    {
        //GenerateReward();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            GenerateReward();
        }
    }

    public void GenerateReward()
    {
        RewardItemsStock[0] = GetWeapon().GetComponent<Item>();
        RewardItemsStock[1] = GetWeapon().GetComponent<Item>();
        RewardItemsStock[2] = GetSkill().GetComponent<Item>();

        RewardItems[0].SetItem(RewardItemsStock[0]);
        RewardItems[1].SetItem(RewardItemsStock[1]);
        RewardItems[2].SetItem(RewardItemsStock[2]);

        GameManager.Instance.canvasManager.OpenReward(true);
    }

    public void SelectReward(Item reward)
    {
        if (reward != null)
        {
            for (int i = 0; i < RewardItemsStock.Count; i++)
            {
                if (RewardItemsStock[i] != reward)
                {
                    Destroy(RewardItemsStock[i].gameObject);
                }
            }
        }else
        {
            for (int i = 0; i < RewardItemsStock.Count; i++)
            {
                Destroy(RewardItemsStock[i].gameObject);
            }
            GameManager.Instance.canvasManager.OpenReward(false);
        }
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
