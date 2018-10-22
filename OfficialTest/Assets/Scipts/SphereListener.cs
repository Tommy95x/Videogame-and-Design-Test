using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereListener : MonoBehaviour {


    private void OnEnable()
    {
        EventManager.StartListening("Enable", Enable);
        EventManager.StartListening("Disable", Disable);
    }

    private void Disable()
    {
        EventManager.StartListening("Disable", Disable);
        gameObject.SetActive(false);
    }

    private void Enable()
    {
        EventManager.StartListening("Enable", Enable);
        gameObject.SetActive(true);
    }


}
