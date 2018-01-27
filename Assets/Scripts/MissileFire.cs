using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileFire : MonoBehaviour {
    
    [SerializeField]
    private Vector3 startMarker;
    [SerializeField]
    private Vector3 endMarker;
    [SerializeField]
    private float time = 5F;
    private float startTime;
    private float journeyLength;

    void Start()
    {
        startTime = Time.time;
        journeyLength = Vector3.Distance(startMarker, endMarker);
    }
    void Update()
    {
        float distCovered = (Time.time - startTime) / time;
        float fracJourney = distCovered / journeyLength;
        transform.position = Vector3.Lerp(startMarker, endMarker, fracJourney);
    }

    void SetMissile (Vector3 startVal, Vector3 endVal)
    {
        startMarker = startVal;
        endMarker = endVal;
        startTime = Time.time;
        journeyLength = Vector3.Distance(startMarker, endMarker);
    }

   void SetTime (float stageTime)
    {
        time = stageTime;
    }






	
}