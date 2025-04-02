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
    
    private EffectTarget target;
    private TMP_Text text;
    private float duration;
    
    private RectTransform rectTransform;

    public bool isInitialize = false;
    
    public void Initialize(Camera camera, Canvas canvas)
    {
        if (isInitialize) return;
        isInitialize = true;
        
        text = GetComponent<TMP_Text>();
        if(text is null)
            Debug.LogWarning("FollowUI : TMP_Text 컴포넌트가 없습니다.");
        
        rectTransform = GetComponent<RectTransform>();
        if(rectTransform is null)
            Debug.LogWarning("FollowUI : RectTransform 컴포넌트가 없습니다.");
        
        this.uiCamera = camera;
        this.uiCanvas = canvas;
    }
    
    private void Update()
    {
       
        // 사용 종료
        if (duration < 0)
        {
            ReturnToPool();
            return;
        }
        
        duration -= Time.deltaTime;

        // 3d -> screen 2d
        Vector3 screenPos = uiCamera.WorldToScreenPoint(target.transform.position);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            uiCanvas.transform as RectTransform, 
            screenPos, 
            uiCamera, 
            out Vector2 localPoint
            );
        
        rectTransform.anchoredPosition = localPoint;
    }
    

    // 텍스트 출력
    // 스스로 사용종료 처리를 하는 것
    // 풀로 되돌아 가는것
    // 3차원 좌표계의 오브젝트(Transform)을 참조해서 실시ㅏ능로
    // 2차원 자표계 (Canvas, Camera)로 위치 좌표를 변환하는 기능


    public void Set(EffectTarget target, string content, float duration, Color color)
    {
        this.target = target;
        
        text.text = content;
        text.color = color;
        text.outlineColor = color;
        
        this.duration = duration;
    }
    
    public void ReturnToPool()
    {

        target.ReturnToPool();
        ObjectPoolManager.Instance.ReturnToPool(this);
    }
}
