using UnityEngine;
using TMPro;

/// <summary>
/// WinFlag functonality
/// </summary>
public class WinTrigger : MonoBehaviour
{
    public Timer timerScript;
    public TMP_Text timerText;

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
        }
    }
}
