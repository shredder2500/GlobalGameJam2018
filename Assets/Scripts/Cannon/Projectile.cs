using UnityEngine;

public class Projectile : MonoBehaviour
{
    public const string POOL_NAME = "PROJECTILES";

    [SerializeField]
    private float _speed = 2;
    [SerializeField]
    private LayerMask _hitMask;

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
            CheckForHit();
        }
        else
        {
            ObjectPool.Main.PoolObject(POOL_NAME, gameObject);
        }
    }

    private Vector3 RaycastOrigin() => transform.position + (-(transform.up) * .5f);

    private void CheckForHit()
    {
        RaycastHit2D hit = Physics2D.Raycast(RaycastOrigin(), transform.up, 1, _hitMask);
        if(hit.collider != null)
        {
            ObjectPool.Main.PoolObject(POOL_NAME, gameObject);
            hit.collider.GetComponent<IHitable>()?.Hit();
        }
    }

    public void Fire(Vector3 startPosition, Quaternion rotation)
    {
        transform.position = startPosition;
        transform.rotation = rotation;
    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine(RaycastOrigin(), RaycastOrigin() + (transform.up));
    }
}
