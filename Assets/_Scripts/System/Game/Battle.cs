using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : MonoBehaviour
{
    public Battle nextBattle;

    public AreaCombat StartArea;
    public bool combat = false;

    public List<Transform> SpawnerPoints = new List<Transform>();
    public List<GameObject> EntitiesSpawn = new List<GameObject>();

    private List<Entity> spawned = new List<Entity>();

    public List<GameObject> walls = new List<GameObject>();

    public int enemyNumCount = 10;
    private int actualEnemy;

    private float cadence = 1f;
    float nextCheck;


    public float cadenceSpawnBattle = 10;
    public float cadenceRangeSpawnBattle = 5;

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
                CreateEnemy();
                enemyNumCount = enemyNumCount - 1;
                nextCheck = Time.time + cadence;
            }else
            {
                if (combat)
                {
                    EndCombat();
                }
            }
        }

    }

    public void StartCombat()
    {
        combat = true;
        WallsEnabled(true);
        Debug.Log("Start Combat");

    }

    public void EndCombat()
    {
        combat = false;
        WallsEnabled(false);
        Debug.Log("End Combat");
    }

    public void CreateEnemy()
    {
        int randomPoint = Random.Range(0, SpawnerPoints.Count);

        GameObject enemy = Instantiate(EntitiesSpawn[actualEnemy], SpawnerPoints[randomPoint].position, SpawnerPoints[randomPoint].rotation);

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


}
