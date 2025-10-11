using UnityEngine;

public class PauseController : MonoBehaviour
{
    // get publicly but can only set within this script
    public static bool IsGamePaused { get; private set; } = false;

    public static void SetPause(bool pause)
    {
        IsGamePaused = pause;
        Time.timeScale = pause ? 0f : 1f;
    }
}
