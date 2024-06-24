using UnityEngine;
using TMPro;

/// <summary>
/// Timer class
/// </summary>
public class Timer : MonoBehaviour
{
    public bool isTimerRunning = false;
    private float timerValue = 0f;
    public TMP_Text timerText;

    /// <summary>
    /// Start timer
    /// </summary>

    void Start()
    {
        if (enabled)
        {
            StartTimer();
        }
    }

    /// <summary>
    /// Iter timer
    /// </summary>
    void Update()
    {
        if (isTimerRunning)
        {
            timerValue += Time.deltaTime;
            UpdateTimerText();
        }
    }

    /// <summary>
    /// Update timer text to timer val
    /// </summary>
    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timerValue / 60f);
        int seconds = Mathf.FloorToInt(timerValue % 60f);
        int milliseconds = Mathf.FloorToInt((timerValue * 100f) % 100f);

        if (timerText != null)
        {
            timerText.text = string.Format("{0}:{1:00}.{2:00}", minutes, seconds, milliseconds);
        }
    }

    public void StartTimer()
    {
        isTimerRunning = true;
        // Debug.Log("timer started");
    }

}
