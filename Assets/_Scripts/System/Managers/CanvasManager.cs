using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CanvasManager : MonoBehaviour
{
    //Menu REAL
    public GameObject menuObject;
    public GameObject redemptionObject;
    public GameObject GamePanelObject;
    public GameObject panelDeath;


    //Menu ------------------

    public GameObject loading;
    public GameObject loadSaveMenu;
    public List<GameObject> savesItems;
    public RectTransform saveContent;
    public GameObject itemSaveObject;

    //Stats
    public Slider[] statsBars;
    public Text spiritText;


    //InventoryGeneral
    public GameObject prefabItem;
    public GameObject itemInfo;
    public GameObject itemInfoWorld;
    private Item itemSelect;
    //public GameObject itemSelector;

    //Reward
    public GameObject RewardObject;

    //InventoryWeapon
    public GameObject inventoryWeaponObject;
    public List <Image> itemsInventoryWeapon = new List<Image>();
    public Image itemsInventoryWeaponEquiped;
    public TMP_Text AmmoText;

    //InventorySkill
    public Image itemSkillAttackImage;
    public Image itemSkillExtraImage;


    private RectTransform itemSelectCanvas;
    public List<Color> itemlevelColor;

    //WorldObjects
    public GameObject damageInfoObject;

    #region Menu
    public void OpenMenu(bool value)
    {
        menuObject.SetActive(value);
    }
    public void OpenRedemption(bool value)
    {
        redemptionObject.SetActive(value);
    }
    public void OpenGamePanel(bool value)
    {
        GamePanelObject.SetActive(value);
    }
    public void OpenDeathPanel(bool value)
    {
        panelDeath.SetActive(value);
    }
    public void SetLoading(bool isActive)
    {
        loading.SetActive(isActive);
    }
    public void OpenLoadSaveMenu()
    {
        loadSaveMenu.SetActive(!loadSaveMenu.activeSelf);
    }


    #endregion

    #region Stats

    public void UpdateStats(int stat, int _value)
    {
        if (stat == 0) //Health
        {
            float startValue = statsBars[stat].value;
            StartCoroutine(AnimStats(startValue, _value, stat));
        }

        if (stat == 1) //Spirit
        {
            
        }

    }

    IEnumerator AnimStats(float startValue, float endValue, int stat)
    {
        float elapsedTime = 0;
        float waitTime = (startValue - endValue);
        waitTime = Mathf.Abs(waitTime * 0.005f);
        while (elapsedTime < waitTime)
        {
            statsBars[stat].value = Mathf.Lerp(startValue, endValue, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        statsBars[stat].value = endValue;
        yield return null;
    }

    #endregion

    public void OpenReward(bool value)
    {
        RewardObject.SetActive(value);
    }


    #region InventoryWeapon
    public void OpenInventoryWeapon(bool value)
    {
        inventoryWeaponObject.SetActive(value);
    }

    public void WeaponEquiped(Weapon weapon)
    {
        itemsInventoryWeaponEquiped.sprite = weapon.itemIcon;

    }

    public void UpdateAmmo(int value)
    {
        if(value == -1)
        {
            AmmoText.gameObject.SetActive(false);
        }
        else
        {
            AmmoText.gameObject.SetActive(true);
        }

        AmmoText.text = value.ToString();
    }
    public void ItemSelected(RectTransform item)
    {
        //itemSelector.SetActive(true);
        //itemSelector.GetComponent<RectTransform>().position = item.position;
        itemSelect = item.GetComponent<ItemSaved>().itemSaved;
        itemSelectCanvas = item;
        ItemInfoCanvasProcess(itemSelect);
    }
    public void ItemDeselected()
    {
        itemSelect = null;
        itemSelectCanvas = null;
        //itemSelector.SetActive(false);
        itemInfo.SetActive(false);
    }
    public void ItemInfoCanvasProcess(Item item)
    {
        itemInfo.SetActive(true);
        itemInfo.transform.GetChild(1).GetComponent<Text>().text = item.itemName + "\nValue: " + item.itemValue + "\nWeight: " + item.itemWeight;
        itemInfo.transform.GetChild(0).GetComponent<Image>().color = itemlevelColor[(int)item.itemLevel];
        itemInfo.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = item.itemIcon;

    }

    public void ItemInfoWorldProcess(Item item)
    {
        if(item != null)
        {
            itemInfoWorld.SetActive(true);
            itemInfoWorld.transform.GetChild(1).GetComponent<Text>().text = item.itemName + "\nValue: " + item.itemValue + "\nWeight: " + item.itemWeight;
            itemInfoWorld.transform.GetChild(0).GetComponent<Image>().color = itemlevelColor[(int)item.itemLevel];
            itemInfoWorld.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = item.itemIcon;
        }
        else
        {
            itemInfoWorld.SetActive(false);
        }
    }

    public void AddItem(Item item, int indexWeapon = -1)
    {
        //GameObject itemAdded = (GameObject)Instantiate(prefabItem) as GameObject;
        //itemAdded.transform.GetChild(0).GetComponent<Text>().text = item.itemName;
        //Item saveItem = item;

        if (item.GetComponent<SkillAttack>() != null)
        {
            itemSkillAttackImage.sprite = item.itemIcon;
        }

        if (item.GetComponent<SkillExtra>() != null)
        {
            itemSkillExtraImage.sprite = item.itemIcon;
        }

        if (item.GetComponent<Weapon>() != null)
        {
            itemsInventoryWeapon[indexWeapon].sprite = item.itemIcon;
        }


    }



    public void UseItem(Item item, GameObject itemCanvas)
    {
        Destroy(itemCanvas);
        //itemSelector.SetActive(false);
        itemInfo.SetActive(false);
        GameManager.Instance.playerInventory.UseItem(item);
        //itemsInventory.Remove(itemCanvas);
    }

    public void DropItem()
    {

        if(itemSelect != null)
        {
            Destroy(itemSelectCanvas.gameObject);
            //itemSelector.SetActive(false);
            itemInfo.SetActive(false);
            GameManager.Instance.playerInventory.DropItem(itemSelect);
            //itemsInventory.Remove(itemSelect.gameObject);
        }

    }

    public void ClearItems()
    {

        for (int i = 0; i < itemsInventoryWeapon.Count; i++)
        {
            //Destroy(itemsInventory[i].gameObject);
            //itemSelector.SetActive(false);
            itemInfo.SetActive(false);
        }

    }

    #endregion

    #region World
    
    public void SetDamageInfo(int damage, DamageElement element, bool isCriticalHit, Vector3 position)
    {
        GameObject canvas = Instantiate(damageInfoObject, position, damageInfoObject.transform.rotation);
        canvas.GetComponent<CanvasDamage>().DamageVisual(damage, element, isCriticalHit);
    }

    #endregion

}
