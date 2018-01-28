using UnityEngine;

[System.Serializable]
public class WeightedSpawn
{
    [SerializeField]
    private GameObject _obj;
    [SerializeField]
    private int _weight;

    public GameObject Object => _obj;
    public int Weight => _weight;
}
