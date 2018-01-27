using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    private float startTime;
    private float stageTime;
    public float tempTime;
    private bool warnState;
    private bool endState;
    private Text timerText;

	// Use this for initialization
	void Start () {
        //tempTime = GetComponent<float>();
        if (tempTime > 0)
        {
            stageTime = tempTime;
        } 
        startTime = Time.time;
        warnState = false;
        timerText = GetComponent<Text>();
        timerText.color = Color.blue;
	}
	
	// Update is called once per frame
	void Update () {
        stageTime -= Time.time - startTime;
        timerText.text = stageTime.ToString("f2");
        if (stageTime < 3 && stageTime > 0)
        {
            warnState = true;
            timerText.color = Color.yellow;
        }
        else if (stageTime <= 0)
        {
            endState = true;
            timerText.color = Color.red;
        }
	}

    void SetStageTime (float inTime)
    {
        startTime = Time.time;
        stageTime = inTime;
        warnState = false;
        endState = false;
    }

    bool GetWarnState()
    {
        return warnState;
    }

    bool GetEndState()
    {
        return endState;
    }
}
