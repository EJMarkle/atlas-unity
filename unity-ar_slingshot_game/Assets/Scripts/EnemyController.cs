using UnityEngine;


/// <summary>
/// Class for enemy behavior
/// </summary>
public class EnemyController : MonoBehaviour
{
    public float velocityThreshold = 5f;
    public GameObject replacementPrefab; // holds dead enemy prefab

    private ScoreManager scoreManager;

    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    /// <summary>
    /// If enemy collides with object above velocity threshold, replaces enemy prefab with dead version and increments score
    /// </summary>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude >= velocityThreshold)
        {
            if (replacementPrefab != null)
            {
                scoreManager.AddScore(200);
                Instantiate(replacementPrefab, transform.position, transform.rotation);
            }
            else
            {
                Debug.LogWarning("[EnemyController] Replacement prefab is not assigned!");
            }
            Destroy(gameObject);
        }
    }
}
