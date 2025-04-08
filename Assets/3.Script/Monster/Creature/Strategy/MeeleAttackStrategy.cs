using System.Collections;
using UnityEditor;
using UnityEngine;

public class MeeleAttackStrategy : IAttackStrategy
{
    private static readonly int ATTACK_INDEX = Animator.StringToHash("AttackIndex");
    private static readonly int ATTACK = Animator.StringToHash("Attack");
    public IEnumerator Attack(Creature creature)
    {
        while (creature.gameObject.activeInHierarchy)
        {
            int attackIndex = Random.Range(0, 2);
            creature.animator.SetInteger(ATTACK_INDEX, attackIndex);
            creature.animator.ResetTrigger(ATTACK);
            creature.animator.SetTrigger(ATTACK);

            if (creature.agent is not null)
            {
                creature.agent.isStopped = true;
            }

            creature.isAttacking = true;
            while (creature.isAttacking)
            {
                yield return null;
            }
            
            if (creature.agent is not null)
            {
                creature.agent.isStopped = false;
            }
        }
    }
}