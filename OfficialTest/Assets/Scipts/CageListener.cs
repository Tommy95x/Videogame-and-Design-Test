using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageListener : MonoBehaviour {

    private Rigidbody body;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        EventManager.StartListening("FallCage", FallCage);
    }

    private void FallCage()
    {
        body.useGravity = true;
    }
}
