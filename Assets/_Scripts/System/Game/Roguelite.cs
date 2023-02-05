using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Difficulty {  Roockie, Elite, legendary }
public class Roguelite : MonoBehaviour
{
    public Camera cameraStart;

    public List<AudioEffect> Sounds = new List<AudioEffect>();

    public bool DebugOn = false;

    public int progressWorld = 0; //1 PasoPantano / 2 PasoSelva / 3 PasoCiudad / 4 PasoPiramide

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
            StartCredits();            
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void StartCredits()
    {
        GameManager.Instance.canvasManager.Open32kPanel(true);
        Invoke(nameof(End32KPresent), 3);
    }

    public void End32KPresent()
    {
        GameManager.Instance.canvasManager.Open32kPanel(false);
        StartGame();
    }

    public void StartGame()
    {
        StartCoroutine(StartCinematic());
        Sounds[1].Play(1, "Musica");
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
            GameManager.Instance.canvasManager.UpdateStats(1, GameManager.Instance.playerStats.spirit);
            GameManager.Instance.canvasManager.UpdatePoint(0);
            GameManager.Instance.canvasManager.UpdatePoint(1);
            GameManager.Instance.canvasManager.UpdatePoint(2);
            GameManager.Instance.canvasManager.UpdatePoint(3);
            GameManager.Instance.canvasManager.UpdatePoint(4);
            GameManager.Instance.canvasManager.OpenRedemption(true);
        }
        else
        {
            GoRedemption();
        }        
    }

    public void GoRedemption()
    {
        GameManager.Instance.playerStats.InitStats();
        GameManager.Instance.playerData.SaveGame();
        GameManager.Instance.canvasManager.OpenMenu(false);
        GameManager.Instance.canvasManager.OpenRedemption(false);
        GameManager.Instance.canvasManager.OpenGamePanel(true);
        GameManager.Instance.playerReward.GenerateReward();
        cameraStart.gameObject.SetActive(false);
        Sounds[1].Stop();
        Sounds[1].Play(0, "Musica");
        Sounds[0].Play(1, "ambiente");
    }

    public void GoCredits()
    {
        GameManager.Instance.canvasManager.OpenMenu(false);
        GameManager.Instance.canvasManager.OpenCredits(true);
        Invoke(nameof(EndCredits), 10);
    }

    void EndCredits()
    {
        GameManager.Instance.canvasManager.OpenMenu(true);
        GameManager.Instance.canvasManager.OpenCredits(false);
    }
    
    public void GoQuit()
    {
        Application.Quit();
    }

    public void Death()
    {
        Sounds[1].Stop();
        Sounds[1].Play(0, "Musica");

        Sounds[0].Stop();
        Sounds[0].Play(0, "ambiente");

        GameManager.Instance.canvasManager.OpenGamePanel(false);
        GameManager.Instance.canvasManager.OpenDeathPanel(true);
        GameManager.Instance.playerData.SaveGame();
        GameManager.Instance.playerLineage.RestartLineage();
        Invoke(nameof(FinishDeath), 10);
    }

    public void FinishDeath()
    {        
        GameManager.Instance.canvasManager.OpenDeathPanel(false);
        GameManager.Instance.ResetGame();
    }

    public void WinGame()
    {
        GameManager.Instance.canvasManager.OpenWinPanel(true);
        GameManager.Instance.playerStats.ChangeSpirit((int)(GameManager.Instance.playerStats.spirit * 0.1f));
        GameManager.Instance.playerData.SaveGame();
        GameManager.Instance.playerLineage.RestartLineage();
        Invoke(nameof(FinishWin), 10);
    }
    public void FinishWin()
    {
        
        GameManager.Instance.canvasManager.OpenDeathPanel(false);
        GameManager.Instance.ResetGame();
    }
    public void StartCombat(bool isBoss = false)
    {
        if (isBoss)
        {
            Sounds[1].Stop();
            Sounds[1].Play(3, "Musica");
            Sounds[3].Stop();
            Sounds[2].Play();
        }
        else
        {
            Sounds[1].Stop();
            Sounds[1].Play(2, "Musica");
            Sounds[3].Stop();
            Sounds[2].Play();
        }
    }

    public void EndCombat()
    {
        Sounds[1].Stop();
        Sounds[1].Play(0, "Musica");
        Sounds[2].Stop();
        Sounds[3].Play();
    }

    public void ProgressWorld()
    {
        progressWorld += 1;

        if(progressWorld > 3)
        {
            WinGame();
        }
    }
}
