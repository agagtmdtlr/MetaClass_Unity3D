using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FollowUI : MonoBehaviour , IObjectPoolItem
{
    public string Key {get; set;}
    
    public GameObject GameObject => gameObject;

    private Camera uiCamera;
    private Canvas uiCanvas;
    
    private Transform target;
    private TMP_Text text;
    private float duration;
    
    private RectTransform rectTransform;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
        if(text is null)
            Debug.LogWarning("FollowUI : TMP_Text 컴포넌트가 없습니다.");
        
        rectTransform = GetComponent<RectTransform>();
        if(rectTransform is null)
            Debug.LogWarning("FollowUI : RectTransform 컴포넌트가 없습니다.");
        
        uiCamera = Camera.main;
        uiCanvas = GetComponentInParent<Canvas>(true);
        
        if(uiCanvas is null)
            Debug.LogWarning("FollowUI : Canvas 컴포넌트가 없습니다.");
    }
    
    private void Update()
    {
        if (duration < 0)
        {
            ReturnToPool();
            return;
        }
        
        duration -= Time.deltaTime;

        Vector3 screenPos = uiCamera.WorldToScreenPoint(target.transform.position);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            uiCanvas.transform as RectTransform, 
            screenPos, 
            uiCamera, 
            out Vector2 localPoint
            );
        
        rectTransform.anchoredPosition = localPoint;
    }
    
    public void Set(Transform target, string content, float duration, Color color)
    {
        this.target = target;
        
        text.text = content;
        text.color = color;
        text.outlineColor = color;
        
        this.duration = duration;
    }
    
    public void ReturnToPool()
    {
        ObjectPoolManager.Instance.ReturnToPool(this);
    }
}
