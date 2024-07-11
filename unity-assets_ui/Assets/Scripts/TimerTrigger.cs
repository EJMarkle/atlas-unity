using UnityEngine;

/// <summary>
/// TimerTrigger functionality
/// </summary>
public class TimerTrigger : MonoBehaviour
{
    public Timer timerScript;

    /// <summary>
    /// enable Timer script when Player no longer collides w TimerTrigger object
    /// </summary>
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Debug.Log("player exited trigger");

            if (timerScript != null)
            {
                timerScript.enabled = true;
                // Debug.Log("timer script enabled");
            }
        }
    }
}
