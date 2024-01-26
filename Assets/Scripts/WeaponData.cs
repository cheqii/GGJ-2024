using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon Data/Weapon")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public Sprite weaponIcon;
}