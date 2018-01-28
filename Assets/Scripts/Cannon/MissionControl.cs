using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class MissionControl : MonoBehaviour, IHitable
{
    [SerializeField]
    private CannonAimControl _cannon;
    [SerializeField]
    private CannonFireControl _cannonFire;

    [SerializeField]
    private FloatEvent _onCommanQueued;
    [SerializeField]
    private FloatEvent _onCommandDequeued;
    [SerializeField]
    private UnityEvent _onStart;

    [SerializeField]
    private UnityEvent _onHit, _onMaxQueuedCommands;

    private Queue<float> _cannonCommands
        = new Queue<float>();

    private bool _isExecuting = false;

    public int QueuedCommandCount => _cannonCommands.Count;

    private void Start() => _onStart.Invoke();

    public void QueueFireAngle(float angle)
    {
        Debug.Log($"Attempting to queue command {angle}");
        if (_cannonFire.CurrentAmmo <= QueuedCommandCount) return;

        _cannonCommands.Enqueue(angle);
        _onCommanQueued.Invoke(angle);
        Debug.Log($"Queuing angle command {angle}");

        if (_cannonFire.CurrentAmmo <= QueuedCommandCount)
        {
            _onMaxQueuedCommands.Invoke();
        }
    }
    
    private void NextCommand()
    {
        Debug.Log($"Setting cannon angle to {_cannonCommands.Peek()}");
        _cannon.SetFireAngle(_cannonCommands.Peek());
    }

    private void Fire()
    {
        _cannonCommands.Dequeue();
        _cannonFire.Fire();
        _cannon.TargetLocked -= Fire;
        _isExecuting = false;
        _cannon.SetFireAngle(90);
    }

    public void Execute()
    {
        if (_isExecuting || !_cannonCommands.Any()) return;

        _isExecuting = true;
        _cannon.TargetLocked += Fire;
        NextCommand();
    }

    public void Hit()
    {
        _onHit.Invoke();
        Time.timeScale = 0;
    }
}
