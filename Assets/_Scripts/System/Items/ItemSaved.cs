using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemSaved : MonoBehaviour
{
    public Item itemSaved;

    private Color levelColor;
    private Sprite elementalSprite;
    public Image weaponFrame;
    public Image weaponIcon;
    public Image weaponElemental;
    public Text weaponName;

    public void SetItem(Item item)
    {
        Weapon weapon = null;
        if(item.GetComponent<Weapon>() != null)
        {
            weapon = item.GetComponent<Weapon>();
            levelColor = GameManager.Instance.canvasManager.itemlevelColor[(int)weapon.itemLevel];
            elementalSprite = GameManager.Instance.canvasManager.itemElementSprite[(int)weapon.damageElement];
            weaponFrame.color = levelColor;
            weaponIcon.sprite = item.itemIcon;
            if (weaponName != null)
            {
                weaponName.text = item.itemName + " [" + item.itemLevel.ToString() + "]";
                weaponName.color = levelColor;
            }
            if (weaponElemental != null)
            {
                weaponElemental.sprite = elementalSprite;
            }
        }

        Upgrade upgrade = null;
        if(item.GetComponent<Upgrade>() != null)
        {
            upgrade = item.GetComponent<Upgrade>();
            levelColor = GameManager.Instance.canvasManager.itemlevelColor[(int)upgrade.itemLevel];
            weaponFrame.color = levelColor;
            weaponIcon.sprite = item.itemIcon;
            if (weaponName != null)
            {
                weaponName.text = item.itemName + " [" + item.itemLevel.ToString() + "]";
                weaponName.color = levelColor;
            }
        }



        Skill skill = null;
        if (item.GetComponent<Skill>() != null)
        {
            skill = item.GetComponent<Skill>();
            levelColor = GameManager.Instance.canvasManager.itemlevelColor[(int)skill.itemLevel];
            weaponFrame.color = levelColor;
            weaponIcon.sprite = item.itemIcon;
            if (weaponName != null)
            {
                weaponName.text = item.itemName + " [" + item.itemLevel.ToString() + "]";
                weaponName.color = levelColor;
            }
        }



        itemSaved = item;

    }
    public void SendItemToInventory(bool isReward)
    {
        GameManager.Instance.canvasManager.OpenInventoryWeapon(false);
        GameManager.Instance.playerInventory.SaveItem(itemSaved);

        if(isReward)
        {
            GameManager.Instance.playerReward.SelectReward(itemSaved);
            GameManager.Instance.canvasManager.OpenReward(false);
        }
    }

    public void ReplaceWeapon()
    {
        GameManager.Instance.playerInventory.ReplaceWeapon(itemSaved);
    }
}
