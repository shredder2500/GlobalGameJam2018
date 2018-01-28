using System;
using UnityEngine;
using UnityEngine.Events;

public class CannonAimControl : MonoBehaviour
{
    [SerializeField, Range(0, 90)]
    private float _maxAngle = 75;
    [SerializeField]
    private float _rotateSpeed = 5;

    [SerializeField]
    private AnimationCurve _fireAngleCurve = 
        new AnimationCurve(new Keyframe(0, 90), new Keyframe(.5f, 0), new Keyframe(1, -90));

    [SerializeField]
    private UnityEvent _onStartMove;
    [SerializeField]
    private UnityEvent _onEndMove;
    
    private float _fireAngle = 0;
    private bool _targetLocked = false;

    public event Action TargetLocked;
    
    private Quaternion CalcTargetQuaternion(Vector3 rotation) =>
        Quaternion.Euler(rotation.x, rotation.y, _fireAngle);
    
    private Quaternion CalcTargetQuaternion() =>
        CalcTargetQuaternion(transform.localRotation.eulerAngles);

    private float CurrentAngle(float value) => value > 180 ? -(360 - value) : value;
    private float CurrentAngle() => CurrentAngle(transform.localRotation.eulerAngles.z);

    private void Update()
    {
        if (!CloseEnough(CurrentAngle(), _fireAngle))
        {
            _targetLocked = false;
            transform.localRotation = Quaternion.Lerp(transform.localRotation, CalcTargetQuaternion(), _rotateSpeed * Time.deltaTime);
        }
        else if(!_targetLocked)
        {
            _targetLocked = true;
            _onEndMove.Invoke();
            TargetLocked?.Invoke();
        }
    }

    private bool CloseEnough(float a, float b, float value = .05f) => Math.Abs(a - b) <= value;

    public void SetFireAngle(float angle)
    {
        _targetLocked = false;
        _onStartMove.Invoke();
        _fireAngle = Mathf.Clamp(_fireAngleCurve.Evaluate(angle / 180f), -_maxAngle, _maxAngle);
    }
}
