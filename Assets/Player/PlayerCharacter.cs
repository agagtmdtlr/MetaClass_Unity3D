using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [System.Serializable]
    public class Skill
    {
        public string type;
        public AnimationClip clip;
        public Collider[] colliders;
    }
    
    [System.Serializable]
    public class PlayerState
    {
        [Header("Player Animator Controller")]
        public AnimatorOverrideController animatorController;

        [Header("Player SkillSet")]
        public Skill[] skills;
    }
    
    public GameObject weapon;
    private bool equipped = false;
    
    [Header("Player Un Equipped State ")]
    public PlayerState unEquippedState;
    
    [Header("Player Un Equipped Weapon State ")]
    public PlayerState equippedState;
    
    private PlayerState currentState;
    private static readonly int ATTACK = Animator.StringToHash("Attack");
    public float Speed = 5.0f;
    Animator animator;
    

    public void Hit()
    {
        AnimatorStateInfo currentAnimStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        foreach (var skill in currentState.skills)
        {
            bool isCurrentAttack =currentAnimStateInfo.IsName(skill.type);
            // 현재 skill에 해당하는 콜라이더를 켜준다.
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        OnChangeEquip();
    }

    void Update()
    {
        Move();
        UpdateAttackInput();
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
        if (equipped)
        {
            currentState = equippedState;
            weapon.SetActive(true);
            // 검 장착
        }
        else
        {
            currentState = unEquippedState;
            weapon.SetActive(false);
            // 주먹 장착
        }
        
        animator.runtimeAnimatorController = currentState.animatorController;
        foreach (var skill in currentState.skills)
        {
            currentState.animatorController[skill.type] = skill.clip;
        }
    }

    private void UpdateAttackInput()
    {
        // 목표는 3콤보 주먹질
        // 정확하게 입력에 따라 동작해야 한다.
        AnimatorStateInfo currentAnimStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        
        if (animator.IsInTransition(0)) return;
        
        bool isAttack1 = currentAnimStateInfo.IsName("Attack-1");
        bool isAttack2 = currentAnimStateInfo.IsName("Attack-2");
        bool isAttack3 = currentAnimStateInfo.IsName("Attack-3");
        bool isAttacking = isAttack1 || isAttack2 || isAttack3;

        bool inputAttack = Input.GetKeyDown(KeyCode.Mouse0);
        
        // 세번째는 입력을 받지 않는다.
        if (inputAttack)
        {
            float normalizeTime = currentAnimStateInfo.normalizedTime;
            
            if (isAttacking == false) // 첫번째 공격 진입
            {
                animator.SetTrigger(ATTACK);

                animator.GetCurrentAnimatorClipInfo(0);
                
                animator.Play("Attack-1",0,0.0f);
                
            }
            else
            {
                if (isAttack3) return;
                
                if(0.4f < normalizeTime && normalizeTime < 0.85f) // 두번째 부터 입력 판정
                    animator.SetTrigger(ATTACK);
            }
        }
        else
        {
            
        }
    }

    private void Move()
    {
        Vector2 axisInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        
        float InputSpeed = 1f;
        Input.GetKey(KeyCode.LeftShift); 
        if (Input.GetKey(KeyCode.LeftShift))
            InputSpeed = 2.0f;

        axisInput *= InputSpeed;
        
        animator.SetFloat("MoveZ", axisInput.y);
        animator.SetFloat("MoveX", axisInput.x);
        animator.SetFloat("Speed", InputSpeed);

        transform.Translate(new Vector3(axisInput.x, 0, axisInput.y) * (Time.deltaTime * Speed) );
    }
}
