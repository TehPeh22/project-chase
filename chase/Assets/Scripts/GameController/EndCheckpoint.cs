using UnityEngine;

public class EndCheckpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameController gameController = FindFirstObjectByType<GameController>();
            if (gameController != null)
            {
                gameController.ShowVictoryScreen();
            }
        }
    }
}
