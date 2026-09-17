using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform _doorTransform;

    private float _openedState = -45f;
    private float _closedState = 0f;
    private float _movingSpeed = 90f;

    private float _rotationTarget;
    private float _rotationY;

    private void Awake() 
    { 
        _rotationTarget = _closedState; 
    }

    private void Update() 
    {
        _rotationY = _doorTransform.localEulerAngles.y; 

        if (_rotationY == _rotationTarget)
            return;

        _rotationY = Mathf.MoveTowardsAngle(_rotationY, _rotationTarget, _movingSpeed * Time.deltaTime); 
        _doorTransform.localEulerAngles = new Vector3(_doorTransform.eulerAngles.x, _rotationY, _doorTransform.eulerAngles.z); 
    }

    private void OnTriggerEnter(Collider otherCollider) =>
        _rotationTarget = _openedState;

    private void OnTriggerExit(Collider otherCollider) =>
        _rotationTarget = _closedState;
}
