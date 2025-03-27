using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private static readonly List<Collider> s_hitColliderListOnScene = new List<Collider>();
    public Transform socketTransform;
    public Collider hitCollider;

    struct Log
    {
        public Vector3 pos;
        public Vector3 dir;
    }
    
    List<Log> logs = new List<Log>();

    
    [System.Serializable]
    public class Skill
    {
        public string type;
        public AnimationClip clip;
        [Range(0,1)] public float enableHitBoxTime;
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
        transform.SetParent(socketTransform);
        transform.localPosition = Vector3.zero;
        transform.localRotation= Quaternion.identity;
        transform.localScale = Vector3.one;
    }

    private Log log;
    private void Update()
    {
        log =  new Log() { pos = transform.position, dir = transform.forward };
    }

    private void OnDestroy()
    {
        s_hitColliderListOnScene.Remove(hitCollider);
    }
    
   

    private void OnTriggerEnter(Collider other)
    {
        if (DummyCharacter.IsDummyCollider(other, out DummyCharacter dummyCharacter))
        {
            hitCollider.enabled = false;
            
            var prevPosition = log.pos;
            var prevDirection  = log.dir;
                
            var curPosition = transform.position;
            var curDirection = transform.forward;
                
            for (int i = 0; i <= 10; i++)
            {
                float t = (float)(i) / 10;
                var checkPosition = Vector3.Slerp(prevPosition, curPosition, t);
                var checkDirection = Vector3.Slerp(prevDirection, curDirection, t);
                
                lerpLogs.Add(new Log(){pos = checkPosition, dir = checkDirection});
                
                Ray ray = new Ray(checkPosition, checkDirection);

                if( Physics.SphereCast(ray, 0.05f , out RaycastHit hit, 10f))
                {
                    if (hit.collider.gameObject == dummyCharacter.gameObject)
                    {
                        dummyCharacter.SpawnHitEffect(hit.point, Quaternion.LookRotation(hit.normal));
                        return;
                    }
                }
            }
            
            var origin = Vector3.Slerp(prevPosition, curPosition, 0.5f);
            var point = other.ClosestPoint(origin);
            var dir =  (origin - point).normalized;
            dummyCharacter.SpawnHitEffect(point, Quaternion.LookRotation(dir));
        }
    }
    
    
    List<Log> lerpLogs = new List<Log>();
    float lastTime = 0;
    private void OnDrawGizmos()
    {
        if (Time.time - lastTime > 2f)
        {
            if (lerpLogs.Count > 0)
            {
                lerpLogs.Clear();
                lastTime = Time.time;
            }
        }

        Gizmos.color = Color.red;
        for (int i = 0; i < lerpLogs.Count; i++)
        {
            var lg = lerpLogs[i];
            Gizmos.DrawLine(lg.pos, lg.pos + (lg.dir * 10) );
        }
        
    }
}
