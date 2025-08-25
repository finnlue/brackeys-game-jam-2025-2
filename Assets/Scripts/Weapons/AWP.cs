using UnityEngine;
using UnityEngine.SocialPlatforms;

public class AWP : BaseWeapon
{
    protected override void Start()
    {
        base.Start();
        
    }

    public override void PrimaryFire(Vector3 origin, Vector3 direction)
    {

        RaycastHit target;
        Vector3 spreadVec = new Vector3(Random.Range(0, spread), Random.Range(0, spread), 0);
        bool hit = Physics.Raycast(origin, direction + spreadVec, out target, range, zombieLayer);
        Debug.DrawRay(origin, direction * range, Color.red, 0.7f);
        if (hit)
        {
            Debug.Log(target.transform.name);
        }
        

    }
}
