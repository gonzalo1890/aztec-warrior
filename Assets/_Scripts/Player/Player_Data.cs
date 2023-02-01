using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Player_Data : MonoBehaviour
{

    public void SaveGame()
    {
        DateTime actualTime = DateTime.UtcNow;

        List<int> stats = new List<int>();
        stats = GameManager.Instance.playerStats.SaveStats();

        int descendant = GameManager.Instance.playerLineage.descendantNumber;

        SaveObject saveObject = new SaveObject
        {
            Stats = stats,
            Date = actualTime.ToString(),
            Descendant = descendant,
        };

        string json = JsonUtility.ToJson(saveObject);
        string newSave = "/game.save";
        File.WriteAllText(Application.persistentDataPath + newSave, json);
        Debug.Log("Game Saved");
    }

    
    public void LoadGame()
    {
        string newSave = "/game.save";
        if (File.Exists(Application.persistentDataPath + newSave))
        {
            string saveString = File.ReadAllText(Application.persistentDataPath + newSave);
            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);
            int descendant = saveObject.Descendant;
            GameManager.Instance.playerLineage.descendantNumber = descendant;
            GameManager.Instance.playerStats.LoadStats(saveObject.Stats);
        }
    }
   


    void TeleportPlayer(Vector3 pos)
    {
        CharacterController character = GetComponent<CharacterController>();
        character.enabled = false;
        transform.position = pos;
        character.enabled = true;
    }
}

[System.Serializable]
public class SaveObject
{
    public string Date;
    public List<int> Stats = new List<int>();
    public int Descendant;
}