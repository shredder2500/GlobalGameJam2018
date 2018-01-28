using System.Collections;
using UnityEngine;

public class Run : MonoBehaviour
{
    public static void Execute(Projectile prefab, Vector3 origin)
    {
        new GameObject("RUN", typeof(Run))
            .GetComponent<Run>()
            .StartRun(prefab, origin);
    }

    private void StartRun(Projectile prefab, Vector3 origin) => StartCoroutine(SpawnBullets(prefab, origin));

    private IEnumerator SpawnBullets(Projectile prefab, Vector3 origin)
    {
        for (float i = 0; i < 12; i++)
        {
            yield return new WaitForEndOfFrame();
            var instance = ObjectPool.Main.GetObjectInstance(Projectile.POOL_NAME, () => Instantiate(prefab).gameObject);
            instance.transform.position = origin;
            instance.transform.rotation = Quaternion.Euler(0, 0, i * 30);
        }
    }
}
