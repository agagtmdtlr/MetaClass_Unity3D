using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private static readonly List<Collider> s_hitColliderListOnScene = new List<Collider>();
    public Transform socketTransform;
    public Collider hitCollider;

    public Vector3 prevPosition;
    public Vector3 prevDirection;
    
    [System.Serializable]
    public class Skill
    {
        public string type;
        public AnimationClip clip;
    }
    
    [Header("Player Animator Controller")]
    public AnimatorOverrideController animatorController;

    [Header("Player SkillSet")]
    public Skill[] skills;

    public static bool IsWeaponCollider(Collider collider)
    {
        foreach (var hitCollider in s_hitColliderListOnScene)
        {
            if (hitCollider == collider) return true;
        }
        
        return false;
    }
    
    void Start()
    {
        s_hitColliderListOnScene.Add(hitCollider);
    }

    private void Update()
    {
        prevPosition = socketTransform.position;
        prevDirection = socketTransform.forward;
        
        transform.position = socketTransform.position;
        transform.localScale = socketTransform.localScale;
        transform.rotation = socketTransform.rotation;
    }

    private void OnDestroy()
    {
        s_hitColliderListOnScene.Remove(hitCollider);
    }
    
    List<Vector3> lerpPosition = new List<Vector3>();
    List<Vector3> lerpDirection = new List<Vector3>();
    float lastTime = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (DummyCharacter.IsDummyCollider(other, out DummyCharacter dummyCharacter))
        {
            var curPosition = socketTransform.position;
            var curDirection = socketTransform.forward;

            Debug.Log($"{prevPosition} {curPosition}");
            
            for (int i = 0; i < 10; i++)
            {
                float t = (float)(i) / 10;
                var checkPosition = Vector3.Slerp(prevPosition, curPosition, t);
                var checkDirection = Vector3.Slerp(prevDirection, curDirection, t);
                
                lerpPosition.Add(checkPosition);
                lerpDirection.Add(checkDirection);

                if (Physics.Raycast(checkPosition, checkDirection, out RaycastHit hit, 10))
                {
                    dummyCharacter.SpawnHitEffect(hit.point, Quaternion.LookRotation(hit.normal));
                    break;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (Time.time - lastTime > 2f)
        {
            if (lerpPosition.Count > 0)
            {
                lerpPosition.RemoveAt(0);
                lerpDirection.RemoveAt(0);
                lastTime = Time.time;
            }
        }

        Gizmos.color = Color.red;
        for (int i = 0; i < lerpPosition.Count; i++)
        {
            Gizmos.DrawLine(lerpPosition[i], lerpPosition[i] + (lerpDirection[i] * 10) );
        }
        
    }
}
