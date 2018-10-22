using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mom : MonoBehaviour {

    public float speed = 6f;

    private Vector3 movement;
    private Rigidbody player;

    private void Awake()
    {
        player = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Move(h,v);

    }

    private void Move(float h, float v)
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;
        player.MovePosition(transform.position + movement);
    }
}
