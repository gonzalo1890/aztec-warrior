using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemLevel { Common, Rare, Legendary, Epic, Unique }

public abstract class Item : MonoBehaviour, Iaction
{
    public int id;
    public string itemName;
    public int itemValue;
    public float itemWeight;
    public Sprite itemIcon;
    public ItemLevel itemLevel;

    //Se setea si el item esta predefinido
    public Vector3 initPosition = Vector3.zero;

    //Se setea cuando dropeamos un item
    public Vector3 lastPosition = Vector3.zero;
    public bool isTaked = false;
    public void OnAction(GameObject actionObject)
    {
        GameManager.Instance.playerInventory.SaveItem(this);
    }

    public void OnOver(GameObject actionObject)
    {
        if(actionObject != null)
        {
            GameManager.Instance.canvasManager.ItemInfoWorldProcess(this);
        }else
        {
            GameManager.Instance.canvasManager.ItemInfoWorldProcess(null);
        }
    }


}
