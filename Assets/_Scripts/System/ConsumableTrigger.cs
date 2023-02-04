using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableTrigger : MonoBehaviour
{
    public consumableType type;
    public int value;


    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.GetComponent<Player_Stats>() != null)
        {
            GameManager.Instance.playerStats.ConsumableProcess(type, value);

            Destroy(gameObject);
        }
    }
}
