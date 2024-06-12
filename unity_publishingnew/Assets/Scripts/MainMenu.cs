using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// Main menu methods
public class MainMenu : MonoBehaviour
{  
    public Material trapMat;
    public Material goalMat;
    public Toggle colorblindMode;

    // sets colorblindmode off on start
    void Start()
    {
        // colorblindMode.isOn = false;
    }

    // starts maze scene
    public void PlayMaze()
    {
        if (colorblindMode.isOn)
        {
            trapMat.color = new Color32(255, 112, 0, 1);
            goalMat.color = Color.blue;
        }
        else
        {
            trapMat.color = new Color32(255, 0, 0, 1);
            goalMat.color = new Color32(0, 255, 0, 255);
        }

        SceneManager.LoadScene("maze");
    }

    // quits application
    public void QuitMaze()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
