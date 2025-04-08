using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameStartUI : MonoBehaviour
{
    [SerializeField] Button startButton;

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MiniGameScene");    
    }
}
