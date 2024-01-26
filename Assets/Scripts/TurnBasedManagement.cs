using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnBasedManagement : MonoBehaviour
{
    [Header("Timer")] 
    [SerializeField] private TextMeshProUGUI timerText;
    
    [SerializeField] private float turnTime;
    [SerializeField] private float timer;

    [SerializeField] private bool player1BullyTurn;

    [SerializeField] private List<Player> playerList;

    // Start is called before the first frame update
    void Start()
    {
        timer = turnTime;
        Player1BullyTurn();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(timer / 60);
            int seconds = Mathf.FloorToInt(timer % 60);

            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        if (timer <= 0)
        {
            RandomWeapon();
            StartTurn();
        }
    }
    
    void StartTurn()
    {
        // Reset timer for the new turn.
        timer = turnTime;

        if (player1BullyTurn)
        {
            // Player's turn logic goes here
            // Call method A for player's turn
            Player2BullyTurn();
        }
        else
        {
            // Enemy's turn logic goes here
            // Call method B for enemy's turn
            Player1BullyTurn();
        }

        // Switch turn
        player1BullyTurn = !player1BullyTurn;
    }

    void RandomWeapon()
    {
        // End turn logic, if any
        // random new weapon
    }

    void Player1BullyTurn()
    {
        Debug.Log("Player 1 bullying turn.");
        playerList[0].GetComponent<Transform>().localScale = new Vector3(1.2f, 1.2f, 1f);
        playerList[1].GetComponent<Transform>().localScale = new Vector3(0.7f, 0.7f, 1f);
        // Add your logic for method B here
    }

    void Player2BullyTurn()
    {
        Debug.Log("Player 2 bullying turn.");
        playerList[1].GetComponent<Transform>().localScale = new Vector3(1.2f, 1.2f, 1f);
        playerList[0].GetComponent<Transform>().localScale = new Vector3(0.7f, 0.7f, 1f);
        // Add your logic for method A here
    }
}