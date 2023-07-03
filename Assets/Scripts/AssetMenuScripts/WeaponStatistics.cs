using System.IO.Enumeration;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Weapon Data", order = 1)]
public class WeaponStatistics : ScriptableObject
{
    public string Name;
    public float FireRate;
    public float CoolDownTime;
    public float ProjectileSpeed;
    public float BulletSpread;
    public int ClipSize;
    public GameObject PrimaryProjectile;
    public GameObject MuzzleFlash;
    public AudioClip[] FireSounds;
}
