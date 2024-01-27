using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public Vector3 targetPosition;
    public float curveSpeed = 1f;
    public float arrivalThreshold = 0.1f;
    public int damage = 0;
    public GameObject TrapObject;
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
        var trap = Instantiate(TrapObject, transform.position, Quaternion.identity);

        trap.GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
        
        Instantiate(AttackFx, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}