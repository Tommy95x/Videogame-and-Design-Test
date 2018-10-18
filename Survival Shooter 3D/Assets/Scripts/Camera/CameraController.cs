 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    [Header("Target that our camera follows")]
    public Transform target;
    [Header("Speed of movement of camera")]
    public float smoothing;

    private Vector3 offset;

    private void Start()
    {
        offset = transform.position - target.position;
    }

    private void FixedUpdate()
    {
        Vector3 targetCamPos = target.position + offset;
        //We use Vector.Lerp like movement because is a soft movement our camera will do without create some bad fealing
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);

    }

}
