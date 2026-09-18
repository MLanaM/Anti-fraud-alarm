using UnityEngine;

public class BurglarMovement : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Vertical = nameof(Vertical);

    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _moveSpeed;

    private float _rotation;
    private float _direction;
    private float _distance;

    private void Update()
    {
        Rotate();
        Move();
    }

    private void Rotate()
    {
        _rotation = Input.GetAxis(Horizontal);

        transform.Rotate(Vector3.up * _rotation * _rotateSpeed * Time.deltaTime);
    }

    private void Move()
    {
        _direction = Input.GetAxis(Vertical);
        _distance = _direction * _moveSpeed * Time.deltaTime;

        transform.Translate(Vector3.forward * _distance);
    }
}
