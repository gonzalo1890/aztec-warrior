using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : MonoBehaviour
{
    public Battle nextBattle;

    public AreaCombat StartArea;
    public bool combat = false;

    public List<GameObject> SpawnHealthPoints;
    public List<GameObject> SpawnAmmoPoints;


    public List<Transform> SpawnerPoints = new List<Transform>();
    public List<GameObject> EntitiesSpawn = new List<GameObject>();

    private List<Entity> spawned = new List<Entity>();

    public List<GameObject> walls = new List<GameObject>();

    public int enemyNumCount = 10;
    private int actualEnemy;

    private float cadence = 1f;
    float nextCheck;

    public int enemyActualSpawned = 0;
    public int enemyMaxSpawned = 5;

    public float cadenceSpawnBattle = 10;
    public float cadenceRangeSpawnBattle = 5;

    private bool rewardAdd = false;

    void Start()
    {
        
    }


    void Update()
    {
        if(StartArea.getInArea() && !combat)
        {
            StartCombat();
        }

        if (Time.time > nextCheck && combat)
        {
            if (enemyNumCount > 0)
            {
                if (enemyActualSpawned < enemyMaxSpawned)
                {
                    CreateEnemy();
                    enemyActualSpawned = enemyActualSpawned + 1;
                    enemyNumCount = enemyNumCount - 1;
                    nextCheck = Time.time + cadence;
                }
            }else
            {
                if (combat)
                {
                    if (enemyActualSpawned < 1)
                    {
                        EndCombat();
                    }
                }
            }
        }

    }

    public void StartCombat()
    {
        combat = true;
        GameManager.Instance.roguelite.Sounds[1].Stop();
        GameManager.Instance.roguelite.Sounds[1].Play(2, "Musica");
        WallsEnabled(true);
        StartArea.SetInArea(false);
        StartArea.gameObject.SetActive(false);
        Debug.Log("Start Combat");

    }

    public void EndCombat()
    {
        combat = false;
        GameManager.Instance.roguelite.Sounds[1].Stop();
        GameManager.Instance.roguelite.Sounds[1].Play(0, "Musica");
        WallsEnabled(false);
        if (!rewardAdd)
        {
            GameManager.Instance.playerReward.GenerateReward();
            rewardAdd = true;
        }
        Debug.Log("End Combat");
    }

    public void CreateEnemy()
    {
        int randomPoint = Random.Range(0, SpawnerPoints.Count);

        GameObject enemy = Instantiate(EntitiesSpawn[actualEnemy], SpawnerPoints[randomPoint].position, SpawnerPoints[randomPoint].rotation);

        if(enemy.GetComponent<EntityTarget>() != null)
        {
            enemy.GetComponent<EntityTarget>().battle = this;
        }

        float randomTimeCadence = Random.Range(cadenceSpawnBattle - cadenceRangeSpawnBattle, cadenceSpawnBattle + cadenceRangeSpawnBattle);

        cadence = randomTimeCadence;
    }

    public void WallsEnabled(bool isEnabled)
    {
        for (int i = 0; i < walls.Count; i++)
        {
            walls[i].SetActive(isEnabled);
        }
    }

    public void EnemyDeath()
    {
        enemyActualSpawned = enemyActualSpawned - 1;
    }

}
