using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class MissionControl : MonoBehaviour
{
    [SerializeField]
    private CannonAimControl _cannon;
    [SerializeField]
    private CannonFireControl _cannonFire;

    [SerializeField]
    private FloatEvent _onCommanQueued;
    [SerializeField]
    private FloatEvent _onCommandDequeued;

    private Queue<float> _cannonCommands
        = new Queue<float>();

    private bool _isExecuting = false;

    public int QueuedCommandCount => _cannonCommands.Count;

    public void QueueFireAngle(float angle)
    {
        if (_isExecuting || _cannonFire.CurrentAmmo == QueuedCommandCount) return;
        _cannonCommands.Enqueue(angle);
        _onCommanQueued.Invoke(angle);
        Debug.Log($"Queuing angle command {angle}");
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
}
