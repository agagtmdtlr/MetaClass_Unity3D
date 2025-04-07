

using UnityEngine;

public class Globalable<TValue> : MonoBehaviour  where TValue : class
{
    public static TValue Instance
    {
        get
        {
            if (Instance == null)
            {
                Instance = FindObjectOfType<Globalable<TValue>>(true) as TValue;
                if (Instance == null)
                {
                    Debug.LogError($"No instance of {typeof(TValue).Name} found in the scene.");
                }
            }

            return Instance;
        }
        protected set
        {
            Instance = value;
        }
    }
    
    protected virtual void Awake()
    {
        Instance = this as TValue;
    }
}
