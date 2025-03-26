using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scratch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
