using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Breath : MonoBehaviour , IAttackable
{
    
    GridLayout gridLayout;
    public Collider Collider;
    
    private List<Collider> colliders = new List<Collider>();
    private Transform breathPoint; //위치 벡터 참조 트랜스 폼
    private Transform breathDir; // 방향벡터 참조 트랜스폼
    [SerializeField] ParticleSystem breathParticle;
    
    public void SetProperty(Transform worldPoint, Transform worldDir)
    {
        breathPoint = worldPoint;
        breathDir = worldDir;
    }

    public void ResetColliders()
    {
        colliders.Clear();
    }

    private void OnEnable()
    {
        breathParticle.Play();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = breathPoint.position;
        transform.forward =  breathDir.forward;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(colliders.Contains(other)) return;

        if (Player.localPlayer.MainCollider.Equals(other))
        {
            var hitPoint = other.ClosestPoint(transform.position);
            var hitNormal = (transform.position - hitPoint).normalized;
            
            CombatEvent combatEvent = new CombatEvent()
            {
                Sender = this,
                Receiver = Player.localPlayer,
                HitPosition = hitPoint,
                HitNormal = hitNormal,
                HitBox = Player.localPlayer.MainCollider.GetComponent<HitBox>()
            };
            CombatSystem.Instance.AddGameEvent(combatEvent);
            colliders.Add(other);
        }
        
    }

    public int Damage => 1;
}
