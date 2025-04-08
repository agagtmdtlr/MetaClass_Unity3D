using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimStateController : MonoBehaviour
{
    [Header("카메라 설정")]
    public Transform cameraTransform;
    public float mouseSensitivity = 2f;
    public float maxLookAngle = 80f;

    private float verticalLookRotation = 0f;
    private Quaternion initialCameraRotation;
    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        initialCameraRotation = cameraTransform.localRotation;
        
        CombatSystem.Instance.Events.OnDeathEvent += onEndedGame;
    }

    private void onEndedGame(DeathEvent deathEvent)
    {
        if (deathEvent.Dead.DamageableType == typeof(LocalPlayer)
            || deathEvent.Dead.DamageableType == typeof(BossMonster))
        {
            enabled = false;
        }
    }
    

    private void Update()
    {
        UpdateCameraRotation();
    }

    private void UpdateCameraRotation()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity;

        Quaternion yawRotation = Quaternion.AngleAxis(mouseX, Vector3.up);
        transform.rotation *= yawRotation;

        verticalLookRotation -= mouseY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -maxLookAngle, maxLookAngle);

        Quaternion pitchRotation = Quaternion.AngleAxis(verticalLookRotation, Vector3.right);
        cameraTransform.localRotation = initialCameraRotation * pitchRotation;
    }

}
