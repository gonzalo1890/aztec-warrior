using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public Transform itemRoot;
    public List<Item> items;
    void Start()
    {
        InitializedWorld();
    }

    void InitializedWorld()
    {
        for (int i = 0; i < itemRoot.childCount; i++)
        {
            items.Add(itemRoot.GetChild(i).GetComponent<Item>());
            //items[i].id = i;
        }
    }
}

