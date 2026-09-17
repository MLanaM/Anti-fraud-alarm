using UnityEngine;

public class BurglarMovenemt : MonoBehaviour
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

        transform.Rotate(_rotation * _rotateSpeed * Time.deltaTime * Vector3.up);
    }

    private void Move()
    {
        _direction = Input.GetAxis(Vertical);
        _distance = _direction * _moveSpeed * Time.deltaTime;

        transform.Translate(_distance * Vector3.forward);
    }
}
