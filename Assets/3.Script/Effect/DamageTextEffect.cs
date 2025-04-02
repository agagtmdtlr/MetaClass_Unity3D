using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;

public class DamageTextEffect : Poolable
{
    public Vector3 effectPosition;
    public int damage;

    private RectTransform rectTransform;
    private TMP_Text damageText;
    
    private float showTime = 0f;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
        damageText = gameObject.GetComponentInChildren<TMP_Text>();
        damageText.text = damage.ToString();
        
        showTime = 0f;
    }

    
    private void Update()
    {
        UpdateDamageText();
        UpdatePosition();

        showTime += Time.deltaTime;
        if (showTime > 2f)
        {
            Release();
        }
    }

    private void UpdateDamageText()
    {
        damageText.text = damage.ToString();
    }

    private void UpdatePosition()
    {
        var screenPoint = mainCamera.WorldToScreenPoint(effectPosition);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform.parent as RectTransform,
            screenPoint,
            mainCamera,
            out Vector2 localPoint
        );

        rectTransform.localPosition = localPoint;
    }
    
}
