using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MeteoRock : MonoBehaviour
{
    private float interval = 0.5f;
    private float currentTime = 0f;
    private float rotateSpeed;
    private Vector3 rotateAxis;

    private void Start()
    {
        rotateSpeed = 360 / interval;
    }

    private void Update()
    {
        if (currentTime < interval)
        {
            rotateAxis = Random.insideUnitCircle.normalized;
            currentTime = interval;
        }
        
        transform.Rotate(rotateAxis, rotateSpeed * Time.deltaTime);
        
    }
}
