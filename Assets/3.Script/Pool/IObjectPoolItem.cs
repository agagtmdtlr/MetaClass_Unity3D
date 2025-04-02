using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObjectPoolItem
{
    string Key { get; set; }
    GameObject GameObject { get; }
    
    void ReturnToPool();
}



public class ObjectPool
{
    private Stack<IObjectPoolItem> Pool { get; set; }
    private Transform Parent { get; set; }
    private IObjectPoolItem Sample { get; set; }
    private byte ExpandSize { get; set; }
    
    
    /// <summary>
    /// 오브젝트 풀 초기화 함수
    /// </summary>
    /// <param name="parent"> 풀 아이템을 포함할 부모 오브젝트</param>
    /// <param name="sample"> 프리팹이 전달됨 풀 아이템 생성 시 템플릿 객체</param>
    /// <param name="expandSize">아이템 부족 시 풀 확장 사이즈</param>
    /// <param name="key">풀 아이템 참조 키</param>
    public void Initialize(Transform parent, IObjectPoolItem sample, string key, byte expandSize)
    {
        Parent = parent;
        Pool = new Stack<IObjectPoolItem>();
        Sample = sample;
        Sample.GameObject.SetActive(false);
        Sample.Key = key;
        ExpandSize = expandSize;
    }

    private void Expand()
    {
        for (int i = 0; i < ExpandSize; i++)
        {
            var instance = GameObject.Instantiate(Sample.GameObject, Parent).GetComponent<IObjectPoolItem>();
            instance.Key = Sample.Key;
            Return(instance);
        }
    }

    public IObjectPoolItem Get()
    {
        if (Pool.Count == 0)
        {
            Expand();
        }
        
        return Pool.Pop();
    }

    public void Return(IObjectPoolItem item)
    {
        item.GameObject.SetActive(false);
        Pool.Push(item);
        
    }
}
