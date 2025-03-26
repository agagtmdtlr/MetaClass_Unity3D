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
        transform.position = socketTransform.position;
        transform.localScale = socketTransform.localScale;
        transform.rotation = socketTransform.rotation;
    }

    private void OnDestroy()
    {
        s_hitColliderListOnScene.Remove(hitCollider);
    }

}
