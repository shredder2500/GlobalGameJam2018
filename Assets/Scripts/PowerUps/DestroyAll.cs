using System.Collections;
using System.Linq;
using UnityEngine;

public class DestroyAll : MonoBehaviour
{
    [SerializeField]
    private Projectile _projectilePrefab;
    private int _numberOfProjectiles = 12;

    private void OnEnable()
    {
        GetComponent<Collider2D>().enabled = true;
    }

    public void Execute()
    {
        GetComponent<Collider2D>().enabled = false;
        for (float i = 0; i < _numberOfProjectiles; i++)
        {
            var instance = ObjectPool.Main.GetObjectInstance(Projectile.POOL_NAME, () => Instantiate(_projectilePrefab).gameObject);
            instance.transform.position = transform.position;
            instance.transform.rotation = Quaternion.Euler(0, 0, i * 30);
        }
    }

    private IEnumerator SpawnBullets(Vector3 position)
    {
        for (float i = 0; i < _numberOfProjectiles; i++)
        {
            yield return new WaitForEndOfFrame();
            var instance = ObjectPool.Main.GetObjectInstance(Projectile.POOL_NAME, () => Instantiate(_projectilePrefab).gameObject);
            instance.transform.position = transform.position;
            instance.transform.rotation = Quaternion.Euler(0, 0, i * 30);
        }
    }
}
