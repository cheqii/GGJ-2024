using UnityEngine;

public class BloodSpawner : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public SpriteRenderer spriteRenderer;
    public int numberOfObjectsToSpawn = 10;
    public float minRotation = 0f;
    public float maxRotation = 360f;



    public void SpawnObjects()
    {
        if (prefabToSpawn == null || spriteRenderer == null)
        {
            Debug.LogError("Prefab or SpriteRenderer not set!");
            return;
        }

        Bounds spriteBounds = spriteRenderer.bounds;

        for (int i = 0; i < numberOfObjectsToSpawn; i++)
        {
            // Generate random position within sprite bounds
            float randomX = Random.Range(spriteBounds.min.x, spriteBounds.max.x);
            float randomY = Random.Range(spriteBounds.min.y, spriteBounds.max.y);
            Vector3 randomPosition = new Vector3(randomX, randomY, 0f);

            // Generate random rotation
            float randomRotation = Random.Range(minRotation, maxRotation);

            // Instantiate the prefab at the random position with random rotation
            GameObject spawnedObject = Instantiate(prefabToSpawn, randomPosition, Quaternion.Euler(0f, 0f, randomRotation));

            // Optionally, you can parent the spawned objects to this GameObject
            spawnedObject.transform.parent = transform;
            
            
            Destroy(spawnedObject,2f);
        }
    }
}