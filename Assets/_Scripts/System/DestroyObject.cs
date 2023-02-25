using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public float timeDestroy = 0.1f;
    void Start()
    {
        Destroy(gameObject, timeDestroy);
    }


}
