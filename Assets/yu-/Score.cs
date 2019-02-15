using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour {
    private static int score;
    public static int GetScore()
    {
        return score;
    }
    public static void SetScore(int value)
    {
        score = value * 100;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
