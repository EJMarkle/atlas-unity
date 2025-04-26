using UnityEngine;
using UnityEngine.Audio;



/// <summary>
/// Class for destructible block behavior
/// </summary>
public class DestructibleBlock : MonoBehaviour
{
    public int maxHP = 3; 
    public Material fullMaterial; 
    public Material halfMaterial;
    public Material lowMaterial;
    public float velocityThreshold = 5f; 

    private int currentHP; 
    private MeshRenderer meshRenderer; 
    private ScoreManager scoreManager; 
    public int pointsValue = 50;
    private GameObject audioManager; 
    private AudioSource[] audioSources;
    private bool isWood = false;


    private void Start()
    {
        currentHP = maxHP;
        meshRenderer = GetComponent<MeshRenderer>();
        UpdateBlockMaterial();

        scoreManager = FindObjectOfType<ScoreManager>();
        audioManager = GameObject.Find("AudioManager");
        audioSources = audioManager.GetComponents<AudioSource>();


        if (scoreManager == null)
        {
            Debug.LogWarning("[DestructibleBlock] ScoreManager not found in the scene!");
        }
        if (gameObject.name.Contains("Wood"))
        {
            isWood = true;
        }
        if (gameObject.name.Contains("Stone"))
        {
            pointsValue = 100;
        }
    }

    /// <summary>
    /// Decrements hp and calls UpdateBlockMaterial()
    /// </summary>
    public void TakeDamage(int damage)
    {
        currentHP -= damage; 

        if (currentHP < 0)
        {
            currentHP = 0;
        }

        UpdateBlockMaterial();
        if (currentHP == 1)
        {
            audioSources[0].Play();
        }

        // Play correct audio on hp change
        if (currentHP == 0)
        {
            if (isWood)
            {
                audioSources[2].Play();
            }
            else if (!isWood)
            {
                audioSources[1].Play();
            }
            gameObject.SetActive(false);

            if (scoreManager != null)
            {
                scoreManager.AddScore(pointsValue);
            }
        }
    }

    /// <summary>
    /// Updates object material to reflect object hp (mat set in editor)
    /// </summary>
    private void UpdateBlockMaterial()
    {
        if (currentHP == 3)
        {
            meshRenderer.material = fullMaterial;
        }
        else if (currentHP == 2)
        {
            meshRenderer.material = halfMaterial;
        }
        else if (currentHP == 1)
        {
            meshRenderer.material = lowMaterial;
        }
    }

    /// <summary>
    /// If destructible object collides with another object above the velocity threshold, decrement object hp
    /// </summary>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude >= velocityThreshold)
        {
            TakeDamage(1);
        }
    }
}
