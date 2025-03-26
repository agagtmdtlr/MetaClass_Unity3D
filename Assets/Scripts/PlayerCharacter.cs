using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public static PlayerCharacter CurrentPlayerCharacter;

    public Transform weaponSocket;
    private bool equipped = false;
    
    private static readonly int ATTACK = Animator.StringToHash("Attack");
    public float Speed = 5.0f;
    Animator animator;

    private int currentWeaponIndex = 0;
    public Weapon[] weapons;

    void Start()
    {
        CurrentPlayerCharacter = this;
        animator = GetComponent<Animator>();
        OnChangeEquip();

        InitializeClipObjectReference();
    }
    
    void Update()
    {
        UpdateEquipInput();
    }

    private void UpdateEquipInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            equipped  = !equipped;
            OnChangeEquip();
        }
    }
    
    private void OnChangeEquip()
    {
        Weapon currentWeapon = weapons[currentWeaponIndex];
        animator.runtimeAnimatorController = currentWeapon.animatorController;
        foreach (var skill in currentWeapon.skills)
        {
            currentWeapon.animatorController[skill.type] = skill.clip;
        }
    }


    public void EndHit()
    {
        AnimatorStateInfo currentAnimStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        Weapon currentWeapon = weapons[currentWeaponIndex];
        currentWeapon.hitCollider.enabled = true;
    }
    
    public void BeginHit()
    {
        AnimatorStateInfo currentAnimStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        Weapon currentWeapon = weapons[currentWeaponIndex];
        currentWeapon.hitCollider.enabled = false;
    }

    /// <summary>
    /// clip event 호출 에러 메시지 없애기 위한 이벤트 및 초기화 함수
    /// </summary>
    public void Hit()
    {
    }
    
    public void FootR()
    {
        
    }

    public void FootL()
    {
        
    }
    
   
    void InitializeClipObjectReference()
    {
        foreach (var weapon in weapons)
        {
            var clips = weapon.animatorController.animationClips;
            foreach (var clip in clips)
            {
                var events = clip.events;
                foreach (var evt in events)
                {
                    evt.objectReferenceParameter = this;
                }
            }
        }
    }
}
