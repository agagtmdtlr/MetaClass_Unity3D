using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollector
{
    public void OnRequiredItem(ItemEvent itemEvent);
}
