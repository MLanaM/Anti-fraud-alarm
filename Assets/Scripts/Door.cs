using System;
using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform _doorTransform;

    private float _openedState = -45f;
    private float _closedState = 0f;
    private float _movingSpeed = 90f;

    private float _rotationTarget;
    private float _rotationY;

    private Coroutine _doorMovingCoroutine;
    private bool _isTargetAchieved = true;
    private int _hinderCount = 0;

    private void Awake()
    {
        _rotationTarget = _closedState;
    }

    private void OnDisable()
    {
        if (_doorMovingCoroutine == null)
            return;

        StopCoroutine(_doorMovingCoroutine);
        _doorMovingCoroutine = null;
    }

    private IEnumerator DoorTurning()
    {
        while (_isTargetAchieved == false)
        {
            _rotationY = _doorTransform.localEulerAngles.y;

            if (Mathf.Approximately(_rotationY, _rotationTarget))
            {
                _isTargetAchieved = true;
                yield break;
            }

            _rotationY = Mathf.MoveTowardsAngle(_rotationY, _rotationTarget, _movingSpeed * Time.deltaTime);
            _doorTransform.localEulerAngles = new Vector3(_doorTransform.localEulerAngles.x, _rotationY, _doorTransform.localEulerAngles.z);

            yield return null;
        }
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        _hinderCount++;

        if (_hinderCount > 1)
            return;

        _rotationTarget = _openedState;
        _isTargetAchieved = false;
        _doorMovingCoroutine = StartCoroutine(DoorTurning());
    }

    private void OnTriggerExit(Collider otherCollider)
    {
        _hinderCount = Mathf.Max(0, _hinderCount - 1);

        if (_hinderCount > 0)
            return;

        _rotationTarget = _closedState;
        _isTargetAchieved = false;
        _doorMovingCoroutine = StartCoroutine(DoorTurning());
    }
}
