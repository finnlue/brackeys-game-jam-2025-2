using System.Collections;
using UnityEngine;

public class ShootWeapon : MonoBehaviour
{
    public LayerMask zombieLayer;
    public MovementController movementController;
    private GunController gunController;
    public GameObject decal;

    [HideInInspector] public bool canFire = true;
    private bool keepFiring=false;

    protected void Start()
    {
        zombieLayer = LayerMask.GetMask("Zombie");
        gunController =  GetComponent<GunController>();
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
        canFire = false;
        yield return new WaitForSeconds(weapon.fireCooldown);
        canFire = true;
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
        gunController.currentAnimator.SetTrigger("Fire");
        for (int i = 0; i < numberOfShots; i++)
        {
            if (weapon.projectile)
                Projectile(origin, direction);
            else
                Hitscan(origin, direction, spread, weapon.range, weapon.damagePerHit);
        }
        weapon.ammoInMagazine -= numberOfShots;
    }

    public Vector3 calculateSpread(Vector3 direction, float spread)
    {
        Vector2 randomSpread = Random.insideUnitCircle * Mathf.Tan(spread * Mathf.Deg2Rad);
        Quaternion rotation = Quaternion.LookRotation(direction);
        Vector3 spreadVec = rotation * ( Vector3.forward + new Vector3(randomSpread.x,randomSpread.y,0));
        return spreadVec;

    }
    public void Hitscan(Vector3 origin, Vector3 direction, float spread, float range, int damage)
    {
        Vector3 spreadVec = calculateSpread(direction, spread);
       
        bool hit = Physics.Raycast(origin, spreadVec, out RaycastHit targetHit, range, zombieLayer);
        Debug.DrawRay(origin, (direction + spreadVec) * range, Color.yellow, 0.7f);
        Instantiate(decal, origin + (direction + spreadVec) * range, Quaternion.identity);
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
