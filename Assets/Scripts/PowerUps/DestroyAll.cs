using System.Linq;
using UnityEngine;

public class DestroyAll : MonoBehaviour
{
    [SerializeField]
    private float _killDistance = 10;

    public void Execute() => GameObject.FindObjectsOfType<MissileFire>()
        .Where(enemy => Vector3.Distance(enemy.transform.position, transform.position) <= _killDistance)
        .ForEach(enemy => enemy.Kill());
}
