using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WheelSelector : MonoBehaviour
{
    public Vector2 normalizedMousePosition;
    public float currentAngle;
    public int selection;
    private int previousSelection;
    public GameObject[] menuItems;

    private WheelItem selectItem;
    private WheelItem previousSelectItem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //obtenemos que se esta seleccionando
        normalizedMousePosition = new Vector2(Input.mousePosition.x - Screen.width / 2, Input.mousePosition.y - Screen.height / 2);
        currentAngle = Mathf.Atan2(normalizedMousePosition.y, normalizedMousePosition.x) * Mathf.Rad2Deg;
        currentAngle = (currentAngle + 360) % 360;
        selection = (int)currentAngle / 90;


        //Debug.Log(selection);


        if(selection != previousSelection)
        {
            if (!menuItems[selection].GetComponent<WheelItem>().isLocked)
            {
                previousSelectItem = menuItems[previousSelection].GetComponent<WheelItem>();
                previousSelectItem.Deselect();
                previousSelection = selection;

                selectItem = menuItems[selection].GetComponent<WheelItem>();
                selectItem.Select();
            }
        }
    }

    public void SaveWeapon(Item weapon, int index)
    {
        menuItems[index].GetComponent<WheelItem>().SaveWeapon(weapon);
        menuItems[index].transform.GetChild(0).GetComponent<ItemSaved>().SetItem(weapon);

    }

    public void SelectWeapon()
    {
        if (menuItems[selection].GetComponent<WheelItem>().weaponSaved != null)
        {
            if (menuItems[selection].GetComponent<WheelItem>().weaponSaved.GetComponent<Weapon>() != null)
            {
                Weapon weapon = menuItems[selection].GetComponent<WheelItem>().weaponSaved.GetComponent<Weapon>();
                GameManager.Instance.playerInventory.EquipWeapon(weapon, selection);
            }
        }
    }

    public void UpdateSlots(int SlotsCount)
    {
        for (int i = 0; i < SlotsCount; i++)
        {
            menuItems[i].GetComponent<WheelItem>().Unlocked();
            GameManager.Instance.canvasManager.itemsInventoryWeapon[i].GetComponent<Button>().interactable = true;
        }
    }
}
