using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform _doorTransform;

    private float _openedState = -45f;
    private float _closedState = 0f;
    private float _speed = 90f;

    private Coroutine _doorMovingCoroutine;
    private int _hinderCount = 0;

    private void OnDisable()
    {
        TryStop();
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        _hinderCount++;

        if (_hinderCount > 1)
            return;

        TryStop();
        _doorMovingCoroutine = StartCoroutine(TurnDoor(_openedState));
    }

    private void OnTriggerExit(Collider otherCollider)
    {
        _hinderCount = Mathf.Max(0, _hinderCount - 1);

        if (_hinderCount > 0)
            return;

        TryStop();
        _doorMovingCoroutine = StartCoroutine(TurnDoor(_closedState));
    }

    private IEnumerator TurnDoor(float target)
    {
        float rotationY = _doorTransform.localEulerAngles.y;

        while (Mathf.DeltaAngle(rotationY, target) != 0)
        {
            rotationY = Mathf.MoveTowardsAngle(rotationY, target, _speed * Time.deltaTime);
            Vector3 rotation = _doorTransform.localEulerAngles;
            rotation.y = rotationY;
            _doorTransform.localEulerAngles = rotation;

            yield return null;
        }
    }

    private void TryStop()
    {
        if (_doorMovingCoroutine == null)
            return;

        StopCoroutine(_doorMovingCoroutine);
        _doorMovingCoroutine = null;
    }
}