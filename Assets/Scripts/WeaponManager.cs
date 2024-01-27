using System.Collections;
using System.Collections.Generic;
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
                longRange.SetWeaponRenderer
                (weaponData.WeaponSprite,
                    weaponData.WeaponCooldown,
                    weaponData.Range,
                    weaponData.WeaponRotation,
                    weaponData.damage
                );
                break;
            case Weapon.WeaponRange.melee:
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
