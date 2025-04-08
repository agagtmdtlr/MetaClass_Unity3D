
using System.Collections;

public class StaticMoveStrategy : IMoveStrategy
{
    public IEnumerator Move(Creature creature)
    {
        while (creature.gameObject.activeInHierarchy)
        {
            yield return null;
        }
    }
}