using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Timer))]
public class Manager : MonoBehaviour
{
    [SerializeField]
    private WeightedSpawn[] _enemyPrefab;
    [SerializeField]
    private int _missileCount;
    [SerializeField]
    private float _waveTime = 2;

    private Timer _timer;

    private int _score = 0;
    public int Score => _score;

    [SerializeField]
    private AnimationCurve _spawnCurve;

    private int _currentWave = 0;

    [SerializeField]
    private AnimationCurve spawnDegree =
        new AnimationCurve(new Keyframe(0, 90), new Keyframe(.5f, 0), new Keyframe(1, -90));

    private float CurrentAngle(float value) =>
        value > 180 ? -(360 - value) : value;

    private float CurrentAngle() =>
        CurrentAngle(transform.localRotation.eulerAngles.z);

    private void Start()
    {
        Time.timeScale = 1;
        _timer = GetComponent<Timer>();
        SpawnEnemies();
    }

    public void SpawnEnemies()
    {
        _currentWave++;
        for (var i = 0; i < _spawnCurve.Evaluate(_currentWave / 100); i++)
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

        var instance = ObjectPool.Main.GetObjectInstance(MissileFire.MISSILE_POOL_NAME, CreateEnemey);
        var enemy = instance.GetComponent<MissileFire>();
        enemy.SetMissile(transform.up * 10, Vector3.zero, _waveTime * 2);
        enemy.OnHit.AddListener(IncreaseScore);

    }

    private GameObject GetRandomEnemy() => WeightedRandomizer
        .From(_enemyPrefab.ToDictionary(value => value.Object, value => value.Weight))
        .TakeOne();

    private GameObject CreateEnemey()
    {
        var enemy = Instantiate(GetRandomEnemy());
        enemy.GetComponent<MissileFire>().OnKill += IncreaseScore;
        return enemy;
    }

    public void IncreaseScore(int amount) => _score += amount;

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
