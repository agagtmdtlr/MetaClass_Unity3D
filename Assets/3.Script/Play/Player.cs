using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static LocalPlayer _localPlayer;
    public static LocalPlayer localPlayer
    {
        get
        {
            if (_localPlayer == null)
            {
                _localPlayer = FindObjectOfType<LocalPlayer>(true);
            }
            return _localPlayer;
        }
        set
        {
            _localPlayer = value;
        }
    }
}
