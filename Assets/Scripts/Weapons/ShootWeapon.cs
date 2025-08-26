using System.Collections;
using UnityEngine;

public class ShootWeapon : MonoBehaviour
{
    public LayerMask zombieLayer;
    public MovementController movementController;

    [HideInInspector] public bool canFire = true;
    private bool keepFiring=false;

    protected void Start()
    {
        zombieLayer = LayerMask.GetMask("Zombie");
    }


    public void StartFire(WeaponBluePrint weapon, Vector3 origin, Vector3 direction)
    {
        if (!canFire)
            return;
        if (weapon.fireMode == WeaponBluePrint.FireModes.SemiAutomatic)
        {
            PullTrigger(weapon, origin, direction,weapon.spread); 
            canFire = false;
            Invoke(nameof(ResetFire), weapon.fireCooldown);
        }
        else if (weapon.fireMode == WeaponBluePrint.FireModes.Automatic)
        {
            keepFiring = true;
            StartCoroutine(AutomaticFire(weapon,origin,direction,weapon.spread));        
        }       
    }

    public virtual void ResetFire()
    {
        canFire = true;
    }

    public void StopFire()
    {
        keepFiring = false;
    }

    IEnumerator AutomaticFire(WeaponBluePrint weapon, Vector3 origin, Vector3 direction, float spread)
    {
        origin = movementController.camerPos.position;
        direction = movementController.orientation.forward;
        PullTrigger(weapon, origin, direction, spread);
        spread += weapon.spreadScaling;
        yield return new WaitForSeconds(weapon.fireCooldown);
        if (weapon.ammoInMagazine <= 0)
        {
            keepFiring = false;
        } 
        if (keepFiring)
            StartCoroutine(AutomaticFire(weapon, origin, direction,spread));
    }
    
    public void PullTrigger(WeaponBluePrint weapon, Vector3 origin, Vector3 direction, float spread)
    {
        int numberOfShots = Mathf.Min(weapon.ammoPerShot, weapon.ammoInMagazine);

        for (int i = 0; i < numberOfShots; i++)
        {
            if (weapon.projectile)
                Projectile(origin, direction);
            else
                Hitscan(origin, direction,spread, weapon.range, weapon.damagePerHit);
        }
        weapon.ammoInMagazine -= numberOfShots;
    }

    public void Hitscan(Vector3 origin, Vector3 direction, float spread, float range, int damage)
    {

        RaycastHit targetHit;
        Vector3 spreadVec = new Vector3(Random.Range(-spread /2, spread/2), Random.Range(-spread/2, spread/2), 0);
        bool hit = Physics.Raycast(origin, direction + spreadVec, out targetHit, range, zombieLayer);
        Debug.DrawRay(origin, (direction + spreadVec) * range, Color.yellow, 0.7f);
        if (hit)
        {
            Debug.Log(targetHit.transform.name);
            targetHit.transform.gameObject.GetComponent<BaseEnemyLogic>().TakeDamage(damage);
        }
    }

    public void Projectile(Vector3 origin, Vector3 direction)
    {

    }


    

   
}
