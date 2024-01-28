using System;
using System.Collections;
using System.Collections.Generic;
using Microlight.MicroBar;
using MoreMountains.Feedbacks;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    public enum PlayerType
    {
        RedPlayer,
        BluePlayer
    }

    
    #region -Declared Variables-

    public int playerIndex;

    [SerializeField] private GameObject playerPrefab;
    
    [Header("Player Health")]
    [SerializeField] private float maxHealth;
    public float MaxHealth
    {
        get => maxHealth;
        set => maxHealth = value;
    }
    
    [SerializeField] private float currentHealth;
    public float CurrentHealth
    {
        get => currentHealth;
        set => currentHealth = value;
    }

    [Header("Micro Bar")]
    [SerializeField] private MicroBar _microBar;
    public MicroBar _MicroBar
    {
        get => _microBar;
        set => _microBar = value;
    }
    
    [Header("Random SpawnArea")]
    [SerializeField] private float spawnAreaWidth = 10f;
    [SerializeField] private float spawnAreaHeight = 5f;

    [SerializeField] private float respawnDelay;

    [SerializeField] private bool isDead;

    public bool IsDead
    {
        get => isDead;
        set => isDead = value;
    }
    
    [Header("Bullying State")]
    [SerializeField] private bool isBullying;

    public PlayerType _PlayerType;

    public bool IsBullying
    {
        get => isBullying;
        set => isBullying = value;
    }

    [Header("Status Data")]
    [SerializeField] private bool triggerStatus;

    public bool TriggerStatus
    {
        get => triggerStatus;
        set => triggerStatus = value;
    }

    [SerializeField] private StatusData _statusData;

    public StatusData _StatusData
    {
        get => _statusData;
        set => _statusData = value;
    }


    public GameObject Blood;
    public MMF_Player ShakeFeedback;

    public GameObject pointParticle;

    [Header("Floating Text")]
    [SerializeField] private GameObject bluePointFloatingText;
    [SerializeField] private GameObject redPointFloatingText;

    public GameObject canvas;
    
    
    #endregion

    void Start()
    {
        _microBar = GetComponentInChildren<MicroBar>();
        currentHealth = maxHealth;
        _microBar.Initialize(currentHealth);
    }
    
    void Update()
    {
        if(_microBar == null) return;
        if (isBullying)
        {
            _microBar.gameObject.SetActive(false);
        }
        else
        {
            _microBar.gameObject.SetActive(true);
        }
        
        if (currentHealth <= 0 && !isBullying)
        {
            isDead = true;
            Die();
            StartCoroutine(RespawnPlayer());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("CheckPoint"))
        {
            var score = GameManager.Instance.GetComponent<ScoreCount>();
            var checkPoint = GameManager.Instance.GetComponent<CheckPoint>();
            if (!isBullying && playerIndex == 0)
            {
                SpawnFloatingText(bluePointFloatingText, 1.5f, 1f);
                Instantiate(pointParticle, other.gameObject.transform.position, quaternion.identity);
                ShakeFeedback.PlayFeedbacks();
                checkPoint.spawnCheck = false;
                score.IncreasePlayer1Score(1);
                Destroy(other.gameObject, 0.2f);
                StartCoroutine(checkPoint.RandomSpawnCheckPoint());
                FindObjectOfType<CameraZoom>().target = this.transform;
                FindObjectOfType<CameraZoom>().DoZoom();
            }
            if (!isBullying && playerIndex == 1)
            {
                SpawnFloatingText(redPointFloatingText, 1.5f, 1f);
                Instantiate(pointParticle, other.gameObject.transform.position, quaternion.identity);
                ShakeFeedback.PlayFeedbacks();
                checkPoint.spawnCheck = false;
                score.IncreasePlayer2Score(1);
                Destroy(other.gameObject, 0.2f);
                StartCoroutine(checkPoint.RandomSpawnCheckPoint());
                FindObjectOfType<CameraZoom>().target = this.transform;
                FindObjectOfType<CameraZoom>().DoZoom();
            }
        }
    }

    public void DeceaseHealth(float value)
    {
        Instantiate(Blood, transform.position, Quaternion.identity);
        GetComponent<BloodSpawner>().SpawnObjects();
        ShakeFeedback.PlayFeedbacks();
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
        transform.position = new Vector3(1000f, 1000f, 1f);
    }

    IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(respawnDelay);
        if (GameManager.Instance.GetComponentInChildren<TurnBasedManagement>().Timer > 0 && isDead)
        {
            float randomX = Random.Range(-spawnAreaWidth / 2, spawnAreaWidth / 2);
            float randomY = Random.Range(-spawnAreaHeight / 2, spawnAreaHeight / 2);

            transform.position = new Vector3(randomX, randomY, 1f);
            isDead = false;
            currentHealth = maxHealth;
            
            // Instantiate(playerPrefab, transform.position, quaternion.identity, transform);
            
            // find new micro bar assign it!
            _microBar = GetComponentInChildren<MicroBar>();
            _microBar.Initialize(currentHealth);
            
            if (playerIndex == 1 && !isBullying) // if player1 is bullying then player 1 will get score by bully player 2
            {
                print($"Player 1 get score");
                SpawnFloatingText(bluePointFloatingText, 1.5f, 1f);
                GameManager.Instance.GetComponent<ScoreCount>().IncreasePlayer1Score(1);
            }

            if (playerIndex == 0 && !isBullying) // if player2 is bullying then player 2 will get score by bully player 1
            {
                print($"Player 2 get score");
                SpawnFloatingText(redPointFloatingText, 1.5f, 1f);
                GameManager.Instance.GetComponent<ScoreCount>().IncreasePlayer2Score(1);
            }
        }
    }

    public void SpawnFloatingText(GameObject prefab, float height , float delay)
    {
        var textTrans = new Vector3(transform.position.x, transform.position.y + height, transform.position.z);
        var text = Instantiate(prefab, textTrans, quaternion.identity, canvas.transform);
        
        Destroy(text, delay);
    }
}
