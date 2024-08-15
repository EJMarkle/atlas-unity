using UnityEngine;
using TMPro;

/// <summary>
/// WinFlag functionality
/// </summary>
public class WinTrigger : MonoBehaviour
{
    public Timer timerScript;
    public TMP_Text timerText;

    private bool _hasWon = false; // Private win state

    /// <summary>
    /// Public getter for the win state
    /// </summary>
    public bool HasWon => _hasWon;

    /// <summary>
    /// Stop clock when Player collides with WinFlag
    /// </summary>
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (timerScript != null)
            {
                timerScript.StopTimer();
                timerScript.UpdateTextProperties(60f, Color.green);
            }
            
            _hasWon = true;
        }
    }
}
