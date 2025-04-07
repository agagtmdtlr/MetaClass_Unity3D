using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Obstacle : MonoBehaviour
{
    HitBox hitBox;
    // Start is called before the first frame update
    void Start()
    {
        hitBox = GetComponent<HitBox>();
    }

    public GameObject GameObject => gameObject;
    public void TakeDamage(CombatEvent combatEvent)
    {
    }
}
