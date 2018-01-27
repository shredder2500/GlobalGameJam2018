using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {
    [SerializeField]
    private int missileCount;
    [SerializeField]
    private int level;
    private int powerupCount;
    private float levelTime;
    [SerializeField]
    private float diffMod;
    [SerializeField]
    private float initialTime;
    [SerializeField]
    private int proximity;

    private AnimationCurve spawnDegree =
        new AnimationCurve(new Keyframe(0, 90), new Keyframe(.5f, 0), new Keyframe(1, -90));

    private float CurrentAngle(float value)
    {
        if (value > 180) {
            return -(360 - value);
        }
        else
        {
            return value;
        }
    }

    private float CurrentAngle()
    {
        return CurrentAngle(transform.localRotation.eulerAngles.z);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void LevelInit()
    {
        level++;
    }

    public void SetSpawnPoint(float angle)
    {
        var spawnAngle = Random.Range(15, 165);
        Vector3.
        _fireAngle = Mathf.Clamp(_fireAngleCurve.Evaluate(angle / 180), -_maxAngle, _maxAngle);
    }

    void TimerSet()
    {
        levelTime = (initialTime - (level * diffMod) - proximity);
    }
}
