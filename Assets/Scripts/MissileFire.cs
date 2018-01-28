using UnityEngine;
using UnityEngine.Events;

public class MissileFire : MonoBehaviour, IHitable
{
    public const string MISSILE_POOL_NAME = "MISSILE_POOL_NAME";

    [SerializeField]
    private int _killPoints = 1;
    [SerializeField]
    private LayerMask _hitMask;
    [SerializeField]
    private IntEvent _onHit;
    public UnityEvent<int> OnHit => _onHit;

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
        Move();

        CheckForHit();
    }

    private void CheckForHit()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.zero - transform.position, 1, _hitMask);
        if (hit.collider != null)
        {
            hit.collider.GetComponent<IHitable>()?.Hit();
        }
    }

    private void Move()
    {
        float distCovered = (Time.time - startTime) / _time;
        float fracJourney = distCovered / journeyLength;
        transform.position = Vector3.Lerp(startMarker, endMarker, fracJourney);
    }

    public void SetMissile (Vector3 startVal, Vector3 endVal, float time)
    {
        transform.position = startVal;
        //transform.up = endVal - startVal;
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
        _onHit?.Invoke(_killPoints);
        _onHit.RemoveAllListeners();
        ObjectPool.Main.PoolObject(MISSILE_POOL_NAME, gameObject);
    }
}