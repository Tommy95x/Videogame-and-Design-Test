using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour {

    [Header("Player sound source end clips")]
    public AudioSource playerSoundSource;
    public AudioClip footstepSound;
    public AudioClip stomachSound;

    [Header("Player' speeds")]
    public float fowardSpeed;
   

    [Header("Wating time to activate some static animation")]
    public float waitingTime;

    [Header("Height of jump & lenght")]
    public float jumpHigh;

    private Animator anim;
    private float speed;
    private Rigidbody playerRigidbody;
    private bool isGrounded;
    private bool isWalking;
    private bool isPushing;
    private float countdown;
    private Vector3 movement;


    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        speed = fowardSpeed;
        isGrounded = true;
        isPushing = false;
        isWalking = false;
        countdown = waitingTime;
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jumping();
        }else if (isPushing)
        {
            Pushing(h,v);
        }else if(h == 0 && v == 0)
        {
            if (isPushing || isWalking)
            {
                SetStatic();
            }
            else
            {
                countdown -= Time.deltaTime;
                if(countdown <= 0)
                {
                    StaticAnimation(UnityEngine.Random.Range(0, 3));
                }
            }
        }
        else {
            Move(h, v);
            Turning(h, v);
        }   
    }

    private void Move(float h, float v)
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;
        playerRigidbody.MovePosition(transform.position + movement);
    }

    private void Turning(float h, float v)
    {
        transform.rotation = Quaternion.LookRotation(movement);
    }

    private void Jumping()
    {
        isGrounded = false;
        playerRigidbody.AddForce(0f, jumpHigh, 0f);
        anim.SetBool("IsJumping", !isGrounded);
    }

    private void Pushing(float h, float v)
    {
        if(v == 1 || h == 1)
        {
            anim.SetBool("IsPushing", true);

        }else if(v == -1 || h == -1)
        {
            anim.SetBool("IsPushing", false);
        }
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;
        playerRigidbody.MovePosition(transform.position + movement);
    }

    private void SetStatic()
    {
        isWalking = false;
        countdown = waitingTime;
        speed = fowardSpeed;
        anim.SetBool("IsWalking", false);
    }

    private void StaticAnimation(int v)
    {
        anim.SetInteger("IsStatic", v);
        Invoke("SetStatic", 2f);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            Debug.Log("IsGrounded");
            isGrounded = true;
            anim.SetBool("IsJumping", !isGrounded);
        }
    }

    public void setIsPushing(bool v)
    {
        isPushing = v;
    }

    public void changeSpeed(float metalPercentageReduction)
    {
        speed -= speed * metalPercentageReduction;
    }

   

}
