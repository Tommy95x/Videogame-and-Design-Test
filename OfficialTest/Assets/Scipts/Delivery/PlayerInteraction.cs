using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour {

    [Header("ObjectReductionSpeed")]
    public float metalPercentageReduction;

    private Animator anim;
    private PlayerMovements movements;
    private bool sphereDetect;
    private bool leverDetect;
    private bool boxDetect;
    private bool keyDetect;
    private bool buttonDetect;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        movements = GetComponent<PlayerMovements>();
    }

    private void Update()
    {
        if (sphereDetect && Input.GetKeyDown(KeyCode.U))
        {
            //Decide a common name for the event
            EventManager.TriggerEvent("");
        }
        else if (leverDetect && Input.GetKeyDown(KeyCode.I))
        {
            //Decide a common name for the event
            EventManager.TriggerEvent("InputLever");
            EventManager.TriggerEvent("StartMovementElevator");
            EventManager.TriggerEvent("FallCage");
        }else if(keyDetect && Input.GetKeyDown(KeyCode.U))
        {
            //Decide a common name for the event
            EventManager.TriggerEvent("");
        }
        else if (Input.GetKeyUp(KeyCode.U))
        {
            //Common event to release some object
            EventManager.TriggerEvent("Release");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Lever"))
        {
            //Thing how to do the animation
            leverDetect = true;
            Debug.Log("LeverDetect");
        }
        else if (other.CompareTag("Sphere"))
        {
            //Thing how to do the animation
            sphereDetect = true;
            Debug.Log("SphereDetect");
        }
        else if (other.CompareTag("Key"))
        {
            //Thing how to do the animation
            keyDetect = true;
            Debug.Log("KeyDetect");
        }else if(other.gameObject.CompareTag("WoodBox"))
        {
            //Thing how to do the animation
            movements.setIsPushing(true);
            Debug.Log("WoodBoxDetect");
        }
        else if (other.gameObject.CompareTag("MetalBox"))
        {
            movements.setIsPushing(true);
            movements.changeSpeed(metalPercentageReduction);
            Debug.Log("MetalBoxDetect");
        }else if (other.gameObject.CompareTag("Button"))
        {
            //Event that declare that the player is presented on the button/platform
            EventManager.TriggerEvent("ButtonDown");
        }else if (other.gameObject.CompareTag("DDouble"))
        {
            EventManager.TriggerEvent("NewPlayer");
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Sphere"))
        {
            sphereDetect = false;
            Debug.Log("SphereOut");
        }else if (other.CompareTag("Key"))
        {
            keyDetect = false;
            Debug.Log("KeyOut");
        }
        else if (other.CompareTag("Lever"))
        {
            leverDetect = false;
            Debug.Log("LeverOut");
        }else if (other.CompareTag("Button"))
        {
            //Possible event that lifts the button/platform back
        }
    }

}
