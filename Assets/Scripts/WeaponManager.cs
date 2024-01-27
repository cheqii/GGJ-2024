using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{

    [SerializeField] private Weapon melee;
    [SerializeField] private Weapon longRange;

    [Header("**for debug only**")]
    [SerializeField] private WeaponData testData;



    public void SetWeaponTest()
    {
        SetWeapon(testData);
    }

    public void SetWeapon(WeaponData weaponData)
    {
        switch (weaponData.WeaponType)
        {
            case Weapon.WeaponRange.long_range:
                longRange.transform.GameObject().SetActive(true);
                melee.transform.GameObject().SetActive(false);

                longRange._WeaponRange = Weapon.WeaponRange.long_range;

                
                longRange.SetWeaponRenderer
                (weaponData.WeaponSprite,
                    weaponData.WeaponCooldown,
                    weaponData.Range,
                    weaponData.WeaponRotation,
                    weaponData.damage
                );
                break;
            
            case Weapon.WeaponRange.trap:
                longRange.transform.GameObject().SetActive(true);
                melee.transform.GameObject().SetActive(false);

                longRange._WeaponRange = Weapon.WeaponRange.trap;
                
                longRange.SetWeaponRenderer
                (weaponData.WeaponSprite,
                    weaponData.WeaponCooldown,
                    weaponData.Range,
                    weaponData.WeaponRotation,
                    weaponData.damage
                );
                
                
                break;
            
            case Weapon.WeaponRange.melee:
                longRange.transform.GameObject().SetActive(false);
                melee.transform.GameObject().SetActive(true);
                
                melee.SetWeaponRenderer
                    (weaponData.WeaponSprite,
                    weaponData.WeaponCooldown,
                    weaponData.Range,
                    weaponData.WeaponRotation,
                weaponData.damage

                        );
                
                break;
        }
    }
}
