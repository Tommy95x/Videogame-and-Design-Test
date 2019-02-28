using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputExample: MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Fire1");
        }

        if (Input.GetButtonDown("Jump")) {
            Debug.Log("Jump");
        }
	}
}
