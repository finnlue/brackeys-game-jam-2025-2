using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    public LayerMask zombieLayer;
    public float spread;
    public float range;
    public bool automatic;
    public int maxAmmo;
    public int ammoPerShot;
    public int ammo;
    public float primaryFireCooldown;
    public int damagePerHit;

    private bool canPrimaryFire = true;

    protected virtual void Start()
    {
        zombieLayer = LayerMask.GetMask("Zombie");
    }

    public void SemiAutomatic(Vector3 origin, Vector3 direction)
    {
        ammo-=ammoPerShot;

        RaycastHit targetHit;
        Vector3 spreadVec = new Vector3(Random.Range(0, spread), Random.Range(0, spread), 0);
        bool hit = Physics.Raycast(origin, direction + spreadVec, out targetHit, range, zombieLayer);
        Debug.DrawRay(origin, (direction + spreadVec) * range, Color.yellow, 0.7f);
        if (hit)
        {
            Debug.Log(targetHit.transform.name);
            targetHit.transform.gameObject.GetComponent<BaseEnemyLogic>().TakeDamage(damagePerHit);
        }
    }


    public virtual void PrimaryFire(Vector3 origin, Vector3 direction)
    {   
        Debug.Log(ammo);
        if (ammo <= 0)
        {
            //need to reload
            Debug.Log("No Ammo");
            return;
        }
        if (!automatic)
        {
            SemiAutomatic(origin, direction);
            canPrimaryFire = false;
            Invoke(nameof(ResetPrimaryFire), primaryFireCooldown);
        }
    }
 
    public virtual void ResetPrimaryFire()
    {
        canPrimaryFire = true;
    }
}
