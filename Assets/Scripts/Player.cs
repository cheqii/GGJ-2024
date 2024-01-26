using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int playerIndex;
    
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    
    [SerializeField] private float spawnAreaWidth = 10f;
    [SerializeField] private float spawnAreaHeight = 5f;

    [SerializeField] private float respawnDelay = 2f;

    [SerializeField] private bool isDead;
    
    [Header("Bullying State")]
    [SerializeField] private bool isBullying;

    public bool IsBullying
    {
        get => isBullying;
        set => isBullying = value;
    }
    void Start()
    {
        currentHealth = maxHealth;
    }
    
    void Update()
    {
        if (currentHealth <= 0 && !isBullying)
        {
            isDead = true;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            StartCoroutine(RespawnPlayer());
        }

        if(Input.GetKeyDown(KeyCode.Space)) DeceaseHealth(10);
    }

    public void DeceaseHealth(float value)
    {
        if (currentHealth > 0 && !isBullying) currentHealth -= value;
        if (currentHealth <= 0 && !isBullying) {currentHealth = 0;}
    }

    IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(respawnDelay);
        if (GameManager.Instance.GetComponentInChildren<TurnBasedManagement>().Timer > 0 && isDead)
        {
            float randomX = Random.Range(-spawnAreaWidth / 2, spawnAreaWidth / 2);
            float randomY = Random.Range(-spawnAreaHeight / 2, spawnAreaHeight / 2);

            transform.position = new Vector3(randomX, randomY, 0f);
            isDead = false;
            currentHealth = maxHealth;
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            
            if (playerIndex == 1 && !isBullying) // if player1 is bullying then player 1 will get score by bully player 2
            {
                print($"Player 1 get score");
                GameManager.Instance.GetComponent<ScoreCount>().IncreasePlayer1Score(1);
            }

            if (playerIndex == 0 && !isBullying) // if player2 is bullying then player 2 will get score by bully player 1
            {
                print($"Player 2 get score");
                GameManager.Instance.GetComponent<ScoreCount>().IncreasePlayer2Score(1);
            }
        }
    }
}
