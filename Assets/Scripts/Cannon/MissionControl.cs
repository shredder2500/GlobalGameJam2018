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
    private UnityEvent _onHit;

    private Queue<float> _cannonCommands
        = new Queue<float>();
    private Queue<float> _nextCommands
        = new Queue<float>();

    private bool _isExecuting = false;

    public int QueuedCommandCount => _cannonCommands.Count;

    public void QueueFireAngle(float angle)
    {
        if (_cannonFire.CurrentAmmo == QueuedCommandCount) return;

        if (_isExecuting)
        {
            _nextCommands.Enqueue(angle);
        }
        else
        {
            _cannonCommands.Enqueue(angle);
            _onCommanQueued.Invoke(angle);
            Debug.Log($"Queuing angle command {angle}");
        }
    }
    
    private void NextCommand()
    {
        if(_cannonCommands.Any())
        {
            Debug.Log($"Setting cannon angle to {_cannonCommands.Peek()}");
            _cannon.SetFireAngle(_cannonCommands.Peek());
        }
        else
        {
            _cannon.TargetLocked -= Fire;
            _isExecuting = false;
            _cannon.SetFireAngle(90);

            while(_nextCommands.Any())
            {
                QueueFireAngle(_nextCommands.Dequeue());
            }
        }
    }

    private void Fire()
    {
        _cannonCommands.Dequeue();
        _cannonFire.Fire();
        NextCommand();
    }

    public void Execute()
    {
        if (_isExecuting) return;

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
