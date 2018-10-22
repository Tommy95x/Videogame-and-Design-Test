using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorListener : MonoBehaviour {

    [Header("Height of elevetor' movement")]
    public float stop;
    //Element that represents the gears
    //public GameObject cylinder;

    private bool move;


    // Use this for initialization
    private void Awake () {
        move = false;
	}

     //Initialization of trigger listener
    private void OnEnable()
    {
        EventManager.StartListening("StartMovementElevator", StartMovementElevator);
    }

    //Call metod by an EventManager.TriggerEvent that start the movement execution
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

                //Part of code to add in case we want to implement also the gears
                //cylinder.transform.localPosition -= transform.up * Time.deltaTime;
                //cylinder.transform.Rotate(new Vector3(0, 15, 0) * Time.deltaTime);
            }
        }
    }
}
