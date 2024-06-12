using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody rb;
    private int score = 0;
    public int health = 5;
    public TMP_Text scoreText;
    public TMP_Text healthText;
    public TMP_Text winLoseText;
    public GameObject winLoseBG;

    /// Preload get rigidbody component 
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        SetScoreText();
        SetHealthText();
    }

    /// X and Y axis movement
    void FixedUpdate()
    {
        float xMovement = Input.GetAxis("Horizontal");
        float yMovement = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(xMovement, 0f, yMovement) * speed;
        rb.velocity = movement;
    }

    
    void OnTriggerEnter(Collider other)
    {
        /// On collision with coin, increment player score and deactivate object
        if (other.gameObject.CompareTag("Pickup"))
        {
            score++;

            Debug.Log("Score: " + score);

            other.gameObject.SetActive(false);
            SetScoreText();
        }

        /// If player collides with trap, decrement health
        if (other.gameObject.CompareTag("Trap"))
        {
            health--;
  
            Debug.Log("Health: " + health);
            SetHealthText();
        }

        /// Win if collide with Goal
        if (other.gameObject.CompareTag("Goal"))
        {
            Debug.Log("You win!");
            ShowWinMessage();
            StartCoroutine(LoadScene(3f));

            health = 5;
            score = 0;
            SetHealthText();
            SetScoreText();
        }
    }
    
    /// check if dead and reload if
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LoadMenuScene();
        }

        if (health == 0)
        {
            Debug.Log("Game Over!");
            ShowGameOverMessage();

            StartCoroutine(LoadScene(3f));

            health = 5;
            score = 0;
            SetHealthText();
            SetScoreText();
        }
    }
    
    // updates the score text
    void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    // updates the health display
    void SetHealthText()
    {
        healthText.text = "Health: " + health.ToString();
    }

    // displays the win message
    void ShowWinMessage()
    {
        winLoseText.text = "You Win!";
        winLoseText.color = Color.black;
        winLoseBG.GetComponent<Image>().color = Color.green;
        winLoseBG.SetActive(true);
    }

    // displays game over message
    void ShowGameOverMessage()
    {
        winLoseText.text = "Game Over!";
        winLoseText.color = Color.white;
        winLoseBG.GetComponent<Image>().color = Color.red;
        winLoseBG.SetActive(true);
    }

    // wait coroutine
    IEnumerator LoadScene(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // loads mennu scene with esc
    void LoadMenuScene()
    {
        SceneManager.LoadScene("menu");
    }

}
