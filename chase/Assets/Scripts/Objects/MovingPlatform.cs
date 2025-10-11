using UnityEngine;

public class movingPlatform : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float moveSpeed = 2f;

    private Vector3 nextPosition;
    void Start()
    {
        nextPosition = pointB.position;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, moveSpeed * Time.deltaTime);
        if (transform.position == nextPosition)
        {
            nextPosition = (nextPosition == pointA.position) ? pointB.position : pointA.position;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Platform collision with: " + collision.gameObject.name + " | Tag: " + collision.gameObject.tag);
        Debug.Log("Collision point count: " + collision.contactCount);

        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("NPC"))
        {
            Debug.Log("Parenting: " + collision.gameObject.name + " to " + transform.name);
            collision.gameObject.transform.SetParent(transform);
            Debug.Log("Parent set. Current parent: " + collision.gameObject.transform.parent.name);
        }
        else
        {
            Debug.Log("Tag check failed. Expected 'Player' or 'NPC', got: '" + collision.gameObject.tag + "'");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("NPC"))
        {
            if (collision.gameObject != null && collision.gameObject.transform.parent == transform)
            {
                collision.gameObject.transform.SetParent(null);
            }
        }
    }
}
