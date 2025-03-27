using UnityEngine;
using UnityEngine.Events;

public class MenuManager : MonoBehaviour
{
    public UnityEvent OnGameStart; // Event for starting the game
    public GameObject mainMenu;

    // Reference to level buttons to enable/disable
    public GameObject levelButton1;
    public GameObject levelButton2;
    public GameObject levelButton3;

    public void StartGame()
    {
        Debug.Log("[MenuManager] Start button pressed.");
        OnGameStart?.Invoke(); // Trigger the event for GameManager
    }

    public void QuitGame()
    {
        Debug.Log("[MenuManager] Quit button pressed.");
        Application.Quit();

        // If running in the editor, stop play mode
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void ToggleMenu()
    {
        if (mainMenu == null)
        {
            Debug.LogError("[MenuManager] MainMenu GameObject is not assigned!");
            return;
        }

        // Toggle menu visibility
        bool isActive = mainMenu.activeSelf;
        mainMenu.SetActive(!isActive);

        Debug.Log($"[MenuManager] Toggling menu: {(isActive ? "Hiding" : "Showing")}");
    }

    public void LevelCheck()
    {
        // Check level completion status from PlayerPrefs
        bool level1Completed = PlayerPrefs.GetInt("Level01_completed", 0) == 1;
        bool level2Completed = PlayerPrefs.GetInt("Level02_completed", 0) == 1;

        // Enable or disable buttons based on level completion
        if (level1Completed)
        {
            levelButton2.SetActive(true); // Enable Level 2 button if Level 1 is completed
        }
        else
        {
            levelButton2.SetActive(false); // Disable Level 2 button if Level 1 is not completed
        }

        if (level2Completed)
        {
            levelButton3.SetActive(true); // Enable Level 3 button if Level 2 is completed
        }
        else
        {
            levelButton3.SetActive(false); // Disable Level 3 button if Level 2 is not completed
        }

        Debug.Log("[MenuManager] Level check completed.");
    }
}
