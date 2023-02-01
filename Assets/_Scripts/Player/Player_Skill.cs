using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Skill : MonoBehaviour
{

    public GameObject cameraMain;
    public CharacterController character;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActiveSkill(int skill, bool isActive)
    {

        //character.Move(inputDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
    }


    public void PlayerMove(Vector3 direction, float speed)
    {
        character.Move(direction.normalized * (speed * Time.deltaTime));// + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
    }
}
