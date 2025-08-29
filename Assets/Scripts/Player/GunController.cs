using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public ShootWeapon shootWeapon;

    [Header("Primary Fire")]
    private WeaponBluePrint currentWeapon;
    private int currentWeaponID = 0;
    public List<WeaponBluePrint>availableWeapons;
    public TestUI testUI;
    public GameObject currentModel;
    public GameObject gunPosition;
    public Animator currentAnimator;
    public MovementController movementController;
    private GameObject currentInstance;

    private Vector3 cameraPosition;
    private Vector3 forwardVector;

    private bool reloading;

    void Start()
    {
        foreach (WeaponBluePrint weapon in availableWeapons)
        {
            weapon.ammoInMagazine = weapon.magazineSize;
            weapon.reserveAmmo = weapon.maxAmmo;
        }
        shootWeapon = GetComponentInChildren<ShootWeapon>();
        currentWeapon = availableWeapons[currentWeaponID];
        currentModel = currentWeapon.model;
        currentInstance = Instantiate(currentModel, gunPosition.transform);
        currentAnimator = currentInstance.GetComponentInChildren<Animator>();
        testUI.currentWeapon = currentWeapon;
     
    }

    public void StartFire()
    {
        cameraPosition = movementController.camerPos.position;
        forwardVector = movementController.orientation.forward;
        if (currentWeapon.ammoInMagazine <= 0 && !reloading)
        {
            StartCoroutine(Reload());
            return;
        }
        shootWeapon.StartFire(availableWeapons[currentWeaponID], cameraPosition, forwardVector);
    }

    public void StopFire()
    {
        shootWeapon.StopFire();
    }

    public IEnumerator Reload()
    {
        if (currentWeapon.reserveAmmo <= 0 || currentWeapon.ammoInMagazine == currentWeapon.magazineSize)
        {
            Debug.Log("cant reload, no ammo left");
            yield break;
        }
        currentAnimator.SetTrigger("Reload");
        
        shootWeapon.canFire = false;
        reloading = true;
        yield return new WaitForSeconds(currentWeapon.reloadTime);
        int reloadAmmo = Mathf.Min(currentWeapon.reserveAmmo, currentWeapon.magazineSize - currentWeapon.ammoInMagazine);
        currentWeapon.reserveAmmo -= reloadAmmo;
        currentWeapon.ammoInMagazine += reloadAmmo;
        shootWeapon.canFire = true;
        currentAnimator.ResetTrigger("Reload");
        reloading = false;
        }

    public void SwitchWeapon()
    {
        StopFire();
        Destroy(currentInstance);
        currentWeapon = availableWeapons[currentWeaponID];
        currentModel = currentWeapon.model;
        currentInstance = Instantiate(currentModel, gunPosition.transform);
        currentAnimator = currentInstance.GetComponentInChildren<Animator>();
        testUI.currentWeapon = currentWeapon;

    }

    public void NextWeapon()
    {
        currentWeaponID += 1;
        if (currentWeaponID > availableWeapons.Count - 1)
            currentWeaponID = 0;

        SwitchWeapon();
    }
    public void prevoiusWeapon()
    {
        currentWeaponID -= 1;
        if (currentWeaponID < 0)
            currentWeaponID = availableWeapons.Count - 1;
        SwitchWeapon();
    }

    public void PickUpWeapon(WeaponBluePrint weapon)
    {
        if (availableWeapons.Contains(weapon))
        {
            weapon.reserveAmmo += weapon.magazineSize * 2;
        }
        else
        {
            Debug.Log("Added Shotgun");
            availableWeapons.Add(weapon);
            currentWeaponID = availableWeapons.Count-1;
            currentWeapon =weapon;
        }
    }
}