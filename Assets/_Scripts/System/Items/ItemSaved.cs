using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSaved : MonoBehaviour
{
    public Item itemSaved;

    public void SetItem(Item item)
    {
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
