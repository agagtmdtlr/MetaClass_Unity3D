using Unity.VisualScripting;
using UnityEngine;

public class AnimStateEventSender : StateMachineBehaviour
{
    private AnimStateEventListener listener;

    [System.Serializable]
    public class AnimStateEvent
    {
        public string name;
        public string parameter;
        
        [Range(0f, 1f)]
        public float time;
        [HideInInspector] public bool triggered;
    }
    [SerializeField] AnimStateEvent[] events;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.TryGetComponent(out listener) == false)
            Debug.LogWarning("AnimStateEventSender: animator component not found");
        
        foreach (var evt in events)
            evt.triggered = false;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var curTime =stateInfo.normalizedTime;
        
        foreach (var evt in events)
        {
            if(evt.triggered)
                continue;
            
            if(evt.time > curTime)
                continue;
            
            evt.triggered = true;
            listener.CallEvent(evt.name, evt.parameter);
        }
    }
}
