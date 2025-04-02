using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class DamageTextEffectPool : MonoBehaviour
{
    public Canvas effectCanvas;
    public GameObject effectPrefab;

    private ObjectPool<Poolable> effectPool;

    void Start()
    {
        effectPool = new ObjectPool<Poolable>(
            CreateEffect,
            OnGetEffect,
            OnReleaseEffect,
            OnDestroyEffect,
            false, 10, 100);

        CombatSystem.Instance.Events.OnCombatEvent += ShowDamageText;
    }

    private void OnDestroyEffect(Poolable obj) => Destroy(obj.gameObject);

    private void OnReleaseEffect(Poolable obj) => obj.gameObject.SetActive(false);

    private void OnGetEffect(Poolable obj) => obj.gameObject.SetActive(true);

    private Poolable CreateEffect()
    {
        Poolable effect = Instantiate(effectPrefab, Vector3.zero, Quaternion.identity).GetComponent<Poolable>();
        effect.Pool = effectPool;
        return effect;
    }

    public void ShowDamageText(CombatEvent combatEvent)
    {
        Poolable obj = effectPool.Get();
        DamageTextEffect txtEffect = obj as DamageTextEffect;
        if (txtEffect is not null)
        {
            Debug.Log("show damage text effect");
            
            txtEffect.transform.SetParent(effectCanvas.transform);

            txtEffect.transform.localRotation = Quaternion.identity;
            txtEffect.transform.localScale = Vector3.one;

            txtEffect.damage = combatEvent.Damage;
            txtEffect.effectPosition = combatEvent.HitPosition;
        }
        else
        {
            Debug.LogWarning("Invalid effect type");
        }
    }
}