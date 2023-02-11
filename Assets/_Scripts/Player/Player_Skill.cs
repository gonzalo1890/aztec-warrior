using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
public class Player_Skill : MonoBehaviour
{
    public FirstPersonController fps;
    public GameObject cameraMain;
    public CharacterController character;
    // Start is called before the first frame update
    void Start()
    {
        fps = GameManager.Instance.player.GetComponent<FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActiveSkill(int skill, bool isActive)
    {

        //character.Move(inputDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
    }

    public void PlayerJump(float forceJump)
    {
        fps.Jump(forceJump);
    }
    public void PlayerMove(float speed, float time)
    {
        fps.MoveDirection(speed, time);
    }
}
