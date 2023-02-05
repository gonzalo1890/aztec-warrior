using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Absorbable : MonoBehaviour
{
    public int type;

    public int value;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<CharacterController>() != null)
        {
            if(type == 0)
            {
                GameManager.Instance.playerStats.ChangeHealth(value);
            }
            if (type == 1)
            {
                GameManager.Instance.playerStats.ChangeBullet(value);
                GameManager.Instance.playerStats.ChangeShell(value/2);
                GameManager.Instance.playerStats.ChangeMisil(value/4);
                GameManager.Instance.playerStats.ChangeGranade(value/6);
            }
            Destroy(gameObject);
        }
    }
}
