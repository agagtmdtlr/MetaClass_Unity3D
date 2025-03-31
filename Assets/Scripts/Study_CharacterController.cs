using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Study_CharacterController : MonoBehaviour
{
    [Header("move settings")]
    public float moveSpeed = 2.0f;
    public float jumpSpeed = 10.0f;
    public float jumpHeight = 2.0f;
    
    [Header("camera settings")]
    public Transform cameraTransform;
    public float sensitivity = 4f;
    public float maxLookAngle = 60f;
    
    float verticalRotation = 0f;
    
    private Vector3 velocity = Vector3.zero;
    private float gravity = -9.8f;
    private CharacterController characterController;
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        UpdateCamera();
        UpdateMovement();
    }

    void UpdateMovement()
    {
        float speed = moveSpeed;
        Vector2 inputAxis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector3 move =  inputAxis.x * transform.right + inputAxis.y * transform.forward;
        
        characterController.Move(move * (speed * Time.deltaTime));
        characterController.Move(velocity * Time.deltaTime);
        
        if (characterController.isGrounded && Input.GetButtonDown("Jump"))
            velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);

        if(!characterController.isGrounded)
            velocity.y += gravity * Time.deltaTime;
    }

    void UpdateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;
        
        Quaternion yawRotation = Quaternion.AngleAxis(mouseX, Vector3.up);
        transform.rotation *= yawRotation;
        
        verticalRotation += mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -maxLookAngle, maxLookAngle);
        Quaternion pitchRotation = Quaternion.Euler(Vector3.left * verticalRotation);
        cameraTransform.localRotation = pitchRotation;
        
    }
}
