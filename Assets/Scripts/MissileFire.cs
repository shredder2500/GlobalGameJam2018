using UnityEngine;
using UnityEngine.Events;

public class MissileFire : MonoBehaviour, IHitable
{
    public const string MISSILE_POOL_NAME = "MISSILE_POOL_NAME";

    [SerializeField]
    private UnityEvent _onHit;

    private Vector3 startMarker;
    private Vector3 endMarker;
    private float _time = 5F;
    private float startTime;
    private float journeyLength;



    void Start()
    {
        startTime = Time.time;
        journeyLength = Vector3.Distance(startMarker, endMarker);
    }
    void Update()
    {
        float distCovered = (Time.time - startTime) / _time;
        float fracJourney = distCovered / journeyLength;
        transform.position = Vector3.Lerp(startMarker, endMarker, fracJourney);
    }

    void SetMissile (Vector3 startVal, Vector3 endVal, float time)
    {
        _time = time;
        startMarker = startVal;
        endMarker = endVal;
        startTime = Time.time;
        journeyLength = Vector3.Distance(startMarker, endMarker);
    }

   void SetTime (float stageTime)
    {
        _time = stageTime;
    }

    public void Hit()
    {
        _onHit.Invoke();
        ObjectPool.Main.PoolObject(MISSILE_POOL_NAME, gameObject);
    }
}