using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BossMonster : MonoBehaviour
{
    private static readonly int HIT = Animator.StringToHash("Hit");
    private static readonly int DEAD = Animator.StringToHash("Dead");

    [System.Serializable]
    public class Weapon
    {
        public AttackType type;
        public Collider[] colliders;
        public ParticleSystem[] effects;
    }
    
    [Header("HP UI References")]
    public Slider bossHpSlider;
    [Header("Weapon References")]
    public Weapon[] weapons;
    
    private readonly Dictionary<AttackType, Weapon> weaponPerType = new Dictionary<AttackType, Weapon>();
    private AttackType currentAttack;
    public AttackType CurrentAttack => currentAttack;
    private bool isAttacking = true;

    private readonly int maxHp = 10;
    private int hp = 10;
    private int hitCount = 0;
    private bool isDead = false;

    public enum AttackType
    {
        Scratch,
        Breath,
        UpDown,
        TwoHand
    }

    Animator animator;
    private readonly List<GameObject> hittedPlayers = new List<GameObject>();
    void Start()
    {
           animator = GetComponent<Animator>();

           foreach (var weapon in weapons)
           {
               weaponPerType[weapon.type] = weapon;
               
               foreach (var weaponCollider in weapon.colliders)
                   weaponCollider.enabled = false;
               
               foreach(var effect in weapon.effects)
                   effect.Stop();
           }

           bossHpSlider.value = 1;
    }

    void Update()
    {
        if (isDead) return;
    
        UpdateAttack();
    }

    void UpdateAttack()
    {
        if (isAttacking)
        {
        }
        else
        {
            if (animator.IsInTransition(0)) return;

            AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
            bool attackValid = info.IsName("Idle");
            if (!attackValid) return;

            int attackCount = System.Enum.GetValues(typeof(AttackType)).Length;

            AttackType nextAttack = (AttackType)Random.Range(0, attackCount);

            StartAttack(nextAttack);
        }
    }

    public void OnEnableAttackWeapon()
    {
        var weapon = weaponPerType[currentAttack];
        foreach (var weaponCollider in weapon.colliders)
            weaponCollider.enabled = true;
    }

    public void PlayWeaponEffect()
    {
        var weapon = weaponPerType[currentAttack];
        foreach(var effect in weapon.effects)
            effect.Play();
    }

    void StartAttack(AttackType attackType)
    {
        currentAttack = attackType;
        Debug.Log($"Start Attack = {currentAttack.ToString()}");
        isAttacking = true;
        hittedPlayers.Clear();
        animator.SetTrigger(currentAttack.ToString());
    }

    public void EndAttack()
    {
        Debug.Log($"End Attack = {currentAttack.ToString()}");
        isAttacking = false;
        var weapon = weaponPerType[currentAttack];
        
        foreach (var weaponCollider in weapon.colliders)
            weaponCollider.enabled = false;
        
        foreach(var effect in weapon.effects)
            effect.Stop();

    }
    
    public void TakeDamage(int damage)
    {
        if(isDead)
            return;
        
        hitCount += 1;
        hp -= damage;
        bossHpSlider.value = hp / (float)maxHp;
        
        if (hp <= 0)
        {
            OnDeath();
        }
        else if (hitCount >= 3)
        {
            OnHit();
        }
    }

    void OnHit()
    {
        EndAttack();
        hitCount = 0;
        animator.SetTrigger(HIT);
    }

    void OnDeath()
    {
        EndAttack();
        animator.SetTrigger(DEAD);
        isDead = true;
    }


    public void AddHitPlayer(GameObject player)
    {
        hittedPlayers.Add(player); 
    }

    public bool WasHittedPlayer(GameObject player)
    {
        return hittedPlayers.Contains(player);
    }

}
