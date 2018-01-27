using UnityEngine;

public class MissleControl : MonoBehaviour
{
    private float _fireAngle = 0;

    private void Update()
    {
        
    }

    public void SetFireAngle(float angle) => _fireAngle = angle;
}
