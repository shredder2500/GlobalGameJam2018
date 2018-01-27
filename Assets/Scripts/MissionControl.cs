using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MissionControl : MonoBehaviour
{
    [SerializeField]
    private CannonControl _cannon;
    [SerializeField]
    private Transform _projectSpawnPoint;
    [SerializeField]
    private Projectile _projectile;

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
            _cannon.TargetLocked -= Fire;
            _isExecuting = false;
        }
    }

    private void Fire()
    {
        var instance = ObjectPool.Main.GetObjectInstance(Projectile.POOL_NAME, () => Instantiate(_projectile.gameObject, _projectSpawnPoint.position, _projectSpawnPoint.rotation));
        instance.GetComponent<Projectile>().Fire(_projectSpawnPoint.position, _projectSpawnPoint.rotation);
    }

    public void Execute()
    {
        if (_isExecuting) return;

        _isExecuting = true;
        _cannon.TargetLocked += NextCommand;
        _cannon.TargetLocked += Fire;
        NextCommand();
    }
}
