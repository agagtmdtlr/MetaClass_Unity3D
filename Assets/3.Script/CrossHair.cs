using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour
{
    [Header("CrossHair Elements")]
    public RectTransform top; 
    public RectTransform left; 
    public RectTransform right; 
    public RectTransform bottom;

    [Header("Crosshair Settings")]
    public float defaultSpread = 20f;
    public float maxSpread = 50f;
    public float spreadSpeed = 5f;
    
    float currentSpreadSpeed;

    private void Start()
    {
        currentSpreadSpeed = defaultSpread;
        UpdateCrossHairPosition();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            currentSpreadSpeed = maxSpread;
        }

        currentSpreadSpeed = Mathf.Lerp(currentSpreadSpeed, defaultSpread, Time.deltaTime * spreadSpeed);
        UpdateCrossHairPosition();

    }



    void UpdateCrossHairPosition()
    {
        top.anchoredPosition = new Vector2(0, currentSpreadSpeed);
        bottom.anchoredPosition = new Vector2(0, -currentSpreadSpeed);
        left.anchoredPosition = new Vector2(-currentSpreadSpeed,0);
        right.anchoredPosition = new Vector2(currentSpreadSpeed,0);
    }
}
