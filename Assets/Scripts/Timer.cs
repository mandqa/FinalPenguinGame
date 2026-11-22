using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text TimerText;

    [SerializeField] private Transform Player;

    [SerializeField] private Transform[] SpawnPoints;

    private float timeRemaining = 60f;

    private bool timerRunning = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        TimerText.gameObject.SetActive(false); //hides timer when game starts
        UpdateTimerDisplay();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!timerRunning)
            return;
        //subtracts time as game runs
        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0)
        {
            //resets timer back to 60
            timeRemaining = 60f;
            //stops timer from counting down
            timerRunning = false;

            TimerText.gameObject.SetActive(false);
            
            RespawnPlayer();
        }

        UpdateTimerDisplay();
    }

    public void StartTimer()
    {
        timeRemaining = 60f;
        timerRunning = true;

        TimerText.gameObject.SetActive(true); //shows
        UpdateTimerDisplay();
    }

    //moves player to random spawn point
    private void RespawnPlayer()
    {
        if (SpawnPoints.Length == 0)
            return;

        //picks rand spawn point from array
        int randomSpawn = Random.Range(0, SpawnPoints.Length);
        //moves player to pos selected spawn point
        Player.position = SpawnPoints[randomSpawn].position;
    }

    private void UpdateTimerDisplay()
    {
        //rounds remaining time up to whole num
        int seconds = Mathf.CeilToInt(timeRemaining);

        //displays text
        TimerText.text = $"{seconds}";
    }
}
