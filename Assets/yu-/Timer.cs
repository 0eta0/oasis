using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    [SerializeField]
    private Text timerText;

    private int minute;
    private float seconds;
    private float oldSeconds;


    void Start()
    {
        minute = 0;
        seconds = 0f;
        oldSeconds = 0f;
    }

    void Update()
    {
        if (!CountDown.GetCanStart())
            return;
        seconds += Time.deltaTime;
        if (seconds >= 60f)
        {
            minute++;
            seconds = seconds - 60;
        }

        if ((int)seconds != (int)oldSeconds)
        {
            timerText.text = minute.ToString("00") + ":" + ((int)seconds).ToString("0");
        }
        oldSeconds = seconds;
    }
}
