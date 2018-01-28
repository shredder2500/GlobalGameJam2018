using UnityEngine;

public class AddAmmo : MonoBehaviour
{
    public void Add(int amount)
    {
        FindObjectOfType<CannonFireControl>().AddAmmo(amount);
    }
}
