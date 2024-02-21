using MoreMountains.Feedbacks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    public float totalTime = 60f; // Set the total time in seconds
    private float currentTime;
    private bool isPaused = false;

    public TMP_Text timerText; // Reference to a UI text element to display the timer

    public MMF_Player feedback;

    void Start()
    {
        StartTimer(); // Start the timer when the script is first initialized
    }

    void Update()
    {
        if (!isPaused)
        {
            if (currentTime > 0f)
            {
                currentTime -= Time.deltaTime;
                UpdateTimerDisplay();
            }
            else
            {
                OnTimerEnd();
            }
        }
    }

    void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            timerText.text = Mathf.CeilToInt(currentTime).ToString();
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
        currentTime = totalTime;
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
        currentTime = totalTime;
        UpdateTimerDisplay(); // Update the display when resetting the timer
    }
}