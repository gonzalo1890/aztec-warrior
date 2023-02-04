using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public List<Sprite> skulls = new List<Sprite>();
    public Image skullImage;

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
    public Image itemsInventoryWeaponEquipedFrame;
    public Image itemsInventoryWeaponEquiped;
    public Image itemsInventoryWeaponEquipedAmmo;
    public Image itemsInventoryWeaponEquipedElement;
    public Text AmmoText;

    //InventorySkill
    public Image itemSkillAttackImage;
    public Image itemSkillAttackFrameImage;
    public Image itemSkillExtraImage;
    public Image itemSkillExtraFrameImage;
    public Image itemColdDownSkill;
    public List<Color> SkillColdDownColors;
    Coroutine coldDown;

    private RectTransform itemSelectCanvas;
    public List<Color> itemlevelColor;
    public List<Sprite> itemElementSprite;
    public List<Sprite> itemAmmoSprite;
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
            skullImage.sprite = SkullSelected(statsBars[stat].value);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        statsBars[stat].value = endValue;
        yield return null;
    }

    public Sprite SkullSelected(float value)
    {
        Sprite result = null;

        if (value >= 1000f)
        {
            result = skulls[10];
        }
        if (value > 900f)
        {
            result = skulls[9];
        }
        else if (value > 800f)
        {
            result = skulls[8];
        }
        else if (value > 700f)
        {
            result = skulls[7];
        }
        else if (value > 600f)
        {
            result = skulls[6];
        }
        else if (value > 500f)
        {
            result = skulls[5];
        }
        else if (value > 400f)
        {
            result = skulls[4];
        }
        else if (value > 300f)
        {
            result = skulls[3];
        }
        else if (value > 200f)
        {
            result = skulls[2];
        }
        else if (value > 100f)
        {
            result = skulls[1];
        }
        else
        {
            result = skulls[0];
        }

        return result;
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
        itemsInventoryWeaponEquipedFrame.color = itemlevelColor[(int)weapon.itemLevel];
        itemsInventoryWeaponEquipedElement.sprite = itemElementSprite[(int)weapon.damageElement];
        itemsInventoryWeaponEquipedAmmo.sprite = itemAmmoSprite[(int)weapon.ammoType];
    }

    public void UpdateAmmo(int value)
    {
        if(value == -1)
        {
            AmmoText.gameObject.SetActive(false);
            return;
        }
        else
        {
            AmmoText.gameObject.SetActive(true);
        }

        AmmoText.text = value.ToString();
    }

    public void ColdDownSkill(float cadence, bool isOn)
    {
        if(isOn)
        {
            itemColdDownSkill.color = SkillColdDownColors[0];
            if(coldDown != null)
            {
                StopCoroutine(coldDown);
                coldDown = StartCoroutine(animColdDown(cadence));
            }else
            {
                coldDown = StartCoroutine(animColdDown(cadence));
            }

        }
        else
        {
            itemColdDownSkill.color = SkillColdDownColors[0];
        }
    }

    public IEnumerator animColdDown(float waitTime)
    {
        float elapsedTime = 0;
        while (elapsedTime < waitTime)
        {
            itemColdDownSkill.fillAmount = Mathf.Lerp(0, 1, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        itemColdDownSkill.color = SkillColdDownColors[1];
        itemColdDownSkill.fillAmount = 1;
        coldDown = null;
        yield return null;
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

        string newItemInfo = item.itemName;

        if (item.GetComponent<Weapon>() != null)
        {
            Weapon weapon = item.GetComponent<Weapon>();

            float cadence = weapon.cadence;
            string cadenceString = cadence.ToString("F3");
            newItemInfo += "\nDamage: " + weapon.damage + "\nCadence: " + cadenceString + "\nCritic Prob: " + weapon.criticalHitProbability + "%";
            itemInfo.transform.GetChild(2).transform.gameObject.SetActive(true);
            itemInfo.transform.GetChild(2).GetComponent<Image>().sprite = itemElementSprite[(int)weapon.damageElement];
        }
        else if (item.GetComponent<Skill>() != null)
        {
            Skill skill = item.GetComponent<Skill>();
            newItemInfo += "\nCadence: " + skill.cadence + " seg." + "\nDescription: " + skill.description;
            itemInfo.transform.GetChild(2).transform.gameObject.SetActive(false);
        }
        else
        {
            newItemInfo += "\nValue: " + item.itemValue + "\nWeight: " + item.itemWeight;
            itemInfo.transform.GetChild(2).transform.gameObject.SetActive(false);
        }

        itemInfo.transform.GetChild(1).GetComponent<Text>().text = newItemInfo;
        itemInfo.transform.GetChild(0).GetComponent<Image>().color = itemlevelColor[(int)item.itemLevel];
        itemInfo.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = item.itemIcon;



    }

    public void ItemInfoWorldProcess(Item item)
    {
        if (item != null)
        {
            itemInfoWorld.SetActive(true);

            string newItemInfo = item.itemName;

            if (item.GetComponent<Weapon>() != null)
            {
                Weapon weapon = item.GetComponent<Weapon>();
                float cadence = weapon.cadence;
                string cadenceString = cadence.ToString("F3");
                newItemInfo += "\nDamage: " + weapon.damage + "\nCadence: " + cadenceString + "\nCritic Prob: " + weapon.criticalHitProbability + "%";
                itemInfo.transform.GetChild(2).transform.gameObject.SetActive(true);
                itemInfo.transform.GetChild(2).GetComponent<Image>().sprite = itemElementSprite[(int)weapon.damageElement];
            }
            else if (item.GetComponent<Skill>() != null)
            {
                Skill skill = item.GetComponent<Skill>();
                float cadence = skill.cadence;
                string cadenceString =  cadence.ToString("F3");
                newItemInfo += "\nCadence: " + cadenceString + "\nDescription: " + skill.description;
                itemInfo.transform.GetChild(2).transform.gameObject.SetActive(false);
            }
            else
            {
                newItemInfo += "\nValue: " + item.itemValue + "\nWeight: " + item.itemWeight;
                itemInfo.transform.GetChild(2).transform.gameObject.SetActive(false);
            }

            itemInfoWorld.transform.GetChild(1).GetComponent<Text>().text = newItemInfo;
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
            itemSkillAttackFrameImage.color = itemlevelColor[(int)item.itemLevel];
        }

        if (item.GetComponent<SkillExtra>() != null)
        {
            itemSkillExtraImage.sprite = item.itemIcon;
            itemSkillExtraFrameImage.color = itemlevelColor[(int)item.itemLevel];
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
