using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Player_Menu : MonoBehaviour
{

    public int nextIDSave = 0;
    public List<SaveObject> saves;
    public int indexSaveSelected = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //Abrir Inventario
        {
            GameManager.Instance.canvasManager.OpenMenu();
        }
    }
    public void ResumeGame()
    {

    }


    public void SaveGame()
    {
        DateTime actualTime = DateTime.UtcNow;
        List<int> stats = new List<int>();
        stats = GameManager.Instance.playerStats.SaveStats();

        List<int> inventory = new List<int>();
        inventory = GameManager.Instance.playerInventory.SaveInventory();

        SaveObject saveObject = new SaveObject
        {
            Id = nextIDSave,
            Date = actualTime.ToString(),
            PlayerPosition = transform.position,
            PlayerRotation = transform.rotation,
            Stats = stats,
            Inventory = inventory,
        };

        string json = JsonUtility.ToJson(saveObject);
        string newSave = "/game" + nextIDSave + ".save";
        File.WriteAllText(Application.persistentDataPath + newSave, json);
        nextIDSave = GameManager.Instance.saveId + 1;
        GameManager.Instance.saveId = nextIDSave;
        GameManager.Instance.SaveConfigGame(nextIDSave);
        UpdateLoadSaveList();
        Debug.Log("Game Saved");
    }

    //Carga todas las partidas en la pantalla load/save
    public void UpdateLoadSaveList()
    {
        saves.Clear();

        for (int i = 0; i < nextIDSave; i++)
        {
            string newSave = "/game" + i + ".save";
            if (File.Exists(Application.persistentDataPath + newSave))
            {
                string saveString = File.ReadAllText(Application.persistentDataPath + newSave);
                SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);
                saves.Add(saveObject);
            }
        }
        if (saves.Count > 0)
        {
            GameManager.Instance.canvasManager.SetSaveGameItems(saves);
        }else
        {
            GameManager.Instance.canvasManager.SetSaveGameItems(null);
        }
    }
    
    public void LoadGame()
    {
        GameManager.Instance.canvasManager.SetLoading(true);
        Invoke(nameof(StopLoadGame), 3);
        string newSave = "/game" + indexSaveSelected + ".save";
        if (File.Exists(Application.persistentDataPath + newSave))
        {
            string saveString = File.ReadAllText(Application.persistentDataPath + newSave);
            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);

            TeleportPlayer(saveObject.PlayerPosition);
            transform.rotation = saveObject.PlayerRotation;
            GameManager.Instance.playerStats.LoadStats(saveObject.Stats);
            GameManager.Instance.playerInventory.LoadInventory(saveObject.Inventory);
        }
    }
    public void DeleteSave()
    {
        string newSave = "/game" + indexSaveSelected + ".save";
        File.Delete(Application.persistentDataPath + newSave);
        UpdateLoadSaveList();
    }

    public void StopLoadGame()
    {
        GameManager.Instance.canvasManager.OpenMenu();
        if (GameManager.Instance.canvasManager.inventoryObject.activeSelf)
        {
            GameManager.Instance.canvasManager.OpenInventory();
        }
        GameManager.Instance.canvasManager.SetLoading(false);
    }


    void TeleportPlayer(Vector3 pos)
    {
        CharacterController character = GetComponent<CharacterController>();
        character.enabled = false;
        transform.position = pos;
        character.enabled = true;
    }

    public void SettingsGame()
    {

    }

    public void QuitGame()
    {

    }
}

[System.Serializable]
public class SaveObject
{
    public int Id;
    public string Date;
    public Vector3 PlayerPosition;
    public Quaternion PlayerRotation;
    public List<int> Stats = new List<int>();
    public List<int> Inventory = new List<int>();
}