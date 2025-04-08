using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Button restartButton;
    
    [SerializeField] GameObject gameOverUI;
    
    void Start()
    {
        CombatSystem.Instance.Events.OnDeathEvent += ShowGameOverUI;
        restartButton.onClick.AddListener(RestartGame);
    }
    
    void ShowGameOverUI(DeathEvent deathEvent)
    {
        if (deathEvent.Dead.DamageableType != typeof(LocalPlayer)) return;
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameOverUI.SetActive(true);
        
        scoreText.text = GameManager.Instance.GameScore.ToString();
    }

    void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MiniGameScene",LoadSceneMode.Single);
    }
    
}
