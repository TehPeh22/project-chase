using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject gameOverScreen;
    public GameObject victoryScreen;
    public GameObject player;
    public TMP_Text completionTimeText;

    void Start()
    {
        PlayerHealth.OnPlayerDied += GameOverScreen;
        if (victoryScreen != null)
        {
            victoryScreen.SetActive(false);
        }
    }

    void GameOverScreen()
    {
        PauseController.SetPause(true);
        StartCoroutine(AutoRespawn());
    }

    private IEnumerator AutoRespawn()
    {
        yield return new WaitForSecondsRealtime(2f);
        ResetGame();
    }

    public void ResetGame()
    {
        PauseController.SetPause(false); // Unpause when hit retry

        if (player != null)
        {
            Respawn playerRespawn = player.GetComponent<Respawn>();
            if (playerRespawn != null)
            {
                playerRespawn.RespawnPlayer();
            }

            // Reset Player health
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.ResetHealth();
            }
        }
    }

    public void ShowVictoryScreen()
    {
        if (victoryScreen != null)
        {
            victoryScreen.SetActive(true);
            PauseController.SetPause(true);

            if (Timer.Instance != null)
            {
                Timer.Instance.StopTimer();
            }
        }
    }

    public void RetryLevel()
    {
        PauseController.SetPause(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        PauseController.SetPause(false);
        SceneManager.LoadScene("StartScene");
    }   
}
