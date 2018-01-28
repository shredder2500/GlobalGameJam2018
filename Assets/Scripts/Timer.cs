using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private Text timerText;

    [SerializeField]
    private UnityEvent onTimerComplete;
    [SerializeField]
    private UnityEvent _onEnterWarningTime;

    private float startTime;
    private float stageTime;
    private float runTime;
    private bool warnState;
    private bool endState;
	
	private void Update ()
    {
        runTime = Mathf.Clamp(stageTime - (Time.time - startTime), 0, 999);
        timerText.text = runTime.ToString("f2");
        CheckState();
    }

    private void CheckState()
    {
        if (CheckForWarnState())
        {
            _onEnterWarningTime.Invoke();
            warnState = true;
            timerText.color = Color.yellow;
        }
        else if (runTime <= 0 && !endState)
        {
            endState = true;
            timerText.color = Color.red;
            onTimerComplete.Invoke();
        }
    }

    private bool CheckForWarnState() => runTime < 5 && runTime > 0 && !warnState;

    public void SetStageTime (float inTime)
    {
        startTime = Time.time;
        stageTime = inTime;
        warnState = false;
        endState = false;
        timerText.color = Color.blue;
    }
}
