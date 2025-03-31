using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCam : MonoBehaviour
{
    public float Speed = 5.0f;
    public float MouseSensitivity = 10.0f;
    
    private float angleX;
    private float angleY;
    
    public Transform wayPoint;
    public Study_NaviAgent agent;
    public Dynamic_Ptrol patrol;

    //오늘의 퀴즈
    // 경로 입력
    // 몇번출력 할지 모름
    // 출력을 다 끝낸후 스페이스로 주행
    private List<Vector3> wayPoints = new List<Vector3>();
    
    
    // Start is called before the first frame update
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        angleX = transform.eulerAngles.x;
        angleY = transform.eulerAngles.y;
    }
    

    // Update is called once per frame
    private void Update()
    {
        UpdateMovement();
        
        //Mouse 0 = Left Click
        //Mouse 1 = Right Click
        //Mouse 2 = Wheel Click

        if (Input.GetKey(KeyCode.Mouse1))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            UpdateRotation();
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100.0f, LayerMask.GetMask("Ground")))
            {
                wayPoint.position = hit.point;
                
                wayPoints.Add(hit.point);
                //agent.SetDestination(hit.point);
            }
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            agent.SetDestinations(wayPoints);
            wayPoints.Clear();
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            patrol.SetWaypoint(wayPoints.ToArray());
            wayPoints.Clear();
        }
    }
    
    private void UpdateMovement()
    {
        Vector3 inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));
        inputVector = inputVector.normalized;
        
        Vector3 worldDirection = transform.TransformDirection(inputVector);
        transform.position += worldDirection * (Speed * Time.deltaTime);

        Vector3 upDir = Vector3.zero;
        if (Input.GetKey(KeyCode.Q))
        {
            upDir = -transform.up;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            upDir = transform.up;

        }
        transform.position += upDir * (Speed * Time.deltaTime);
    }

    private void UpdateRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * MouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity;

        angleX -= mouseY;
        angleX = Mathf.Clamp(angleX, -90f, 90f);
        
        angleY += mouseX;
        
        Quaternion xQuaternion = 
            Quaternion.AngleAxis(angleX, Vector3.right);
        Quaternion yQuaternion = 
            Quaternion.AngleAxis(angleY, Vector3.up);
        
        //서순 중요
        transform.rotation = yQuaternion * xQuaternion;
    }
}