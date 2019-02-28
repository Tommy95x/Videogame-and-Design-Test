using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour {

    //Parameters to manage the player exect sounds
    [Header("Player sound source end clips")]
    public AudioSource playerSoundSource;
    public AudioClip footstepSound;
    public AudioClip stomachSound;
    public AudioClip jumpSound;

    //Player' speed
    [Header("Player' speeds")]
    public float fowardSpeed = 6f;

    //Fixed time that the player must attend to do a static animation and that doesn't change
    [Header("Wating time to activate some static animation")]
    public float waitingTime = 5f;

    [Header("Height of jump & length")]
    public float jumpHigh = 330f;

    private Animator anim;
    private float speed;
    private Rigidbody playerRigidbody;
    private CapsuleCollider playerCollider;
    private Vector3 movement;
    //Timer that is decreamented each time that the player is in static position
    private float countdown;
    //Different bools use to implement the player movements
    private bool isWalking;
    private bool isPushing;
    private float landingTime; // time to wait in order to do another jump. This avoid a quick double jump
    private CameraControl camera;

    public LayerMask groundLayer;   // the layer on which the character can jump

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerCollider = GetComponent<CapsuleCollider>();
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraControl>();
        anim = GetComponent<Animator>();
        speed = fowardSpeed;
        isPushing = false;
        isWalking = false;
        countdown = waitingTime;
        playerRigidbody.freezeRotation = true;
        landingTime = 0f;  // the player can jump
        groundLayer = LayerMask.GetMask("Default");
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horiziontal");
        float v = Input.GetAxisRaw("Vertical");

        if(IsGrounded() && landingTime <=0 && 
            Input.GetAxisRaw("IsJumping").Equals(1))
        {
            Juping();
        }else if (h == 0 && v == 0)
        {
            if(isPushing || isWalking)
                SetStatic();
            else
            {
                countdown -= Time.deltaTime;
                if (countdown <= 0)
                    StaticAnimation(UnityEngine.Random.Range(0, 3));
            }
        }
        else
        {
            if (gameObject.GetComponent<PlayerState>().GetIsHandFree())
            {
                Move(h, v);
                Turning();
                WalkingOrPushingWithoutFreeHand();
            }else
                try
                {
                    if (!gameObject.GetComponentInChildren<TakeableObjectState>().GetCollision())
                    {
                        Move(h, v);
                        Turning();
                        WalkingOrPushingWithFreeHand();
                    }
                }
                catch (NullReferenceException)
                {
                    gameObject.GetComponent<PlayerInteraction>().setTakableObject(null);
                    gameObject.GetComponent<PlayerState>().SetHandFree(true);
                }
        }
        if (landingTime > 0)
            landingTime -= Time.deltaTime;
    }

    private void WalkingOrPushingWithoutFreeHand()
    {
        if (IsGrounded())
        {
            if (isPushing)
            {
                isWalking = false;
                anim.SetBool("IsPushing", true);
                if (!playerSoundSource.isPlaying)
                    playerSoundSource.PlayOneShot(footstepSound);
            }
            else
            {
                isWalking = true;
                anim.SetBool("IsWalking", true);
                if (!playerSoundSource.isPlaying)
                    playerSoundSource.PlayOneShot(footstepSound);
            }
        }
    }

    private void WalkingOrPushingWithFreeHand()
    {
        if (IsGrounded())
        {
            if (isPushing)
            {
                isWalking = false;
                anim.SetBool("IsPushingWithObject", true);
                if (!playerSoundSource.isPlaying)
                    playerSoundSource.PlayOneShot(footstepSound);
            }
            else
            {
                isWalking = true;
                anim.SetBool("IsWalkingWithObject", true);
                if (!playerSoundSource.isPlaying)
                    playerSoundSource.PlayOneShot(footstepSound);
            }
        }
    }

    private void Turning()
    {
        transform.rotation = Quaternion.LookRotation(movement);
    }

    private void Move(float h, float v)
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;
        playerRigidbody.MovePosition(transform.position + movement);
    }

    private void StaticAnimation(int animation)
    {
        Invoke("SetStatic", 2f);
        anim.SetInteger("StaticAnimation", animation);
        playerSoundSource.transform.position = new Vector3(0f, 1f, 0f);
        playerSoundSource.clip = stomachSound;
        playerSoundSource.Play();
    }

    private void SetStatic()
    {
        if (anim.GetBool("IsWalking"))
        {
            isWalking = false;
            anim.SetBool("IsWalking", false);
        }
        else if (anim.GetBool("IsPushing"))
            anim.SetBool("IsPushing", false);
        else if (anim.GetBool("IsWalkingWithObject"))
        {
            isWalking = false;
            anim.SetBool("IsWalkingWithObject", false);
        }
        else if (anim.GetBool("IsPushingWithObject"))
            anim.SetBool("IsPushingWithObject", false);
        else if (!anim.GetInteger("StaticAnimation").Equals(0))
            anim.SetInteger("StaticAnimation", 0);

        speed = fowardSpeed;
        countdown = waitingTime;
        playerSoundSource.transform.position = new Vector3(0f, 0f, 0f);
        playerSoundSource.loop = false;
        playerSoundSource.Stop();
    }

    private void Juping()
    {
        landingTime = .6f;
        playerRigidbody.AddForce(0f, jumpHigh, 0f);
        playerSoundSource.Stop();
        if (playerSoundSource.clip != jumpSound)
            playerSoundSource.PlayOneShot(jumpSound);
        anim.SetBool("IsJumping", !IsGrounded());
    }

    private bool IsGrounded()
    {
        // return true if the collision with the specified layer happens vertically
        return Physics.CheckCapsule(playerCollider.bounds.center, new Vector3(playerCollider.bounds.center.x, playerCollider.bounds.min.y, playerCollider.bounds.center.z),
                                    playerCollider.radius * 0.9f, groundLayer);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 0)
            anim.SetBool("IsJumping", IsGrounded());
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("WoodBox") || other.CompareTag("MetalBox") || other.CompareTag("Cage"))
        {
            isPushing = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("WoodBox") || other.CompareTag("MetalBox") || other.CompareTag("Cage"))
        {
            isPushing = false;

        }
    }

}
