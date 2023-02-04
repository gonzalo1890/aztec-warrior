using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Difficulty {  Roockie, Elite, legendary }
public class Roguelite : MonoBehaviour
{
    public Camera cameraStart;
    public List<CombatInstance> combatInstances = new List<CombatInstance>();


    public bool DebugOn = false;

    // Start is called before the first frame update
    void Start()
    {
        if(DebugOn == true)
        {
            GameManager.Instance.playerData.LoadGame();
            GameManager.Instance.playerStats.ChangeHealth(99999999);
            GoRedemption();
        }
        else
        {
            StartGame();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void StartGame()
    {
        StartCoroutine(StartCinematic());
    }

    public IEnumerator StartCinematic()
    {
        yield return new WaitForSeconds(1);
        GameManager.Instance.playerData.LoadGame();
        yield return new WaitForSeconds(10);
        GameManager.Instance.playerStats.ChangeHealth(99999999);
        GameManager.Instance.canvasManager.OpenMenu(true);
    }

    public void Redemption()
    {
        GameManager.Instance.canvasManager.OpenMenu(false);

        if(GameManager.Instance.playerLineage.descendantNumber > 0)
        {
            GameManager.Instance.canvasManager.OpenRedemption(true);
        }
        else
        {
            GameManager.Instance.playerReward.GenerateReward();
            GameManager.Instance.canvasManager.OpenGamePanel(true);
            cameraStart.gameObject.SetActive(false);
        }        
    }

    public void GoRedemption()
    {
        GameManager.Instance.canvasManager.OpenMenu(false);
        GameManager.Instance.canvasManager.OpenRedemption(false);
        GameManager.Instance.canvasManager.OpenGamePanel(true);
        GameManager.Instance.playerReward.GenerateReward();
        cameraStart.gameObject.SetActive(false);
    }


    public void Death()
    {
        GameManager.Instance.canvasManager.OpenGamePanel(false);
        GameManager.Instance.canvasManager.OpenDeathPanel(true);
        GameManager.Instance.playerLineage.RestartLineage();
        Invoke(nameof(FinishDeath), 10);
    }

    public void FinishDeath()
    {
        GameManager.Instance.playerData.SaveGame();
        GameManager.Instance.canvasManager.OpenDeathPanel(false);
        GameManager.Instance.ResetGame();
    }



}

public class CombatInstance
{
    public Difficulty difficulty;
    public int enemyDeath;
}