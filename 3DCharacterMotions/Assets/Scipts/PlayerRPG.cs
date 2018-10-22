using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public bool isGrounded;
    public float rotSpeed;
    public float jumpHigh;
    [Header("Time to wait to see a static animation")]
    public float timeToWaitIdleAnim;
    public AudioSource playerAudioSource;
    public AudioClip footSound;
    public AudioClip stomachSound;

    private float speed;
    private float walk_speed = 0.01f;
    private float run_speed = .05f;
    private Rigidbody rb;
    private Animator anim;
    private CapsuleCollider col;
    private float idleTime;

    
	// Use this for initialization
	void Awake () {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider>();
        isGrounded = true;
        idleTime = timeToWaitIdleAnim;
	}


    // Update is called once per frame
    void Update() {

        float z = Input.GetAxis("Vertical");
        float y = Input.GetAxis("Horizontal");

        transform.Rotate(0, y * rotSpeed, 0);
        transform.Translate(0, 0, z * speed);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            SetJump();

        //This method works only with a keyboard. In this case sometime the player starts later with animations (run or walk animation), but the transaction start correctly
        if (Input.GetKey(KeyCode.LeftShift) && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)))
            SetRun();
        else if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)))
            SetWalk();
        else if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)) && Input.GetKeyUp(KeyCode.LeftShift))
            SetWalk();
        else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
            SetStatic();

         if (z == 0 && y == 0 && anim.GetInteger("Motions").Equals(0)) 
        {
                idleTime -= Time.deltaTime;
                if (idleTime <= 0)
                {
                    SetIdleMotions();
                    idleTime = timeToWaitIdleAnim;
                }
        }

        //Second case that works also with pad. Here we have the previuos problem and additional 
        //the player doesn't stop its walking/running because a z > 0 continues to be registered also with A and D keys
      /*if (z == 0 && y == 0)
      {
          if (idleTime == timeToWaitIdleAnim)
          {
              idleTime -= Time.deltaTime;
              SetStatic();
          }
          else
          {
              idleTime -= Time.deltaTime;
              if (idleTime <= 0)
              {
                  SetIdleMotions();
              }
          }
      }else if(z == 0 && y != 0)
      {
            transform.Rotate(0, y*rotSpeed, 0);
      }else if(z != 0)
      {
          if (Input.GetKey(KeyCode.LeftShift))
              SetRun();
          else
              SetWalk();
            transform.Rotate(0, y * rotSpeed, 0);
            transform.Translate(0, 0, z * speed);
        */
      

    }

    private void SetStatic()
    {
        anim.SetInteger("Motions", 0);
        playerAudioSource.transform.position = new Vector3(0, 0, 0);
        playerAudioSource.enabled = false;
        footstepSound(false);
    }

    //Static animation that are executed in a random way after a set time
    private void SetIdleMotions()
    {
        int idleSelect = UnityEngine.Random.Range(3, 5);
        Debug.Log(idleSelect);
        switch (idleSelect) {
            case 3:
                anim.SetInteger("Motions", 3);
                playerAudioSource.transform.position = new Vector3(0, 1, 0);
                playerAudioSource.enabled = true;
                playerAudioSource.clip = stomachSound;
                playerAudioSource.Play();
                break;
            case 4:
                anim.SetInteger("Motions", 4);
                break;
        }
        Invoke( "SetStatic" ,2f);
    }

    //Methods that change the animations
    private void SetJump()
    {
        idleTime = timeToWaitIdleAnim;
        isGrounded = false;
        rb.AddForce(0, jumpHigh, 0);
        anim.SetTrigger("Jump");
        idleTime = timeToWaitIdleAnim;
    }

    private void SetRun()
    {
        idleTime = timeToWaitIdleAnim;
        speed = run_speed;
        anim.SetInteger("Motions", 2);
        idleTime = timeToWaitIdleAnim;
    }

    private void SetWalk()
    {
        idleTime = timeToWaitIdleAnim;
        speed = walk_speed;
        anim.SetInteger("Motions", 1);
        footstepSound(true);
        idleTime = timeToWaitIdleAnim;
    }

    //Manage of feet sound 
    private void footstepSound(bool start)
    {
        if(start)
        {
            playerAudioSource.clip = footSound;
            playerAudioSource.enabled = true;
            playerAudioSource.loop = true;
        }
        else
        {
            playerAudioSource.enabled = false;
            playerAudioSource.loop = false;
        }   
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            Debug.Log("Hello");
        }else if (collision.gameObject.CompareTag("Lever") && Input.GetMouseButtonDown(0))
        {
            //Methods not implemented yet
        }
        else if (collision.gameObject.CompareTag("Button") && Input.GetMouseButtonDown(0))
        {
            //Methods not implemented yet
        }
    }

}
