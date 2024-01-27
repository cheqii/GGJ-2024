using System;
using System.Collections;
using System.Collections.Generic;
using Microlight.MicroBar;
using TMPro;
using UnityEngine;

public class TurnBasedManagement : MonoBehaviour
{
    [Header("Timer")] 
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float turnTime;
    [SerializeField] private float timer;

    public float Timer
    {
        get => timer;
        set => timer = value;
    }

    [Header("About Player")]
    [SerializeField] private bool player1BullyTurn;
    [SerializeField] private List<Player> playerList;
    
    [SerializeField] private Nf_GameEvent endRoundEvent;

    [Header("Random Items")]
    [SerializeField] private List<RandomItem> randomItems;

    // Start is called before the first frame update
    void Start()
    {
        timer = turnTime;
        // playerList[1]._MicroBar.GetComponent<MicroBar>().Initialize(playerList[1].CurrentHealth);
        EndTurn();
        try
        {
            Player1BullyTurn();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!randomItems[0].IsMoving && !randomItems[0].IsMoving)
        {
            timer -= Time.deltaTime;
            // StartCoroutine(RandomItems());
        }
        
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(timer / 60);
            int seconds = Mathf.FloorToInt(timer % 60);

            // timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            timerText.text = seconds.ToString();
        }

        if (timer <= 0)
        {
            EndTurn();
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

    void EndTurn()
    {
        // End turn logic, if any
        // random new weapon
        // endRoundEvent.Raise();
        
        // randomItems[0].transform.parent.parent.parent.gameObject.SetActive(true);
        
        randomItems[0].StartMoving();
        randomItems[1].StartMoving();
    }

    void Player1BullyTurn()
    {
        Debug.Log("Player 1 bullying turn.");
        
        // randomItems[0].StartMoving();
        // randomItems[1].StartMoving();
        
        playerList[0].IsBullying = true;
        playerList[1].IsBullying = false; 
        
        playerList[0].GetComponent<Transform>().localScale = new Vector3(5f, 5f, 1f);
        playerList[1].GetComponent<Transform>().localScale = new Vector3(3.5f, 3.5f, 1f);
       
        
        playerList[1].CurrentHealth = playerList[1].MaxHealth;
        playerList[1]._MicroBar.UpdateHealthBar(playerList[1].CurrentHealth);
    }

    void Player2BullyTurn()
    {
        Debug.Log("Player 2 bullying turn.");
        
        // randomItems[0].StartMoving();
        // randomItems[1].StartMoving();
        
        playerList[1].IsBullying = true;
        playerList[0].IsBullying = false;
        
        playerList[1].GetComponent<Transform>().localScale = new Vector3(5f, 5f, 1f);
        playerList[0].GetComponent<Transform>().localScale = new Vector3(3.5f, 3.5f, 1f);
       
        playerList[0].CurrentHealth = playerList[0].MaxHealth;
        playerList[0]._MicroBar.UpdateHealthBar(playerList[0].CurrentHealth);
    }

    IEnumerator RandomItems()
    {
        yield return new WaitForSeconds(0.2f);
        randomItems[0].transform.parent.parent.parent.gameObject.SetActive(false);
    }
}