using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    Animator animator;
    private int currentWeaponIndex = 0;
    public Weapon[] weapons;

    private readonly Dictionary<PlayerState.StateName, PlayerState> states 
        = new Dictionary<PlayerState.StateName, PlayerState>();

    void Start()
    {
        InitializeAnimator();
    }

    void Update()
    {
        UpdateEquip();
    }

    void UpdateEquip()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            EquipNextWeapon();
        }
    }
    
    void InitializeAnimator()
    {
        animator = GetComponent<Animator>();

        var attacks = animator.GetBehaviours<AttackBehaviour>();
        foreach (var attack in attacks)
        {
            attack.beginHitEvent += BeginHit;
            attack.endHitEvent += EndHit;
        }
        OnChangeEquip();
        
        InitializeClipObjectReference();
    }
    
    public void EquipNextWeapon()
    {
        Weapon currentWeapon = weapons[currentWeaponIndex];
        currentWeapon.gameObject.SetActive(false);
        
        currentWeaponIndex++;
        currentWeaponIndex = currentWeaponIndex % weapons.Length;
        OnChangeEquip();
    }
    
    private void OnChangeEquip()
    {
        Weapon currentWeapon = weapons[currentWeaponIndex];
        currentWeapon.gameObject.SetActive(true);
        animator.runtimeAnimatorController = currentWeapon.animatorController;
        foreach (var skill in currentWeapon.skills)
        {
            currentWeapon.animatorController[skill.type] = skill.clip;
        }
    }

    public void EndHit()
    {
        Weapon currentWeapon = weapons[currentWeaponIndex];
        currentWeapon.hitCollider.enabled = false;
    }
    
    
    
    public void BeginHit()
    {
        Weapon currentWeapon = weapons[currentWeaponIndex];
        currentWeapon.hitCollider.enabled = true;

    }

    /// <summary>
    /// clip event 호출 에러 메시지 없애기 위한 임시 이벤트 및 초기화 함수
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
