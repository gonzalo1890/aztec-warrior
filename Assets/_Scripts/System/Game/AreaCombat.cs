using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaCombat : MonoBehaviour
{
    private bool inArea = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<CharacterController>() != null)
        {
            inArea = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CharacterController>() != null)
        {
            inArea = false;
        }
    }
    public void SetInArea(bool value)
    {
        inArea = value;
    }
    public bool getInArea()
    {
        return inArea;
    }
}
