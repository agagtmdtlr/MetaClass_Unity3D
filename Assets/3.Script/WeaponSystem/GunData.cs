using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GunData
{
    /// <summary>
    /// 무기 이름
    /// </summary>
    public string weaponName;

    /// <summary>
    /// 발사 속도
    /// </summary>
    public float fireRate = 0.05f;

    /// <summary>
    /// 데미지
    /// </summary>
    public int damage = 10;

    /// <summary>
    /// 총알 총량
    /// </summary>
    public int totalAmmo = 30;

    /// <summary>
    /// 재장전 시간
    /// </summary>
    public float reloadTime = 2f;

    /// <summary>
    /// 사거리
    /// </summary>
    public float range = 100f;
}
