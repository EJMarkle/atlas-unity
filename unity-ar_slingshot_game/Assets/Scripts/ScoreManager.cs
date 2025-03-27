using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GameManagement;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI ammoText;


    public int score = 0;
    public int ammo = 5;


    public void SpendAmmo()
    {
        Debug.Log("Ammo spent");
        ammo--;
    }

    public void AddScore(int points)
    {
        score += points;
        Debug.Log(points + " points added.");
    }
    

    void Update()
    {
        scoreText.text = score.ToString();
        ammoText.text = ammo.ToString();
    }
}
