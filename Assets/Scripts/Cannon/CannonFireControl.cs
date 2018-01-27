using UnityEngine;
using UnityEngine.Events;

public class CannonFireControl : MonoBehaviour
{
    [SerializeField]
    private int _maxAmmo;
    [SerializeField]
    private Transform _projectSpawnPoint;
    [SerializeField]
    private Projectile _projectile;

    [SerializeField]
    private UnityEvent _onFire, _onOutOfAmmo, _onAmmoReset;

    private int _currentAmmo;

    public int CurrentAmmo => _currentAmmo;
    public int MaxAmmo => _maxAmmo;

    private void Awake() => _currentAmmo = _maxAmmo;

    public void Fire()
    {
        if (_currentAmmo <= 0) return;

        var instance = ObjectPool.Main.GetObjectInstance(Projectile.POOL_NAME, () => Instantiate(_projectile.gameObject, _projectSpawnPoint.position, _projectSpawnPoint.rotation));
        instance.GetComponent<Projectile>().Fire(_projectSpawnPoint.position, _projectSpawnPoint.rotation);
        _currentAmmo--;
        _onFire.Invoke();

        if(_currentAmmo == 0)
        {
            _onOutOfAmmo.Invoke();
        }
    }

    public void ResetAmmo()
    {
        _currentAmmo = _maxAmmo;
        _onAmmoReset.Invoke();
    }
}
