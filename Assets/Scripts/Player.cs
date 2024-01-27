using System;
using System.Collections;
using System.Collections.Generic;
using Microlight.MicroBar;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    public int playerIndex;

    [SerializeField] private GameObject playerPrefab;
    
    [Header("Player Health")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;

    [Header("Micro Bar")]
    [SerializeField] private MicroBar _microBar;
    
    [Header("Random SpawnArea")]
    [SerializeField] private float spawnAreaWidth = 10f;
    [SerializeField] private float spawnAreaHeight = 5f;

    [SerializeField] private float respawnDelay;

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
        _microBar = GetComponentInChildren<MicroBar>();
        currentHealth = maxHealth;
        _microBar.Initialize(currentHealth);
    }
    
    void Update()
    {
        if (currentHealth <= 0 && !isBullying)
        {
            isDead = true;
            Die();
            StartCoroutine(RespawnPlayer());
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            DeceaseHealth(10);
        }
    }

    public void DeceaseHealth(float value)
    {
        if (currentHealth > 0 && !isBullying)
        {
            currentHealth -= value;
            _microBar.UpdateHealthBar(currentHealth);
        }

        if (currentHealth <= 0 && !isBullying)
        {
            currentHealth = 0;
        }
    }

    void Die()
    {
        try
        {
            var child = transform.GetChild(0).gameObject;
            Destroy(child);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
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
            
            Instantiate(playerPrefab, transform.position, quaternion.identity, transform);
            _microBar = GetComponentInChildren<MicroBar>();
            _microBar.Initialize(currentHealth);
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
