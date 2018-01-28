using UnityEngine;

[RequireComponent(typeof(Timer))]
public class Manager : MonoBehaviour
{
    [SerializeField]
    private MissileFire _enemyPrefab;
    [SerializeField]
    private int _missileCount;
    [SerializeField]
    private float _waveTime = 2;

    private Timer _timer;

    private AnimationCurve spawnDegree =
        new AnimationCurve(new Keyframe(0, 90), new Keyframe(.5f, 0), new Keyframe(1, -90));

    private float CurrentAngle(float value) =>
        value > 180 ? -(360 - value) : value;

    private float CurrentAngle() =>
        CurrentAngle(transform.localRotation.eulerAngles.z);

    private void Start()
    {
        _timer = GetComponent<Timer>();
        SpawnEnemies();
    }

    public void SpawnEnemies()
    {
        for (var i = 0; i < _missileCount; i++)
        {
            SpawnEnemy();
        }
        _timer.SetStageTime(_waveTime);
    }

    private Quaternion CalcTargetQuaternion(Vector3 rotation, float angle) =>
        Quaternion.Euler(rotation.x, rotation.y, angle);

    private Quaternion CalcTargetQuaternion(float angle) =>
        CalcTargetQuaternion(transform.localRotation.eulerAngles, angle);

    private void SpawnEnemy()
    {
        var spawnAngle = Mathf.Clamp(spawnDegree.Evaluate(Random.value), -75, 75);
        transform.rotation = CalcTargetQuaternion(spawnAngle);

        var instance = ObjectPool.Main.GetObjectInstance(MissileFire.MISSILE_POOL_NAME, () => Instantiate(_enemyPrefab).gameObject);
        instance.GetComponent<MissileFire>().SetMissile(transform.up * 10, Vector3.zero, _waveTime * 2);
    }
}
