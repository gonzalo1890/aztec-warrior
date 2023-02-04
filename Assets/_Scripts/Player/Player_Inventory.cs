using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    public Transform itemsContainer;

    private int indexActualWeapon;

    public List<Weapon> SlotsWeapon;
    public List<ItemSaved> weaponItems = new List<ItemSaved>();
    private Item itemReward;
    private int slotSelected = -1;
    private int slotWeapon = 0;

    public Skill actualSkillAttack;
    public Skill actualSkillExtra;

    void Start()
    {

    }

    void Update()
    {
        InputProcess();
    }

    void InputProcess()
    {
        if (Input.GetKeyDown(KeyCode.Q)) //Arma Anterior
        {
            PreviousWeapon();
        }
        if (Input.GetKeyDown(KeyCode.E)) //Arma siguiente
        {
            NextWeapon();
        }

        if (Input.GetKeyDown(KeyCode.R)) //Soltar objeto seleccionado
        {
            //GameManager.Instance.canvasManager.DropItem();
        }
    }

    public void SaveItem(Item item)
    {
        UseItem(item);
    }


    //Cuando tengamos que cargar un arma o una habilidad
    public void UseItem(Item item)
    {
        if (item.GetComponent<Consumable>() != null)
        {
            Consumable consumable = item.GetComponent<Consumable>();
            GameManager.Instance.playerStats.ConsumableProcess(consumable.consumableType, item.itemValue);
            Destroy(item.gameObject);
        }

        if (item.GetComponent<Weapon>() != null)
        {
            if(CheckInventoryWeaponSpace())
            {
                Weapon weapon = item.GetComponent<Weapon>();
                SaveWeapon(weapon, slotSelected);
            }
            else
            {
                itemReward = item;
                GameManager.Instance.canvasManager.OpenInventoryWeapon(true);
                return;
            }
        }
        if (item.GetComponent<Skill>() != null)
        {
            if (item.GetComponent<SkillAttack>() != null)
            {
                if (actualSkillAttack != null)
                {
                    actualSkillAttack.isEquiped = false;
                }
                Skill skill = item.GetComponent<Skill>();
                skill.isEquiped = true;
                GameManager.Instance.canvasManager.AddItem(item);
                actualSkillAttack = skill;
            }

            if (item.GetComponent<SkillExtra>() != null)
            {
                if (actualSkillExtra != null)
                {
                    actualSkillExtra.isEquiped = false;
                }
                Skill skill = item.GetComponent<Skill>();
                skill.isEquiped = true;
                GameManager.Instance.canvasManager.AddItem(item);
                actualSkillExtra = skill;
            }
        }

        item.transform.SetParent(itemsContainer);
        item.transform.localPosition = Vector3.zero;
        item.GetComponent<Collider>().enabled = false;
        item.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void DropItem(Item item)
    {
        item.transform.SetParent(null);
        items.Remove(item);
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        item.lastPosition = newPosition;
        item.GetComponent<Collider>().enabled = true;
        item.transform.GetChild(0).gameObject.SetActive(true);
    }

    public Weapon GetActualWeapon()
    {
        return SlotsWeapon[slotWeapon];
    }

    void NextWeapon()
    {
        if (SlotsWeapon[slotWeapon] != null)
        {
            SlotsWeapon[slotWeapon].isEquiped = false;
        }
        if (slotWeapon < SlotsWeapon.Count-1)
        {
            if (SlotsWeapon[slotWeapon + 1] != null)
            {
                slotWeapon = slotWeapon + 1;
            }
        }else
        {
            slotWeapon = 0;
        }

        if (SlotsWeapon[slotWeapon] != null)
        {
            EquipWeapon(SlotsWeapon[slotWeapon], slotWeapon);
        }
    }

    void PreviousWeapon()
    {
        if (SlotsWeapon[slotWeapon] != null)
        {
            SlotsWeapon[slotWeapon].isEquiped = false;
        }
        if (slotWeapon > 0)
        {
            if (SlotsWeapon[slotWeapon - 1] != null)
            {
                slotWeapon = slotWeapon - 1;
            }
        }
        else
        {
            slotWeapon = SlotsWeapon.Count - 1;
        }

        if (SlotsWeapon[slotWeapon] != null)
        {
            EquipWeapon(SlotsWeapon[slotWeapon], slotWeapon);
        }
    }

    public bool CheckInventoryWeaponSpace()
    {
        for (int i = 0; i < SlotsWeapon.Count; i++)
        {
            if(SlotsWeapon[i] == null)
            {
                slotSelected = i;
                return true;
            }
        }
        slotSelected = -1;
        return false;
    }

    //Enviamos el arma que va a ser reemplazada
    public void ReplaceWeapon(Item item)
    {
        Weapon weaponItem = item.GetComponent<Weapon>();
        Weapon weaponReward = itemReward.GetComponent<Weapon>();

        for (int i = 0; i < SlotsWeapon.Count; i++)
        {
            if(SlotsWeapon[i] == weaponItem)
            {
                DiscardWeapon(weaponItem);
                SaveWeapon(weaponReward, i);
                GameManager.Instance.canvasManager.OpenInventoryWeapon(false);
                return;
            }
        }
    }
    public void EquipWeapon(Weapon weapon, int slot)
    {
        SlotsWeapon[slotWeapon].isEquiped = false;
        SlotsWeapon[slot].isEquiped = true;
        slotWeapon = slot;
        GameManager.Instance.playerWeapon.ActiveWeapon(weapon.weaponIndex, true);
        GameManager.Instance.canvasManager.WeaponEquiped(weapon);
        GameManager.Instance.playerStats.UpdateAmmo();

    }

    public void SaveWeapon(Weapon weapon, int slot)
    {
        for (int i = 0; i < SlotsWeapon.Count; i++)
        {
            if (SlotsWeapon[i] != null)
            {
                SlotsWeapon[i].isEquiped = false;
            }
        }

        //Se carga el slot con el arma nueva
        SlotsWeapon[slot] = weapon;

        //Se setea el indice actual del arma
        indexActualWeapon = slot;

        //Se equipa el arma
        EquipWeapon(SlotsWeapon[slot], slot);

        //Lo guarda en el inventario de armas en el canvas
        weaponItems[slot].SetItem(weapon);

        //Ahora el player lo contiene
        weapon.transform.SetParent(itemsContainer);
        //Se resetea la posicion
        weapon.transform.localPosition = Vector3.zero;
        //Se apaga el collider
        weapon.GetComponent<Collider>().enabled = false;
        //Se apaga el modelo 3d
        weapon.transform.GetChild(0).gameObject.SetActive(false);       
        

        //GameManager.Instance.canvasManager.AddItem(weapon, slot);

    }

    public void DiscardWeapon(Weapon weapon)
    {
        weapon.transform.SetParent(null);
        Destroy(weapon);
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
