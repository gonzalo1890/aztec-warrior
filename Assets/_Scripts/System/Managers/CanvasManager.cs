using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CanvasManager : MonoBehaviour
{
    //Menu
    public GameObject menuObject;
    public GameObject loading;
    public GameObject loadSaveMenu;
    public List<GameObject> savesItems;
    public RectTransform saveContent;
    public GameObject itemSaveObject;

    //Stats
    public Slider[] statsBars;

    //Inventory
    public GameObject inventoryObject;
    public GameObject itemContainer;
    public GameObject prefabItem;
    public GameObject itemSelector;
    public GameObject itemInfo;
    public GameObject itemInfoWorld;

    private Item itemSelect;
    private List <GameObject> itemsInventory = new List<GameObject>();
    private RectTransform itemSelectCanvas;
    public List<Color> itemlevelColor;

    //WorldObjects
    public GameObject damageInfoObject;

    #region Menu
    public void OpenMenu()
    {
        menuObject.SetActive(!menuObject.activeSelf);
    }

    public void SetLoading(bool isActive)
    {
        loading.SetActive(isActive);
    }
    public void OpenLoadSaveMenu()
    {
        loadSaveMenu.SetActive(!loadSaveMenu.activeSelf);
    }
    public void SetSaveGameItems(List<SaveObject> saves)
    {
        if (savesItems.Count > 0)
        {
            for (int i = 0; i < savesItems.Count; i++)
            {
                Destroy(savesItems[i].gameObject);
            }
        }

        savesItems.Clear();


        if (saves == null)
        {

            return;
        }

        for (int i = 0; i < saves.Count; i++)
        {
            GameObject item = Instantiate(itemSaveObject, transform.position, transform.rotation) as GameObject;
            item.transform.SetParent(saveContent);
            item.GetComponent<RectTransform>().localRotation = Quaternion.identity;
            item.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            item.GetComponent<ButtonSave>().saveId = saves[i].Id;
            item.transform.GetChild(1).GetComponent<Text>().text = saves[i].Id + " - " + saves[i].Date;
            savesItems.Add(item);
            //LayoutRebuilder.ForceRebuildLayoutImmediate(transformMyRewards as RectTransform);
        }
    }

    public void SetSelectedItem(GameObject save)
    {
        for (int i = 0; i < savesItems.Count; i++)
        {
            savesItems[i].GetComponent<ButtonSave>().SetButtonActive(false);
        }
        for (int i = 0; i < savesItems.Count; i++)
        {
            if(savesItems[i].GetComponent<ButtonSave>().gameObject == save)
            {
                savesItems[i].GetComponent<ButtonSave>().SetButtonActive(true);
                GameManager.Instance.playerMenu.indexSaveSelected = savesItems[i].GetComponent<ButtonSave>().saveId;
            }
        }
    }

    #endregion

    #region Stats

    public void UpdateStats(int stat, int _value)
    {
        float startValue = statsBars[stat].value;
        StartCoroutine(AnimStats(startValue, _value, stat));
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

    #region Inventory
    public void OpenInventory()
    {
        inventoryObject.SetActive(!inventoryObject.activeSelf);
    }

    public void ItemSelected(RectTransform item)
    {
        itemSelector.SetActive(true);
        itemSelector.GetComponent<RectTransform>().position = item.position;
        itemSelect = item.GetComponent<ItemSaved>().itemSaved;
        itemSelectCanvas = item;
        ItemInfoCanvasProcess(itemSelect);
    }
    public void ItemDeselected()
    {
        itemSelect = null;
        itemSelectCanvas = null;
        itemSelector.SetActive(false);
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

    public void AddItemToInventory(Item item)
    {
        GameObject itemInventory = (GameObject)Instantiate(prefabItem, itemContainer.transform) as GameObject;
        itemInventory.transform.GetChild(0).GetComponent<Text>().text = item.itemName;
        Item saveItem = item;
        itemInventory.GetComponent<Button>().onClick.AddListener(() => { UseItem(saveItem, itemInventory); });
        itemInventory.GetComponent<ItemSaved>().itemSaved = item; //Referencia item guardado en el boton
        itemsInventory.Add(itemInventory);
    }


    public void UseItem(Item item, GameObject itemCanvas)
    {
        Destroy(itemCanvas);
        itemSelector.SetActive(false);
        itemInfo.SetActive(false);
        GameManager.Instance.playerInventory.UseItem(item);
        itemsInventory.Remove(itemCanvas);
    }

    public void DropItem()
    {
        if(itemSelect != null)
        {
            Destroy(itemSelectCanvas.gameObject);
            itemSelector.SetActive(false);
            itemInfo.SetActive(false);
            GameManager.Instance.playerInventory.DropItem(itemSelect);
            itemsInventory.Remove(itemSelect.gameObject);
        }
    }

    public void ClearItems()
    {
        for (int i = 0; i < itemsInventory.Count; i++)
        {
            Destroy(itemsInventory[i].gameObject);
            itemSelector.SetActive(false);
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
