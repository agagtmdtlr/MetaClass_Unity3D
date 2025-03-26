using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossMonster : MonoBehaviour
{
    public static BossMonster CurrentSceneBossMonster;

    private static Collider[] s_bossMonsterColliders;
    
    public static bool IsBossMonster(Collider collider)
    {
        for (int i = 0; i < s_bossMonsterColliders.Length; i++ )
        {
            if(s_bossMonsterColliders[i] == collider) return true;
        }
        
        return false;
    }
    
    // 상태 이벤트 정보를 만들어서 상태의 특정 시점을 지날시 이벤트를 발생시키는 기능

    public enum EventType
    {
        OnIdleEnter,
        OnIdleExit,
        OnBreathEnter,
        OnBreathExit,
        OnScrathEnter,
        OnScrathExit,
    }
    
    //특정 시점이면 normalized time , name
    [System.Serializable]
    public class StateEventInfo
    {
        /// <summary>
        /// 이벤트가 등록될 Animator
        /// </summary>
        public string TargetStateName;
        /// <summary>
        /// 이벤트의 호출 시점 (0-1 normalized time)
        /// </summary>
        public float EventTime;
        
        public EventType EventType;
    }
    
    public StateEventInfo[] EventInfos;
    public List<StateEventInfo> EventInfoList { get; private set; } = new List<StateEventInfo>();
    
    static readonly int SCRATCH = Animator.StringToHash("Scratch");
    static readonly int BREATH = Animator.StringToHash("BREATH");
    
    private void Start()
    {
        breath.gameObject.SetActive(false);
        breath.SetProperty(breathPoint, breathPoint);
        
        CurrentSceneBossMonster = this;
        
        s_bossMonsterColliders = GetComponentsInChildren<Collider>();
    }

    private Animator animator;
    private AnimatorStateInfo previousState;

    private readonly int[] bossAttacks = { SCRATCH, BREATH };

    [Header("Breath Ref")]
    public Breath breath;
    public Transform breathPoint;
    
    [Header("Scratch Ref")]
    public Scratch scratch;
    public Transform scratchPoint;
    
    void HandleEvent(StateEventInfo eventInfo)
    {
        switch (eventInfo.EventType)
        {
            case EventType.OnIdleEnter:
                int nextAttackTrigger = Random.Range(0, bossAttacks.Length);
                animator.SetTrigger(nextAttackTrigger);
                break;
            case EventType.OnIdleExit:
                break;
            case EventType.OnBreathEnter:
                breath.ResetCollider();
                breath.gameObject.SetActive(true);
                break;
            case EventType.OnBreathExit:
                breath.gameObject.SetActive(false);
                break;
            case EventType.OnScrathEnter:
                scratch.gameObject.SetActive(true);
                break;
            case EventType.OnScrathExit:
                scratch.gameObject.SetActive(false);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    private void Update()
    {
        AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);
        if (previousState.shortNameHash != currentState.shortNameHash)
        {
            for (int i = 0; i < EventInfos.Length; i++)
            {
                if (currentState.IsName(EventInfos[i].TargetStateName))
                {
                    EventInfoList.Add(EventInfos[i]);
                }
            }
        }
        
        for (int i = EventInfoList.Count - 1; i >= 0; i++)
        {
            if (EventInfoList[i].EventTime > currentState.normalizedTime)
            {
                HandleEvent(EventInfoList[i]);
                EventInfoList.RemoveAt(i);
            }
        }
        
        previousState = currentState;
    }

    private int currentHp = 10;
    private int currentHitCount  = 0;
    private const int HITCOUNT = 3;
    public void ChangeHp(int hp)
    {
        currentHp += hp;
        if (currentHp <= 0)
        {
            animator.SetTrigger("Death");
        }
        else
        {
            currentHitCount++;
            if (currentHitCount >= HITCOUNT)
            {
                animator.SetTrigger("Hit");
                currentHitCount = 0;
            }

        }
    }
}
