using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static LocalPlayer localPlayer
    {
        get
        {
            if (localPlayer == null)
            {
                localPlayer = FindObjectOfType<LocalPlayer>(true);
            }
            return localPlayer;
        }
        set
        {
            localPlayer = value;
        }
        
        
    }
}
