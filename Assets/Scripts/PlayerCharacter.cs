using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    Animator animator;
    private int currentWeaponIndex = 0;
    [SerializeField] Weapon[] weapons;

    private readonly Dictionary<PlayerState.StateName, PlayerState> statesDic 
        = new Dictionary<PlayerState.StateName, PlayerState>();

    PlayerState currentState;
    public Weapon CurrentWeapon => weapons[currentWeaponIndex];
    
    void Start()
    {
        PlayerState[] states = GetComponentsInChildren<PlayerState>(true);
        foreach (PlayerState state in states)
        {
            statesDic[state.stateName] = state;
            state.Initialize(this);
        }
        
        ChangestState(PlayerState.StateName.Move);
        InitializeAnimator();
    }

    public void ChangestState(PlayerState.StateName newState)
    {
        if (currentState != null)
        {
            currentState.gameObject.SetActive(false);
        }
        
        currentState = statesDic[newState];
        currentState.gameObject.SetActive(true);
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

    private Dictionary<string, AttackBehaviour> attackBehaviours = new Dictionary<string, AttackBehaviour>();
    
    void InitializeAnimator()
    {
        animator = GetComponent<Animator>();

        var attacks = animator.GetBehaviours<AttackBehaviour>();
        foreach (var attack in attacks)
        {
            attack.beginHitEvent += BeginHit;
            attack.endHitEvent += EndHit;
            attackBehaviours[attack.Name] = attack;
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
            attackBehaviours[skill.type].enableHitBoxTime = skill.enableHitBoxTime;
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
