using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageMovement : MonoBehaviour
{
    public List<Sprite> imageList;
    public float initialSpeed = 720.0f;
    public float acceleration = 10.0f;
    public int totalRounds = 20;
    public float easeInOutDuration = 1.0f; // Duration for ease-in-out curve

    private RectTransform rectTransform;
    [SerializeField] private int currentRound = 0;
    private bool isMoving = false; // Flag to check if the movement should start

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && currentRound < totalRounds && !isMoving)
        {
            StartMoving();
        }

        if (isMoving && currentRound < totalRounds)
        {
            MoveImage();
        }
    }

    void StartMoving()
    {
        isMoving = true;
        StartCoroutine(MoveRoutine());
    }

    void MoveImage()
    {
        if (currentRound == totalRounds - 1) // Check if it's the last lap
        {
            float t = Mathf.SmoothStep(0, 1, Mathf.PingPong(Time.time / easeInOutDuration, 1));
            float easedSpeed = Mathf.Lerp(initialSpeed, 0, t);
            float step = easedSpeed * Time.deltaTime;
            rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, new Vector2(rectTransform.anchoredPosition.x, 0), step);

            if (rectTransform.anchoredPosition.y == 0)
            {
                currentRound++;
                isMoving = false; // Stop movement after the last lap

                // Do something when the randomization is done here!
                Debug.Log("Final round result: " + imageList[currentRound % imageList.Count].name); // Use the current round as an index
                currentRound = 0; // Reset currentRound for the next iteration
            }
        }
        else
        {
            float t;

            if (currentRound < 5) // First 5 laps: Random slot machine-like speed changes
            {
                t = Mathf.SmoothStep(0, 1, Random.Range(0.0f, 1.0f));
            }
            else if (currentRound >= 5 && currentRound < 16) // Laps 6-15: Constant speed
            {
                t = 1.0f;
            }
            else // Laps 16-20: Random slot machine-like speed changes
            {
                t = Mathf.SmoothStep(0, 1, Random.Range(0.0f, 1.0f));
            }

            float easedSpeed = Mathf.Lerp(initialSpeed / 2, initialSpeed, t);
            float step = easedSpeed * Time.deltaTime;
            rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, new Vector2(rectTransform.anchoredPosition.x, -120), step);

            if (rectTransform.anchoredPosition.y == -120)
            {
                ChangeImage();
            }
        }
    }

    void ChangeImage()
    {
        int randomIndex = Random.Range(0, imageList.Count);
        GetComponent<Image>().sprite = imageList[randomIndex];

        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, 120);
        currentRound++;
    }

    IEnumerator MoveRoutine()
    {
        yield return new WaitForSeconds(Random.Range(0.1f, 1.0f)); // Random delay before starting the next round
    }
}