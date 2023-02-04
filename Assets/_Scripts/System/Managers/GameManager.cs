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

    public Player_Inventory playerInventory;

    public Player_Stats playerStats;

    public Player_Data playerData;

    public Player_Weapon playerWeapon;

    public Player_Skill playerSkill;

    public Player_Reward playerReward;

    public Player_Lineage playerLineage;

    public Roguelite roguelite;

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
        playerData = player.GetComponent<Player_Data>();
        playerSkill = player.GetComponent<Player_Skill>();
        playerReward = player.GetComponent<Player_Reward>();
        playerLineage = player.GetComponent<Player_Lineage>();

        roguelite = GetComponent<Roguelite>();

        //LoadConfigGame();

        SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }

    public void ResetGame()
    {
        SceneManager.UnloadSceneAsync(2);
        SceneManager.LoadScene(0, LoadSceneMode.Single);
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