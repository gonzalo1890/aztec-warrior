using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : MonoBehaviour
{
    public Battle nextBattle;

    public AreaCombat StartArea;
    public bool combat = false;

    public GameObject itemHealth;
    public List<GameObject> SpawnHealthPoints;

    public GameObject itemAmmo;
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

    public bool isEndLevel = false;
    void Start()
    {
        
    }


    void Update()
    {
        //Si el jugador entra en la zona de combate este inicia
        if(StartArea.getInArea() && !combat)
        {
            StartCombat();
        }

        //Se crea un enemigo cada cierto tiempo
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
                    if (enemyActualSpawned < 1) //Si ya no quedan enemigos que spawnear
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
        GenerateItems();
        GameManager.Instance.roguelite.StartCombat(isEndLevel);
        WallsEnabled(true);
        StartArea.SetInArea(false);
        StartArea.gameObject.SetActive(false);
        Debug.Log("Start Combat");

    }

    public void EndCombat()
    {
        combat = false;
        GameManager.Instance.roguelite.EndCombat();
        WallsEnabled(false);
        if (!rewardAdd)
        {
            if (GameManager.Instance.roguelite.progressWorld < 4)
            {
                GameManager.Instance.playerReward.GenerateReward(1);
            }
            rewardAdd = true;
        }

        if(isEndLevel)
        {
            BossDefeat();
        }
        Debug.Log("End Combat");
    }

    public void GenerateItems()
    {
        for (int i = 0; i < SpawnHealthPoints.Count; i++)
        {
            GameObject health = Instantiate(itemHealth, SpawnHealthPoints[i].transform.position, transform.rotation);
        }

        for (int i = 0; i < SpawnAmmoPoints.Count; i++)
        {
            GameObject ammo = Instantiate(itemAmmo, SpawnAmmoPoints[i].transform.position, transform.rotation);
        }
    }

    public void BossDefeat()
    {
        GameManager.Instance.roguelite.ProgressWorld();
    }


    public void CreateEnemy()
    {
        int randomPoint = Random.Range(0, SpawnerPoints.Count);
        int randomEnemy = Random.Range(0, EntitiesSpawn.Count);
        GameObject enemy = Instantiate(EntitiesSpawn[randomEnemy], SpawnerPoints[randomPoint].position, SpawnerPoints[randomPoint].rotation);

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

    public void EnemyDeath(int spirit = 1)
    {
        GameManager.Instance.playerStats.ChangeSpirit(spirit);
        enemyActualSpawned = enemyActualSpawned - 1;
    }

}
