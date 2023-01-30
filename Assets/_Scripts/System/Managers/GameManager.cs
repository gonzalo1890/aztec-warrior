using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public CanvasManager canvasManager;
    public WorldManager worldManager;

    public GameObject player;
    [HideInInspector]
    public Player_Inventory playerInventory;
    [HideInInspector]
    public Player_Stats playerStats;
    [HideInInspector]
    public Player_Menu playerMenu;

    public Player_Weapon playerWeapon;

    public Transform targetInfo;

    public int saveId = 0;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }
    void Start()
    {
        worldManager = GetComponent<WorldManager>();

        playerInventory = player.GetComponent<Player_Inventory>();
        playerStats = player.GetComponent<Player_Stats>();
        playerMenu = player.GetComponent<Player_Menu>();

        LoadConfigGame();

        SceneManager.LoadScene(2, LoadSceneMode.Additive);
    }


    public void SaveConfigGame(int value)
    {
        SaveConfigObject saveObject = new SaveConfigObject
        {
            saveId = value,
        };

        string json = JsonUtility.ToJson(saveObject);
        string newSave = "/config.ini";
        File.WriteAllText(Application.persistentDataPath + newSave, json);
        Debug.Log("Game Saved");
    }


    public void LoadConfigGame()
    {
        if (File.Exists(Application.persistentDataPath + "/config.ini"))
        {
            string saveString = File.ReadAllText(Application.persistentDataPath + "/config.ini");
            SaveConfigObject saveObject = JsonUtility.FromJson<SaveConfigObject>(saveString);
            saveId = saveObject.saveId;
            GameManager.Instance.playerMenu.nextIDSave = saveId;
            GameManager.Instance.playerMenu.UpdateLoadSaveList();
        }
        else
        {
            SaveConfigGame(0);
        }
    }

    [System.Serializable]
    public class SaveConfigObject
    {
        public int saveId;
    }
}



public class Damage
{
    public int damageValue;
    public DamageElement damageElement;
    public bool isCritic;
    public Vector3 pointDamage;
}