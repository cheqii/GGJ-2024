using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private Sprite statusSprite;
    
    [SerializeField] private GameObject statusGo;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            AttachStatus();
        }
    }

    public void AttachStatus()
    {
        statusGo.GetComponent<SpriteRenderer>().sprite = statusSprite;
        var status = Instantiate(statusGo, transform.position, quaternion.identity, transform);
        Destroy(status, 1f);
    }
}
