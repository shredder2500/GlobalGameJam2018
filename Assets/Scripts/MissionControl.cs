using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionControl : MonoBehaviour
{
    [SerializeField]
    private CannonControl _cannon;

    private Queue<float> _cannonCommands
        = new Queue<float>();

    public void QueueFireAngle(float angle)
        => _cannonCommands.Enqueue(angle);

    public void Execute()
    {

    }
}
