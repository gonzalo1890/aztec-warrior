using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Lineage : MonoBehaviour
{
    public int descendantNumber = 0;

    public int bloodlust = 0;
    public int rage = 0;
    public int agony = 0;
    public int brutality = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartLineage()
    {
        descendantNumber = descendantNumber + 1;

    }


}
