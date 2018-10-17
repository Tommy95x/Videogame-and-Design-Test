using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour {

    private Rigidbody rigidbody;
    private BoxCollider collider;

    public AudioSource audioBoxCrush;


	// Use this for initialization
	private void Awake () {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<BoxCollider>();
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            Debug.Log(collision.transform.tag);
            audioBoxCrush.enabled = true;
            audioBoxCrush.Play();
        }
    }

}
