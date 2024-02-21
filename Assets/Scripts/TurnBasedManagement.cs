using System;
using System.Collections;
using System.Collections.Generic;
using Microlight.MicroBar;
using MoreMountains.Feedbacks;
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

    private bool isPaused;

    [Header("About Player")]
    [SerializeField] private bool player1BullyTurn;
    [SerializeField] private List<Player> playerList;
    
    [SerializeField] private Nf_GameEvent endRoundEvent;

    public MMF_Player feedback;

    // Start is called before the first frame update
    void Start()
    {
        StartTimer();
        // playerList[1]._MicroBar.GetComponent<MicroBar>().Initialize(playerList[1].CurrentHealth);
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
        if (!isPaused)
        {
            if (timer > 0f)
            {
                timer -= Time.deltaTime;
                UpdateTimerDisplay();
            }
            else
            {
                OnTimerEnd();
                StartTurn();
            }
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
        endRoundEvent.Raise();
        
        // randomItems[0].transform.parent.parent.parent.gameObject.SetActive(true);
    }

    void Player1BullyTurn()
    {
        Debug.Log("Player 1 bullying turn.");

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

        playerList[1].IsBullying = true;
        playerList[0].IsBullying = false;
        
        playerList[1].GetComponent<Transform>().localScale = new Vector3(5f, 5f, 1f);
        playerList[0].GetComponent<Transform>().localScale = new Vector3(3.5f, 3.5f, 1f);
       
        playerList[0].CurrentHealth = playerList[0].MaxHealth;
        playerList[0]._MicroBar.UpdateHealthBar(playerList[0].CurrentHealth);
    }

    void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            timerText.text = Mathf.CeilToInt(timer).ToString();
        }
    }

    void OnTimerEnd()
    {
        Debug.Log("Time's up!");
        BlankMethod();
        ResetTimer(); // Optionally reset the timer after it reaches zero
    }

    void BlankMethod()
    {
        feedback.PlayFeedbacks();
    }

    public void StartTimer()
    {
        isPaused = false;
        timer = turnTime;
    }

    public void PauseTimer()
    {
        isPaused = true;
    }

    public void ResumeTimer()
    {
        isPaused = false;
    }

    public void ResetTimer()
    {
        timer = turnTime;
        UpdateTimerDisplay(); // Update the display when resetting the timer
    }
}