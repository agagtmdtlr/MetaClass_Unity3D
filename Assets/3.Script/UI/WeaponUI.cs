using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal;
using UnityEngine;

public class WeaponUI : MonoBehaviour
{
    public TMP_Text CurrnetWeaponNameText;
    public TMP_Text CurrnetAmmoText;
    public TMP_Text MaxAmmoText;
    
    private void Start()
    {
        if(PlayerController.CurrentPlayerController == null)
            Debug.LogWarning("PlayerController is null");

        PlayerController.CurrentPlayerController.PlayerEvents.OnEquipWeapon += UpdateWeapon;
    }

    private void OnDisable()
    {
        PlayerController.CurrentPlayerController.PlayerEvents.OnEquipWeapon -= UpdateWeapon;
    }
    
    void UpdateWeapon(Weapon weapon)
    {
        CurrnetWeaponNameText.text = weapon.data.weaponName;
        CurrnetAmmoText.text = weapon.CurrentAmmo.ToString();
        MaxAmmoText.text = weapon.data.totalAmmo.ToString();
        
        weapon.events.OnChangeAmmo += UpdateCurrentAmmo;
    }
    
    void UpdateCurrentAmmo(int currentAmmo)
    {
        CurrnetAmmoText.text = currentAmmo.ToString();
    }

}
