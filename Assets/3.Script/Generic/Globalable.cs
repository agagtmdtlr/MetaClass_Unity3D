

using UnityEngine;

public class Globalable<TValue> : MonoBehaviour  where TValue : class
{
    private static TValue _instance;
    public static TValue Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Globalable<TValue>>(true) as TValue;
                if (_instance == null)
                {
                    Debug.LogError($"No instance of {typeof(TValue).Name} found in the scene.");
                }
            }

            return _instance;
        }
        protected set
        {
            _instance = value;
        }
    }
    
    protected virtual void Awake()
    {
        Instance = this as TValue;
    }
}
