using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssultRifle : Weapon
{
    [Header("Visual")]
    public GameObject muzzleFlash;
    public float muzzleFlashDuration = 0.1f;
    
    [Header("Bullet Shell")]
    public Transform shellEjectTransform;
    
    private float CurrentMuzzleFlashDuration { get; set; }
    
    private Camera mainCam;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        muzzleFlash.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        CurrentFireRate += Time.deltaTime;
        CurrentMuzzleFlashDuration += Time.deltaTime;

        if (CurrentMuzzleFlashDuration >= muzzleFlashDuration)
        {
            muzzleFlash.SetActive(false);
        }
    }


    public override bool Fire()
    {
        if(CurrentFireRate < data.fireRate)
            return false;

        /*if (CurrentAmmo <= 0)
            return false;*/

        CurrentFireRate = 0f;

        CurrentAmmo--;
        
        muzzleFlash.transform.localRotation 
            *= Quaternion.AngleAxis(Random.Range(0,360), Vector3.right);
        
        muzzleFlash.SetActive(true);
        CurrentMuzzleFlashDuration = 0f;
        
        // shell eject
        var shell= ObjectPoolManager.Instance.GetObjectOrNull("Shell");
        shell.GameObject.transform.position = shellEjectTransform.position;
        shell.GameObject.transform.forward = shellEjectTransform.forward;
        
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, data.range, LayerMask.GetMask("Enemy")))
        {
            HitBox hitBox = hit.collider.GetComponent<HitBox>();
            if (hitBox is null)
            {
                return false;
            }
            
            IDamagable enemy = CombatSystem.Instance.GetMonsterOrNull(hitBox);
            if (enemy != null)
            {
                CombatEvent combatEvent = new CombatEvent
                {
                    Sender = Player.localPlayer,
                    Receiver = enemy,
                    Damage = data.damage,
                    HitPosition = hit.point,
                    HitNormal = hit.normal,
                    HitBox = hitBox
                };
                CombatSystem.Instance.AddCombatEvent(combatEvent);
            }
        }
        
        
        return true;
    }
    
    
}
