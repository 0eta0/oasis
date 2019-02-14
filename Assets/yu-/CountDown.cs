using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour {
    [SerializeField]
    private Text timerText;
    [SerializeField]
    private int totalTime;

    private int currentTime;

    private static bool canStart;
    public static bool GetCanStart()
    {
        return canStart;
    }

	// Use this for initialization
	void Start () {
        currentTime = totalTime;
        timerText.text = currentTime.ToString();
        StartCoroutine("StartCountDown");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator StartCountDown()
    {
        while (currentTime >= 0)
        {
            yield return new WaitForSeconds(1f);
            currentTime -= 1;

            if (currentTime == 0)
                timerText.text = "Start";
            else
                timerText.text = currentTime.ToString();
        }
        canStart = true;
        gameObject.SetActive(false);
    }
}
