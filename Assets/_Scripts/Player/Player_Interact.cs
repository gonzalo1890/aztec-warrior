using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Interact : MonoBehaviour
{
    public State state;

    private void Update()
    {
        if (state != null)
            state.Tick(Time.deltaTime);
    }
}
