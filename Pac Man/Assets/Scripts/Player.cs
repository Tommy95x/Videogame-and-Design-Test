using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [Header("Player Velocity")]
    public float velocity;
    
    [Header("Layer Identifier")]
    public LayerMask Walls;

    private Animator animator;
    private Transform tr;





    // Use this for initialization
    private void Awake()
    {
        animator = GetComponent<Animator>();
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update () {
        float xDir = Input.GetAxis("Horizontal");
        float yDir = Input.GetAxis("Vertical");
        if (canMove(xDir, yDir))
        {
            if (xDir != 0)
                yDir = 0;
            else
                xDir = 0;
            UpdateAnimation(xDir, yDir);
            tr.position = tr.position + (tr.right * Time.deltaTime * velocity * xDir) + (tr.up * Time.deltaTime * yDir * velocity);
        }
    }

    private bool canMove(float xDir, float yDir)
    {
        Vector3 direction;
        float delta;
        float threshold = 0 ;
        if (xDir > threshold)
        {
            direction = tr.right;
            delta = Mathf.Abs(xDir * velocity * Time.deltaTime);
        }
        else if (xDir < -threshold)
        {
            direction = -tr.right;
            delta = Mathf.Abs(xDir * velocity * Time.deltaTime);
        }
        else if (yDir > threshold)
        {
            direction = tr.up;
            delta = Mathf.Abs(yDir * velocity * Time.deltaTime);
        }
        else if (yDir < -threshold)
        {
            direction = -tr.up;
            delta = Mathf.Abs(yDir * Time.deltaTime * velocity);
        }
        else
            return true;

        RaycastHit2D hit = Physics2D.Raycast(tr.position, direction, delta, Walls);

        if (hit.collider != null)
            return false;
        else
            return true;
    }

    private void playerDeath()
    {
        animator.SetBool("Death", true);
        GameManager.instance.playerDeath();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Ghost")){
            playerDeath();
        }
    }


    private void DestoryObject()
    {
        Destroy(gameObject);
    }

    private void UpdateAnimation(float xDir, float yDir)
    {
        if (xDir > 0)
            animator.SetInteger("Motions", 1);
        else if (xDir < 0)
            animator.SetInteger("Motions", 2);
        else if (yDir > 0)
            animator.SetInteger("Motions", 3);
        else if (yDir < 0)
            animator.SetInteger("Motions", 4);
        else
            animator.SetInteger("Motions", 0);
    }
}
