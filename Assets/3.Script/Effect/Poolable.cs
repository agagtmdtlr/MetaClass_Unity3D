using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Poolable : MonoBehaviour
{
    public ObjectPool<Poolable> Pool;

    public void Release()
    {
        Pool.Release(this);
    }
    
}
