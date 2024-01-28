using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Gravity : MonoBehaviour
{
    //Settings
    public float Gravity = -0.8f;
    public float JumpHight = 0.05f;
    public float redios = 0.1f;

    //Boolians
    public bool OnGround;
    public bool CanJump;

    //Refrences
    public CharacterController Player;
    public Transform Base;
    public LayerMask Ground;
    public SnakeController SnakeControllerScript;
    public AudioSource JumpSound;

    //Private Things
    private Vector3 Velocity;


    private void Update()
    {
        GravityManagerFunction();
        
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Jump();
        }
    }

    //Gravity
    public void GravityManagerFunction()
    {
        OnGround = Physics.CheckSphere(Base.position, redios, Ground);

        if (OnGround == true)
        {
            Velocity.y = -2f;

            // When Player in on ground = He can Steer  ( Affecting SnakeController Script )
            SnakeControllerScript.SteerLock = false;
        }
        else
        {
            Velocity.y += Gravity * Time.deltaTime;
            Player.Move(Velocity);

            // // When Player in on ground = He can not Steer  ( Affecting SnakeController Script )
            SnakeControllerScript.SteerLock = true;
        }

    }

    //Jump
    public void Jump()
    {
        if (OnGround && CanJump)
        {
            Velocity.y = Mathf.Sqrt(JumpHight * -2f * Gravity * Time.deltaTime);
            Player.Move(Velocity);

            //sound
            JumpSound.Play();
        }
        
    }


}
