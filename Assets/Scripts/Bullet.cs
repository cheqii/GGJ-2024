using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 targetPosition;
    public float curveSpeed = 1f;
    public float arrivalThreshold = 0.1f;
    public int damage = 0;
    public GameObject AttackArea;
    public GameObject AttackFx;
    
    
    void Start()
    {
        StartCoroutine(MoveToObject());
        Destroy(this.gameObject,2f);
    }

    IEnumerator MoveToObject()
    {
        float journeyLength = Vector2.Distance(transform.position, targetPosition);
        float startTime = Time.time;

        while (Vector2.Distance(transform.position, targetPosition) > arrivalThreshold)
        {
            float distCovered = (Time.time - startTime) * curveSpeed;
            float fracJourney = distCovered / journeyLength;
            transform.position = Vector2.Lerp(transform.position, targetPosition, fracJourney);

            yield return null;
        }

        // Object has reached the target position
        // Call your blank method here
        Reach();
    }

    void Reach()
    {
        var atk = Instantiate(AttackArea, transform.position, Quaternion.identity);
        atk.GetComponent<DamageArea>().damageAmount = damage;
        Instantiate(AttackFx, transform.position, Quaternion.identity);

    }
}