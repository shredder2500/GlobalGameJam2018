using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class QueuedCommandIndicator : MonoBehaviour
{
    const string COMMAND_TEXT_POOL_NAME = "COMMAND_TEXT";

    [SerializeField]
    private Text _commandTextPrefab;
    [SerializeField]
    private Transform _parent;

    private Queue<GameObject> _instances
        = new Queue<GameObject>();

    public void AddCommandText(float command)
    {
        var instance = ObjectPool.Main.GetObjectInstance(COMMAND_TEXT_POOL_NAME, () => Instantiate(_commandTextPrefab, _parent).gameObject);
        instance.transform.SetParent(_parent);
        instance.GetComponent<Text>().text = command.ToString();
        _instances.Enqueue(instance);
    }

    public void RemoveCommnad()
    {
        if (!_instances.Any()) return;
        var instance = _instances.Dequeue();
        instance.transform.SetParent(null);
        ObjectPool.Main.PoolObject(COMMAND_TEXT_POOL_NAME, instance);
    }
}
