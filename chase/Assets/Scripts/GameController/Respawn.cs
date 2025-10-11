using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound;
    private Transform currCheckpoint;
    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
        currCheckpoint = null;
    }

    public void SetCheckpoint(Transform checkpoint)
    {
        currCheckpoint = checkpoint;
        if (checkpointSound != null)
        {
            AudioSource.PlayClipAtPoint(checkpointSound, transform.position);
        }
        // Debug.Log("checkpoint set")
    }
    public void RespawnPlayer()
    {
        if (currCheckpoint != null)
        {
            transform.position = currCheckpoint.position;
        } else
        {
            transform.position = startPosition;
        }

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }
        
    }
}
