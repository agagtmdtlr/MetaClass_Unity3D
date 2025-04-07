using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatUI : MonoBehaviour
{
    [SerializeField] Slider healthSlider;
    
    
    protected void UpdateHealth(int currentHealth,int maxHealth)
    {
        healthSlider.value = currentHealth / (float)maxHealth;
    }
}
