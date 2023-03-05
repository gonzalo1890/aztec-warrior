using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public CanvasManager canvasManager;

    public TimeManager timeManager;

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

    public bool menuView = false;

    public bool pauseOn = false;
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
        Time.timeScale = 1f;

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

    private void Update()
    {
        if (!menuView)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pauseOn = !pauseOn;
                canvasManager.OpenPause(pauseOn);
                if (pauseOn)
                {
                    timeManager.resetTime();
                    Time.timeScale = 0f;
                }
                else
                {
                    Time.timeScale = 1f;
                }
            }

            if (pauseOn)
            {
                if (Input.GetKeyDown(KeyCode.F1))
                {
                    roguelite.ResetGame();
                }
                if (Input.GetKeyDown(KeyCode.F2))
                {
                    Application.Quit();
                }
            }
        }
    }
    public void ResetGame()
    {
        SceneManager.UnloadSceneAsync(1);
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
    public GameObject emitter;
    public bool isMelee = false;
}