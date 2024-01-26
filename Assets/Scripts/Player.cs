using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Is Bullying")]
    [SerializeField] private bool isBullying;

    public bool IsBullying
    {
        get => isBullying;
        set => isBullying = value;
    }

    [Header("Get Bully")]
    [SerializeField] private bool getBully;

    public bool GetBully
    {
        get => getBully;
        set => getBully = value;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckBullyState()
    {
        
    }
}
