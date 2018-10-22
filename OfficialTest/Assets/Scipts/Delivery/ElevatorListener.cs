using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorListener : MonoBehaviour {

    [Header("Height of elevetor' movement")]
    public float stop;
    //public GameObject cylinder;

    private bool move;


    // Use this for initialization
    private void Awake () {
        move = false;
	}

    private void OnEnable()
    {
        EventManager.StartListening("StartMovementElevator", StartMovementElevator);
    }

    private void StartMovementElevator()
    {
        EventManager.StopListening("StartMovementElevator", StartMovementElevator);
        move = true;
    }

    // Update is called once per frame
    void Update () { 
        if (move)
        {
            if (transform.transform.position.y <= stop)
            {
                transform.position += transform.up * Time.deltaTime;
                //cylinder.transform.localPosition -= transform.up * Time.deltaTime;
                //cylinder.transform.Rotate(new Vector3(0, 15, 0) * Time.deltaTime);
            }
        }
    }
}
