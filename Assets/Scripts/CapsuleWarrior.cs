using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CapsuleWarrior : MonoBehaviour
{
    private  static readonly List<CapsuleWarrior> s_capsuleWarriors = new List<CapsuleWarrior>();
    public static CapsuleWarrior GetCapsuleWarrior(Collider collider)
    {
        foreach (var warrior in s_capsuleWarriors)
        {
            if (warrior.Collider == collider)
                return warrior;
        }
        
        return null;
    }
    
    public Collider Collider { get; private set; }
    
    private Material mat;
    
    private static readonly Color[] HP_COLORS = { Color.clear, Color.red, Color.yellow, Color.gray};

    private int maxHp = 3;
    private int currentHp;
    
    void Start()
    {
        Collider = GetComponent<Collider>();
        mat = GetComponent<MeshRenderer>().material;
        
        currentHp = maxHp;
        ChangeHp(0);
        s_capsuleWarriors.Add(this);
    }

    void Update()
    {
        
    }

    public void ChangeHp(int hp)
    {
        currentHp += hp;
        currentHp = Mathf.Clamp(currentHp, 0, maxHp);

        if (currentHp <= 0)
        {
            Destroy(gameObject);
            return;
        }
        mat.color = HP_COLORS[currentHp];
    }
}
