using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WheelItem : MonoBehaviour
{
    public Color baseColor;
    public Color hoverColor;
    public Color disableColor;

    public Image background;
    public Image frame;
    public Image icon;

    public bool isLocked = false;
    public Item weaponSaved;

    public Sprite unlockedSlot;

    void Awake()
    {
        if(isLocked)
        {
            background.color = disableColor;
        }
        else
        {
            background.color = baseColor;
        }

    }

    public void Select()
    {
        if(isLocked)
        {
            return;
        }
        background.color = hoverColor;
    }

    public void Deselect()
    {
        if (isLocked)
        {
            return;
        }
        background.color = baseColor;
    }

    public void Disable()
    {
        background.color = disableColor;
    }

    public void Unlocked()
    {
        isLocked = false;
        background.color = baseColor;
        icon.sprite = unlockedSlot;
    }

    public void SaveWeapon(Item weapon)
    {
        weaponSaved = weapon;
        frame.color = GameManager.Instance.canvasManager.GetLevelColor((int)weapon.itemLevel);
        icon.sprite = weapon.itemIcon;
    }
}
