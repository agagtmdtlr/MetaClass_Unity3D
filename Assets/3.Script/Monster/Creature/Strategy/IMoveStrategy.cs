using System.Collections;

public interface IMoveStrategy
{
    IEnumerator Move(Creature creature);
}

