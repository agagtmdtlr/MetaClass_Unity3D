using System;
using UnityEngine;

namespace SharedAssets
{
    public class FreeCam : MonoBehaviour
    {
        public float speed = 5.0f;
        public float mouseSensitivity = 2.0f;
        private float angleX;
        private float angleY;

        private const float EPSILON = 0.001f;
        
        void Update()
        {
            UpdateMovement();
            if(Input.GetKey(KeyCode.Mouse1))
                UpdateRotation();
        }
    
        void UpdateMovement()
        {
            Vector3 inputVector = new Vector3(Input.GetAxisRaw("Horizontal"),0.0f, Input.GetAxisRaw("Vertical"));
        
            inputVector = inputVector.normalized;
            Vector3 worldDirection = transform.TransformDirection(inputVector);
        
            transform.position += worldDirection * (speed * Time.deltaTime);
        }

        void UpdateRotation()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        
            angleX -= mouseY;
            angleX = Mathf.Clamp(angleX, -90f, 90f);
            angleY += mouseX;
            Quaternion xQuaternion = Quaternion.AngleAxis(angleX, Vector3.right);
            Quaternion yQuaternion = Quaternion.AngleAxis(angleY, Vector3.up);
        
            transform.rotation = yQuaternion * xQuaternion;

        
        }
    }
}
