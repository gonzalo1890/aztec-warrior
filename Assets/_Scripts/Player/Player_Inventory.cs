using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    public Transform itemsContainer;

    public Weapon actualWeapon; //ARMA EQUIPADA

    void Start()
    {

    }

    void Update()
    {
        InputProcess();
    }

    void InputProcess()
    {
        if (Input.GetKeyDown(KeyCode.Q)) //Abrir Inventario
        {
            GameManager.Instance.canvasManager.OpenInventory();
        }

        if (Input.GetKeyDown(KeyCode.R)) //Soltar objeto seleccionado
        {
            GameManager.Instance.canvasManager.DropItem();
        }
    }

    public void SaveItem(Item item)
    {
        items.Add(item);
        GameManager.Instance.canvasManager.AddItemToInventory(item);
        item.isTaked = true;
        item.transform.SetParent(itemsContainer);
        item.transform.localPosition = Vector3.zero;
        item.GetComponent<Collider>().enabled = false;
        item.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void UseItem(Item item)
    {
        if (item.GetComponent<Consumable>() != null)
        {
            Consumable consumable = item.GetComponent<Consumable>();
            GameManager.Instance.playerStats.ConsumableProcess(consumable.consumableType, item.itemValue);
            item.isTaked = false;
            items.Remove(item);
            Destroy(item.gameObject);
        }

        if(item.GetComponent<Weapon>() != null)
        {
            if (actualWeapon != null)
            {
                actualWeapon.isEquiped = false;
                SaveItem(actualWeapon);
                GameManager.Instance.playerWeapon.ActiveWeapon(actualWeapon.weaponIndex, false);
            }
            

            Weapon weapon = item.GetComponent<Weapon>();
            weapon.isEquiped = true;
            GameManager.Instance.playerWeapon.ActiveWeapon(weapon.weaponIndex, true);
            actualWeapon = weapon;

        }

    }

    public void DropItem(Item item)
    {
        item.transform.SetParent(null);
        items.Remove(item);
        item.isTaked = false;
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        item.lastPosition = newPosition;
        item.GetComponent<Collider>().enabled = true;
        item.transform.GetChild(0).gameObject.SetActive(true);
    }



    public List<int> SaveInventory()
    {
        List<int> inventory = new List<int>();
        for (int i = 0; i < items.Count; i++)
        {
            inventory.Add(items[i].id);
        }
        return inventory;
    }
    public void LoadInventory(List<int> inventory)
    {
        ClearInventory();
        for (int i = 0; i < inventory.Count; i++)
        {
            GameObject itemSpawned = Instantiate(Resources.Load("Prefabs/Items/" + inventory[i], typeof(GameObject))) as GameObject;
            SaveItem(itemSpawned.GetComponent<Item>());
        }
    }

    void ClearInventory()
    {
        GameManager.Instance.canvasManager.ClearItems();
        for (int i = 0; i < items.Count; i++)
        {
            Item itemClear = items[i];

            itemClear.transform.SetParent(null);
            items[i] = null;
            Destroy(itemClear.gameObject);
        }
        items.Clear();
    }
}
