using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AmmoIndicator : MonoBehaviour
{
    const string AMMO_ICON_POOL_NAME = "AMMO_ICON";
    const string AMMO_ICON_USED_POOL_NAME = "AMMO_USED_ICON";

    [SerializeField]
    private Transform _parent;
    [SerializeField]
    private RectTransform _ammoIconPrefab;
    [SerializeField]
    private RectTransform _ammoUsedIconPrefab;
    [SerializeField]
    private CannonFireControl _cannonFire;
    [SerializeField]
    private MissionControl _missionControl;

    private Queue<GameObject> _instances
        = new Queue<GameObject>();
    private Queue<GameObject> _usedInstances
        = new Queue<GameObject>();

    private void Start() => UpdateIcons();

    public void UpdateIcons()
    {
        Debug.Log("Updating Icons");
        while (_cannonFire.CurrentAmmo - _usedInstances.Count != _instances.Count
            || _usedInstances.Count != _missionControl.QueuedCommandCount)
        {
            UpdateUnusedIcons();
            UpdateUsedIcons();
        }
    }

    private void UpdateUsedIcons()
    {
        if (_usedInstances.Any() && _usedInstances.Count > _missionControl.QueuedCommandCount)
        {
            var instance = _usedInstances.Dequeue();
            instance.transform.SetParent(null);
            ObjectPool.Main.PoolObject(AMMO_ICON_USED_POOL_NAME, instance);
        }
        else if (_usedInstances.Count < _missionControl.QueuedCommandCount)
        {
            var instance = ObjectPool.Main.GetObjectInstance(AMMO_ICON_USED_POOL_NAME, () => Instantiate(_ammoUsedIconPrefab, _parent).gameObject);
            instance.transform.SetParent(_parent);
            _usedInstances.Enqueue(instance);
        }
    }

    private void UpdateUnusedIcons()
    {
        if (_instances.Any() && _instances.Count > _cannonFire.CurrentAmmo - _missionControl.QueuedCommandCount)
        {
            var instance = _instances.Dequeue();
            instance.transform.SetParent(null);
            ObjectPool.Main.PoolObject(AMMO_ICON_POOL_NAME, instance);
        }
        else if (_instances.Count < _cannonFire.CurrentAmmo - _missionControl.QueuedCommandCount)
        {
            var instance = ObjectPool.Main.GetObjectInstance(AMMO_ICON_POOL_NAME, () => Instantiate(_ammoIconPrefab, _parent).gameObject);
            instance.transform.SetParent(_parent);
            _instances.Enqueue(instance);
        }
    }
}
