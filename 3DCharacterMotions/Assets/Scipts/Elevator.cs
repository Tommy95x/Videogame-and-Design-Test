using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour {

    public bool move;
    public GameObject cylinder;

    private int stop;
    private Transform transform;
    

	// Use this for initialization
	void Awake () {
        transform = GetComponent<Transform>();
        move = false;
        stop = 0;
	}
	
	// Update is called once per frame
	void Update () { 
        if (move)
        {
            if (transform.transform.position.y < 3.5f)
            {
                cylinder.transform.localPosition -= transform.up * Time.deltaTime;
                transform.position += transform.up * Time.deltaTime;
                cylinder.transform.Rotate(new Vector3(0, 15, 0) * Time.deltaTime);
            }
        }
    }
}
