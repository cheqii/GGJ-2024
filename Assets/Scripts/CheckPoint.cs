using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private GameObject checkPointPrefab;
    
    [Header("Random SpawnArea")]
    [SerializeField] private float spawnAreaWidth = 10f;
    [SerializeField] private float spawnAreaHeight = 5f;
    
    [SerializeField] private float respawnDelay;

    private GameObject checkPoint;

    [SerializeField] public bool spawnCheck;
    
    // Start is called before the first frame update
    void Start()
    {
        checkPoint = GameObject.FindWithTag("CheckPoint");
        StartCoroutine(RandomSpawnCheckPoint());
    }

    // Update is called once per frame
    void Update()
    {
        // StartCoroutine(RandomSpawnCheckPoint());
        // RandomSpawnCheckPoint();
    }

    public IEnumerator RandomSpawnCheckPoint()
    {
        yield return new WaitForSeconds(respawnDelay);
        if (checkPoint == null)
        {
            if (GameManager.Instance.GetComponentInChildren<TurnBasedManagement>().Timer > 0 && !spawnCheck)
            {
                float randomX = Random.Range(-spawnAreaWidth / 2, spawnAreaWidth / 2);
                float randomY = Random.Range(-spawnAreaHeight / 2, spawnAreaHeight / 2);

                var newRandomTransform = new Vector3(randomX, randomY, 1f);
                Instantiate(checkPointPrefab, newRandomTransform, quaternion.identity);
                spawnCheck = true;
            }
        }


        // GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        //
        // foreach (var player in players)
        // {
        //     var checkState = player.GetComponent<Player>();
        //     if (player != null && !checkState.IsBullying)
        //     {
        //         
        //     }
        // }
    }
}
