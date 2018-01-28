using System.Linq;
using UnityEngine;

public class DestroyAll : MonoBehaviour
{
    public void Execute() => GameObject.FindObjectsOfType<MissileFire>()
        .ForEach(enemy => enemy.Kill());
}
