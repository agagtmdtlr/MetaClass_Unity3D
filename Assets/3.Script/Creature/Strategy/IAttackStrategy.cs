using System.Collections;

public interface IAttackStrategy
{
    public IEnumerator Attack(Creature creature);
}