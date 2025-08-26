using UnityEngine;
using UnityEngine.UI;


public class TestUI : MonoBehaviour
{
    public WeaponBluePrint currentWeapon;
    public Text weaponName;
    public Text currentAmmo;
    public Text magazineSize;
    public Text reserveAmmo;
   
    void Start()
    {
        
    }

    void Update()
    {
        weaponName.text = currentWeapon.name;
        currentAmmo.text = currentWeapon.ammoInMagazine.ToString();
        magazineSize.text = currentWeapon.magazineSize.ToString();
        reserveAmmo.text = currentWeapon.reserveAmmo.ToString();
    }
}
