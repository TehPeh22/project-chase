using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 0;
    public bool timeIsRunning = true;
    public TMP_Text timeText;
    public static Timer Instance;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        timeIsRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeIsRunning)
        {
            if (timeRemaining >= 0)
            {
                timeRemaining += Time.deltaTime;
                DisplayTime(timeRemaining);
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float min = Mathf.FloorToInt(timeToDisplay / 60);
        float sec = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00} : {1:00}", min, sec);
    }

    public void StopTimer()
    {
        timeIsRunning = false;
    }
}
