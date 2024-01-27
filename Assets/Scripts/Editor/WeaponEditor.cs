using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WeaponManager))]
public class WeaponEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        WeaponManager weapon = (WeaponManager)target;

        if (GUILayout.Button("Set Weapon"))
        {
            weapon.SetWeaponTest();
        }
    }
}