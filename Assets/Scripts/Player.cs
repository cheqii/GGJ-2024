using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    
    public float spawnAreaWidth = 10f;
    public float spawnAreaHeight = 5f;

    public bool isDead;
    
    void Start()
    {
        currentHealth = maxHealth;
    }
    
    void Update()
    {
        if (currentHealth <= 0)
        {
            isDead = true;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            StartCoroutine(RespawnPlayer());
        }
        else gameObject.GetComponent<SpriteRenderer>().enabled = true;
        
        if(Input.GetKeyDown(KeyCode.Space)) DeceaseHealth(10);

        // if (GameManager.Instance.GetComponentInChildren<TurnBasedManagement>().Timer > 0) StartCoroutine(RespawnPlayer());
    }

    public void DeceaseHealth(float value)
    {
        if (currentHealth > 0) currentHealth -= value;
        if (currentHealth <= 0) {currentHealth = 0;}
    }

    IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(1f);
        if (GameManager.Instance.GetComponentInChildren<TurnBasedManagement>().Timer > 0 && isDead)
        {
            float randomX = Random.Range(-spawnAreaWidth / 2, spawnAreaWidth / 2);
            float randomY = Random.Range(-spawnAreaHeight / 2, spawnAreaHeight / 2);

            transform.position = new Vector3(randomX, randomY, 0f);
            isDead = false;
            currentHealth = maxHealth;
        }
    }
}
