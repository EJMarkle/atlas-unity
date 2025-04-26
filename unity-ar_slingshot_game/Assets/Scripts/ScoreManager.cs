using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GameManagement;


/// <summary>
/// Main score manager class
/// </summary>
public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI ammoText;
    public int score = 0;
    public int ammo = 5;

    /// <summary>
    /// Decrement ammo
    /// </summary>
    public void SpendAmmo()
    {
        Debug.Log("Ammo spent");
        ammo--;
    }

    /// <summary>
    /// Increment score
    /// </summary>
    public void AddScore(int points)
    {
        score += points;
        Debug.Log(points + " points added.");
    }
    
    /// <summary>
    /// Update UI text to reflect data
    /// </summary>
    void Update()
    {
        scoreText.text = score.ToString();
        ammoText.text = ammo.ToString();
    }
}
