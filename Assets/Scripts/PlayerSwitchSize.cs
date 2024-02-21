using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitchSize : MonoBehaviour
{
    public Player.PlayerType side = Player.PlayerType.BluePlayer;

    [SerializeField] private GameObject redPlayer;
    [SerializeField] private GameObject bluePlayer;

    [SerializeField] private float normalSize;
    [SerializeField] private float bossSize;



    public void SwitchSide()
    {
        if (side == Player.PlayerType.BluePlayer)
        {
            redPlayer.transform.localScale = new Vector3(normalSize,normalSize,normalSize);
            bluePlayer.transform.localScale = new Vector3(bossSize,bossSize,bossSize);

            
            side = Player.PlayerType.RedPlayer;
        }
        else
        {
            redPlayer.transform.localScale = new Vector3(bossSize,bossSize,bossSize);
            bluePlayer.transform.localScale = new Vector3(normalSize,normalSize,normalSize);
            side = Player.PlayerType.BluePlayer;
        }
    }
}
