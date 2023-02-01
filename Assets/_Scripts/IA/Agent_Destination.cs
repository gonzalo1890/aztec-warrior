using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum DestinationType {Follow, Point, Randomize, Still}
public class Agent_Destination : MonoBehaviour
{
    public DestinationType destinationType;
    public NavMeshAgent nav;
    public Transform target;
    public Animator anim;
    private bool isNavegate = false;
    Vector3 moveBlend;

    public bool navigation = false;
    // Start is called before the first frame update
    void Start()
    {
        //target = GameManager.Instance.player.transform;
        nav = GetComponent<NavMeshAgent>();
        anim = transform.GetChild(0).GetComponent<Animator>();
        Invoke("StartNavigation", 1);
    }

    // Update is called once per frame
    void Update()
    {
        if(!navigation)
        {
            return;
        }

        switch (destinationType)
        {
            case DestinationType.Follow:
                FollowState();
                break;
            case DestinationType.Point:
                PointState();
                break;
            case DestinationType.Still:
                StillState();
                break;
            default:
                break;
        }
    }

    void StartNavigation()
    {
        navigation = true;
    }
    public void NewTargetPath(Transform _target, DestinationType _destinationType)
    {
        destinationType = _destinationType;
        target = _target;
        isNavegate = true;
    }

    void FollowState()
    {
        AnimationSetup();
        if (target != null & isNavegate)
        {
            if (nav != null)
            {
                if (nav.isActiveAndEnabled)
                {
                    nav.SetDestination(target.position);
                }
            }
        }
    }
    void PointState()
    {
        AnimationSetup();
        if (target != null & isNavegate)
        {
            nav.SetDestination(target.position);
            isNavegate = false;
        }
    }

    void StillState()
    {
        nav.SetDestination(target.position);
        anim.SetFloat("X", 0f);
        anim.SetFloat("Z", 0f);
        anim.SetBool("IsMoving", false);
        nav.velocity = Vector3.zero;
    }


    void AnimationSetup()
    {
        if (nav.desiredVelocity != Vector3.zero)
        {
            moveBlend = nav.desiredVelocity;
            moveBlend = transform.InverseTransformDirection(moveBlend);
            anim.SetFloat("X", moveBlend.x);
            anim.SetFloat("Z", moveBlend.z);
            anim.SetBool("IsMoving", true);
        }
        else
        {
            anim.SetBool("IsMoving", false);
        }
    }

    Vector3 RotateTowardsUp(Vector3 start, float angle)
    {
        // if you know start will always be normalized, can skip this step
        start.Normalize();

        Vector3 axis = Vector3.Cross(start, Vector3.up);

        // handle case where start is colinear with up
        if (axis == Vector3.zero) axis = Vector3.right;

        return Quaternion.AngleAxis(angle, axis) * start;
    }
}
