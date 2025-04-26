using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// Main menu manager class
/// </summary>
public class MenuManager : MonoBehaviour
{
    public UnityEvent OnGameStart;
    public GameObject mainMenu;
    public GameObject levelButton1;
    public GameObject levelButton2;
    public GameObject levelButton3;

    /// <summary>
    /// Starts main game scene
    /// </summary>
    public void StartGame()
    {
        Debug.Log("[MenuManager] Start button pressed.");
        OnGameStart?.Invoke();
    }

    /// <summary>
    /// Closes application
    /// </summary>
    public void QuitGame()
    {
        Debug.Log("[MenuManager] Quit button pressed.");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    /// <summary>
    /// Enables settings toggling
    /// </summary>
    public void ToggleMenu()
    {
        if (mainMenu == null)
        {
            Debug.LogError("[MenuManager] MainMenu GameObject is not assigned!");
            return;
        }

        bool isActive = mainMenu.activeSelf;
        mainMenu.SetActive(!isActive);

        Debug.Log($"[MenuManager] Toggling menu: {(isActive ? "Hiding" : "Showing")}");
    }

    /// <summary>
    /// Ensures UI buttons for selecting levels only display if level is passed
    /// </summary>
    public void LevelCheck()
    {
        bool level1Completed = PlayerPrefs.GetInt("Level01_completed", 0) == 1;
        bool level2Completed = PlayerPrefs.GetInt("Level02_completed", 0) == 1;


        if (level1Completed)
        {
            levelButton2.SetActive(true);
        }
        else
        {
            levelButton2.SetActive(false);
        }

        if (level2Completed)
        {
            levelButton3.SetActive(true);
        }
        else
        {
            levelButton3.SetActive(false);
        }

        Debug.Log("[MenuManager] Level check completed.");
    }
}
