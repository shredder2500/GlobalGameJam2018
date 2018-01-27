using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MissionControl : MonoBehaviour
{
    [SerializeField]
    private CannonControl _cannon;

    private Queue<float> _cannonCommands
        = new Queue<float>();

    private bool _isExecuting = false;

    public void QueueFireAngle(float angle)
    {
        if (_isExecuting) return;
        _cannonCommands.Enqueue(angle);
        Debug.Log($"Queuing angle command {angle}");
    }
    
    private void NextCommand()
    {
        if(_cannonCommands.Any())
        {
            Debug.Log($"Setting cannon angle to {_cannonCommands.Peek()}");
            _cannon.SetFireAngle(_cannonCommands.Dequeue());
        }
        else
        {
            _cannon.TargetLocked -= NextCommand;
            _isExecuting = false;
        }
    }

    public void Execute()
    {
        if (_isExecuting) return;

        _isExecuting = true;
        _cannon.TargetLocked += NextCommand;
        NextCommand();
    }
}
