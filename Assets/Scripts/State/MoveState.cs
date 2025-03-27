using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : PlayerState
{
    public override StateName stateName => StateName.Move;
    public float Speed = 5.0f;
    void Update()
    {
        Vector2 axisInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        float inputSpeed = 1f;
        if (Input.GetKey(KeyCode.LeftShift))
            inputSpeed = 2.0f;
        
        axisInput *= inputSpeed;
        transform.Translate(new Vector3(axisInput.x, 0, axisInput.y) * (Time.deltaTime * Speed) );
    }

}
