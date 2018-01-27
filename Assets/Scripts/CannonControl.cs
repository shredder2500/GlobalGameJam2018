using System;
using UnityEngine;

public class CannonControl : MonoBehaviour
{
    [SerializeField, Range(0, 90)]
    private float _maxAngle = 75;
    [SerializeField]
    private float _rotateSpeed = 5;
    private AnimationCurve _fireAngleCurve = 
        new AnimationCurve(new Keyframe(0, 90), new Keyframe(.5f, 0), new Keyframe(1, -90));

    private float _fireAngle = 0;
    
    private Quaternion CalcTargetQuaternion(Vector3 rotation) =>
        Quaternion.Euler(rotation.x, rotation.y, Mathf.Clamp(_fireAngle, -_maxAngle, _maxAngle));
    
    private Quaternion CalcTargetQuaternion() =>
        CalcTargetQuaternion(transform.rotation.eulerAngles);

    private void Update() =>
        transform.rotation = Quaternion.Lerp(transform.rotation, CalcTargetQuaternion(), _rotateSpeed * Time.deltaTime);

    public void SetFireAngle(float angle) => _fireAngle = _fireAngleCurve.Evaluate(angle / 180);
}
