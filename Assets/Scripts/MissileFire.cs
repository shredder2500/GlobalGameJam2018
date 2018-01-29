using System;
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
    [SerializeField]
    private UnityEvent _onSpawn;
    [SerializeField]
    private Transform _trail;
    
    [SerializeField]
    private float _speed = 1;
    public UnityEvent<int> OnHit => _onHit;
    public event Action<int> OnKill;

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
        RaycastHit2D hit = Physics2D.Raycast(transform.position, (Vector3.zero - transform.position).normalized, .25f, _hitMask);
        if (hit.collider != null)
        {
            hit.collider.GetComponent<IHitable>()?.Hit();
        }
    }

    private void Move()
    {
        float distCovered = (Time.time - startTime) / (_time / _speed);
        float fracJourney = distCovered / journeyLength;
        transform.position = Vector3.Lerp(startMarker, endMarker, fracJourney);
    }

    private void OnEnable() => _onSpawn.Invoke();

    public void SetTrailAngle(float rotation)
    {
        if (!_trail) return;
        _trail.rotation = Quaternion.Euler(0, 0, rotation);
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
        Kill();
    }

    public void Kill()
    {
        OnKill?.Invoke(_killPoints);
        _onHit.RemoveAllListeners();
        
    }

    public void PoolObject() => ObjectPool.Main.PoolObject($"{MISSILE_POOL_NAME}_{name}", gameObject);
}