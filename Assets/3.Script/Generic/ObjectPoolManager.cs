using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    [System.Serializable]
    public class ObjectPoolData
    {
        public string Key;
        public Transform Parent;
        public GameObject Prefab;
        public byte ExpandSize;
    }
    
    [Header("Object Pool Data")]
    public ObjectPoolData[] objectPoolDatas;
    
    public static ObjectPoolManager Instance;
    private Dictionary<string, ObjectPool> poolDict = new Dictionary<string, ObjectPool>();
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        foreach (var objPoolData in objectPoolDatas)
        {
            CreatePool(objPoolData);
        }
    }

    public void CreatePool(ObjectPoolData data)
    {
        if (poolDict.ContainsKey(data.Key))
        {
            Debug.LogWarning($"Pool with key {data.Key} already exists.");
            return;
        }
        
        var poolItem = data.Prefab.GetComponent<IObjectPoolItem>();
        if (poolItem == null)
        {
            Debug.LogWarning($"Pool with key {data.Key} doesn't have a IObjectPoolItem component {data.Prefab.name}.");
            return;
        }
        
        var pool = new ObjectPool();
        pool.Initialize(data.Parent, poolItem,  data.Key, data.ExpandSize);   
        poolDict.Add(data.Key, pool);
    }


    public IObjectPoolItem GetObjectOrNull(string key)
    {
        if (poolDict.TryGetValue(key, out var pool))
        {
            var obj = pool.Get();
            obj.GameObject.SetActive(true);
            return obj;
        }
        else
        {
            Debug.LogWarning($"Pool with key {key} not found.");
        }
        return null;
    }

    public void ReturnToPool(IObjectPoolItem item)
    {
        if (poolDict.TryGetValue(item.Key, out var pool))
        {
            pool.Return(item);
        }
        else
        {
            Debug.LogWarning($"Pool with key {item.Key} not found.");
        }
    }
    
}
