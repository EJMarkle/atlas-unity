using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float velocityThreshold = 5f;
    public GameObject replacementPrefab;

    private ScoreManager scoreManager;

    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }
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
