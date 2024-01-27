using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon Data/Weapon")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public Sprite weaponIcon;

    public Weapon.WeaponRange WeaponType;
    public Sprite WeaponSprite;
    public Sprite BulletSprite;
    public int Range;
    public int WeaponCooldown;
    public int WeaponRotation;

}