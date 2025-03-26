using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breath : MonoBehaviour
{
    public Collider breathCollider;
    private Transform breathPoint;
    private Transform breathDirection;
    

    public void SetProperty(Transform point, Transform worldDir)
    {
        breathPoint = point;
        breathDirection = worldDir;
    }

    
    private void Update()
    {
        transform.position = breathPoint.position;
        transform.forward = breathDirection.forward;
    }


    private void OnTriggerEnter(Collider other)
    {
        CapsuleWarrior warrior = CapsuleWarrior.GetCapsuleWarrior(other);
        if (warrior != null)
        {
            warrior.ChangeHp(-1);
        }
    }
}
