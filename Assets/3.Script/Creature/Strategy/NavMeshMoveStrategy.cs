

using System.Collections;using UnityEngine;
using UnityEngine.AI;

public class NavMeshMoveStrategy : IMoveStrategy
{
    private static readonly int SPEED = Animator.StringToHash("Speed");

    public IEnumerator Move(Creature creature)
    {
        while (creature.gameObject.activeInHierarchy)
        {
            var targetPos = Player.localPlayer.transform.position;
            creature.agent.SetDestination(targetPos);
            yield return null;
            
            while (creature.agent.pathPending)
                yield return null;

            while (creature.agent.hasPath)
            {
                creature.animator.SetFloat(SPEED, 1f);
                yield return null;
            }
            creature.animator.SetFloat(SPEED, 0f);

            yield return new WaitForSeconds(creature.relaxTime);
        }
    }
}
