using UnityEngine;


/// <summary>
/// GameAudio controller class
/// </summary>
public class GameAudio : MonoBehaviour
{
    public PlayerController playerController;
    public WinTrigger winTrigger;
    public GameObject runningSound;
    public GameObject runningStoneSound;
    public GameObject splatSound;
    public GameObject lvl1BGM;
    public GameObject victoryPiano;
    private bool isOnStone;

    // Inits
    void Start()
    {
        if (runningSound != null)
        {
            runningSound.SetActive(false);
        }

        if (runningStoneSound != null)
        {
            runningStoneSound.SetActive(false);
        }

        if (splatSound != null)
        {
            splatSound.SetActive(false);
        }

        if (victoryPiano != null)
        {
            victoryPiano.SetActive(false);
        }
    }

    // Play stone running sound if on stone else regular running sound
    void Update()
    {
        // Raycast downward to check if the player is on a stone surface
        RaycastHit hit;
        if (Physics.Raycast(playerController.transform.position, Vector3.down, out hit, 1f))
        {
            if (hit.collider.CompareTag("Stone"))
            {
                isOnStone = true;
            }
            else
            {
                isOnStone = false;
            }
        }

        // Play running sound if running, grounded, and not on stone
        if (playerController.isRunning && playerController.isGrounded && !isOnStone)
        {
            if (!runningSound.activeSelf) runningSound.SetActive(true);
            if (runningStoneSound.activeSelf) runningStoneSound.SetActive(false);
        }
        // Play running on stone sound if running, grounded, and on stone
        else if (playerController.isRunning && playerController.isGrounded && isOnStone)
        {
            if (!runningStoneSound.activeSelf) runningStoneSound.SetActive(true);
            if (runningSound.activeSelf) runningSound.SetActive(false);
        }
        // Stop all running sounds if not running or not grounded
        else
        {
            if (runningSound.activeSelf) runningSound.SetActive(false);
            if (runningStoneSound.activeSelf) runningStoneSound.SetActive(false);
        }

        // Play splat sound if player is splote
        if (playerController.isSplat && !splatSound.activeSelf)
        {
            splatSound.SetActive(true);
        }
        else if (!playerController.isSplat && splatSound.activeSelf)
        {
            splatSound.SetActive(false);
        }

        // Disable BGM and play win sting on win
        if (winTrigger.HasWon)
        {
            if (lvl1BGM.activeSelf)
            {
                lvl1BGM.SetActive(false);
            }

            if (!victoryPiano.activeSelf)
            {
                victoryPiano.SetActive(true);
            }
        }
    }
}
