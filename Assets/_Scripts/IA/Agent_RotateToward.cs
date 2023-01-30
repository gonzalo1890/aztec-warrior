using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Agent_RotateToward : MonoBehaviour
{
    private NavMeshAgent nav;
    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponentInParent<NavMeshAgent>();
        nav.updateRotation = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (nav.velocity.sqrMagnitude > Mathf.Epsilon)
        {
            transform.rotation = Quaternion.LookRotation(nav.velocity.normalized);
        }
    }
}
