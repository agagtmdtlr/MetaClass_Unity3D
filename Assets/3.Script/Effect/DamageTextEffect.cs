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
    private Camera camera1;

    private void Start()
    {
        camera1 = Camera.main;
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
        damageText.text = damage.ToString();
        var screenPoint= camera1.WorldToScreenPoint(effectPosition);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform.parent as RectTransform, 
            screenPoint, 
            camera1, 
            out Vector2 localPoint
        );
        
        // 로컬 좌표를 RectTransform 위치로 설정
        rectTransform.localPosition = localPoint;
        showTime += Time.deltaTime;
        if (showTime > 2f)
        {
            Release();
        }
    }
    
}
