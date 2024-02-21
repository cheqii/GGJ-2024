using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    private Player player;
    
    [SerializeField] private Sprite statusSprite;
    [SerializeField] private GameObject statusGo;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.RightControl))
        // {
        //     AttachStatus(player._StatusData);
        // }
    }

    public void AttachStatus(StatusData data)
    {
        statusGo.GetComponent<SpriteRenderer>().sprite = data.statusSprite;
        var status = Instantiate(statusGo, transform.position, quaternion.identity, transform);
        Destroy(status, 1f);
    }
}
