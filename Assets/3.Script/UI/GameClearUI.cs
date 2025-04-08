using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameClearUI : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Button restartButton;
    
    [SerializeField] GameObject gameClearUI;
    
    void Start()
    {
        CombatSystem.Instance.Events.OnDeathEvent += ShowGameClearUI;
        restartButton.onClick.AddListener(RestartGame);
    }
    
    void ShowGameClearUI(DeathEvent deathEvent)
    {
        if (deathEvent.Dead.DamageableType != typeof(BossMonster)) return;
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        gameClearUI.SetActive(true);
        
        scoreText.text = GameManager.Instance.GameScore.ToString();
    }

    void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MiniGameScene",LoadSceneMode.Single);
    }
}
