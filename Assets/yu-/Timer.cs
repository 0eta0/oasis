using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    [SerializeField]
    private Text timerText;
    [SerializeField]
    private int totalTime;
    [SerializeField]
    private GameObject gameOverObj;

    private int minute;
    private float seconds;
    private PlayerController playerController;

    private static float currentTime;
    public static float GetCurrentTime()
    {
        return currentTime;
    }

    private static bool isGameOver;
    public static bool GetIsGameOver()
    {
        return isGameOver;
    }

    // Use this for initialization
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        gameOverObj.SetActive(false);
        minute = 0;
        seconds = 0f;

        currentTime = totalTime;
        timerText.text = "1:00";
    }

    void Update()
    {
        if (!CountDown.GetCanStart() || isGameOver)
            return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            isGameOver = true;
            Score.SetScore(playerController.GetfriendsCount() + 1);
            gameOverObj.SetActive(true);
            sendRate rate = FindObjectOfType<sendRate>();
            rate.sendScore(TitleTransition.GetName(), Score.GetScore(),"standard");
        }

        timerText.text = minute.ToString("0") + ":" + ((int)currentTime).ToString("0");
    }

}
