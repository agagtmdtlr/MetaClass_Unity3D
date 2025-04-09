using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    [Range(0, 5)]
    public float boneDistance = 2f;
    public Transform[] bones;
    void Start()
    {
        
    }

    public float speed = 5f;
    public float rotateSpeed = 5f;
    void Update()
    {
        float inputValue = Input.GetAxisRaw("Horizontal");
        transform.Translate(transform.forward * (speed * Time.deltaTime), Space.World);
        transform.Rotate(Vector3.up, (inputValue * rotateSpeed) * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            foreach (Transform bone in bones)
            {
                bone.localPosition = Vector3.back * boneDistance; 

            }
        }
    }
}
