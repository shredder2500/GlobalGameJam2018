using UnityEngine;

public class Projectile : MonoBehaviour
{
    public const string POOL_NAME = "PROJECTILES";

    [SerializeField]
    private float _speed = 2;

    private Renderer _renderer;

    public void Start()
    {
        _renderer = gameObject.GetComponentInChildren<Renderer>();
    }

    private void Update()
    {
        if(_renderer.isVisible)
        {
            transform.position += transform.up * _speed * Time.deltaTime;
        }
        else
        {
            ObjectPool.Main.PoolObject(POOL_NAME, gameObject);
        }
    }

    public void Fire(Vector3 startPosition, Quaternion rotation)
    {
        transform.position = startPosition;
        transform.rotation = rotation;
    }
}
