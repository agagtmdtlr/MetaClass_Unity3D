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

    // 레이어 별로 state 관리하기
    private Dictionary<PlayerState.StateName, PlayerState>[] stateLayers =
        new Dictionary<PlayerState.StateName, PlayerState>[2];
    
    PlayerState[] currentStateInLayer = new PlayerState[2];
    
    
    public Weapon CurrentWeapon => weapons[currentWeaponIndex];
    
    void Start()
    {
        for (int i = 0; i < stateLayers.Length; i++)
        {
            stateLayers[i] = new Dictionary<PlayerState.StateName, PlayerState>();
        }
        
        PlayerState[] states = GetComponentsInChildren<PlayerState>(true);
        foreach (PlayerState state in states)
        {
            stateLayers[state.stateLayerIndex][state.stateName] = state;
            state.Initialize(this);
        }
        
        ChangestState(PlayerState.StateName.Move, 0);
        ChangestState(PlayerState.StateName.ReadyAttack, 1);
        
        InitializeAnimator();
    }

    public void ChangestState(PlayerState.StateName newState, int layerIndex)
    {
        var beforeState = currentStateInLayer[layerIndex];   
        if (beforeState is not null)
        {
            beforeState.gameObject.SetActive(false);
            beforeState.ExitState();
        }
        
        var nextState = stateLayers[layerIndex][newState];
        currentStateInLayer[layerIndex] = nextState;
        nextState.gameObject.SetActive(true);
        nextState.EnterState();
    }
    
    void Update()
    {
        UpdateEquip();
        Ray ray = new Ray(transform.position, Vector3.down);
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
        OnChangeEquip();
        
        InitializeClipObjectReference();
    }

    private void EquipNextWeapon()
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
