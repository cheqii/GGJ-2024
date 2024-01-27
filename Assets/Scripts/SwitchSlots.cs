using UnityEngine;

public class SwitchSlots : MonoBehaviour
{
    public GameObject slotA;
    public GameObject slotB;

    public RandomItem randomItemA;
    public RandomItem randomItemB;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("L");
            SwithSlotsAB();
        }
    }

    public void SwithSlotsAB()
    {
        Vector3 A = slotA.transform.position;
        Vector3 B = slotB.transform.position;

        slotA.transform.position = B;
        slotB.transform.position = A;

        WeaponManager randomA = randomItemA._weaponManager;
        WeaponManager randomB = randomItemB._weaponManager;

        randomItemA._weaponManager = randomB;
        randomItemB._weaponManager = randomA;
    }
}