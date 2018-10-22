using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverListener : MonoBehaviour {

    private void OnEnable()
    {
        EventManager.StartListening("InputLever", InputLever);
    }

    private void InputLever()
    {
        gameObject.SetActive(false);
    }
}
