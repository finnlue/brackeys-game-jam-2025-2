using UnityEngine;
using System.Collections;


[CreateAssetMenu]
public class WeaponBluePrint : ScriptableObject
{
    public enum FireModes
    {
        SemiAutomatic,
        Automatic
    }

    public FireModes fireMode;
    public bool projectile;
    public bool scoped;

    public float range;
    public float spread;
    public float spreadScaling;

    public int magazineSize;
    [HideInInspector] public int ammoInMagazine;
    public int maxAmmo;
    [HideInInspector] public int reserveAmmo;
    public float reloadTime;

    public int ammoPerShot;

    public float fireCooldown;
    public int damagePerHit;

    public GameObject model;
    
}