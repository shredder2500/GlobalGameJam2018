using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    private float startTime;
    private float stageTime;
    private float runTime;
    public float tempTime;
    private bool warnState;
    private bool endState;
    [SerializeField]
    private Text timerText;
    [SerializeField]
    private UnityEvent onTimerComplete;

	// Use this for initialization
	void Start () {
        //tempTime = GetComponent<float>();
        if (tempTime > 0)
        {
            stageTime = tempTime;
        } 
        startTime = Time.time;
        warnState = false;
        //timerText = GetComponent<Text>();
        timerText.color = Color.blue;
	}
	
	// Update is called once per frame
	void Update () {
        runTime = Mathf.Clamp(stageTime - (Time.time - startTime), 0, 999);
        timerText.text = runTime.ToString("f2");
        if (runTime < 5 && runTime > 0)
        {
            warnState = true;
            timerText.color = Color.yellow;
        }
        else if (runTime <= 0)
        {
            endState = true;
            timerText.color = Color.red;
            onTimerComplete.Invoke();
        }
	}

    public void SetStageTime (float inTime)
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
