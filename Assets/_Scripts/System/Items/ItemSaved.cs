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
            if (weaponElemental != null)
            {
                weaponElemental.sprite = elementalSprite;
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
            GameManager.Instance.canvasManager.OpenReward(false);
        }
    }

    public void ReplaceWeapon()
    {
        GameManager.Instance.playerInventory.ReplaceWeapon(itemSaved);
    }
}
