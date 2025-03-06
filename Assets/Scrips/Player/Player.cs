using System;
using UnityEngine;

[RequireComponent(typeof(InputReader))]
public class Player : Unit
{
    [SerializeField] private float _minRotationZ;
    [SerializeField] private float _maxRotationZ;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _speed;
    [SerializeField] private float _force;

    private Rigidbody2D _rigidbody;
    private InputReader _inputReader;
    private Quaternion _maxRotation;
    private Quaternion _minRotation;
    private float _rotationStateOfRest = 0;

    public event Action IsDestroyed;

    public float RotationSpeed { get; private set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _inputReader = GetComponent<InputReader>();
        _minRotation = Quaternion.Euler(0, 0, _minRotationZ);
        _maxRotation = Quaternion.Euler(0, 0, _maxRotationZ);
    }
    private void OnEnable()
    {
        _inputReader.IsJumped += Jump;
    }

    private void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, _minRotation, RotationSpeed * Time.deltaTime);
    }

    public void SetNormalPosition()
    {
        transform.rotation = Quaternion.Euler(_rotationStateOfRest, _rotationStateOfRest, _rotationStateOfRest);

        RotationSpeed = _rotationStateOfRest;

        _rigidbody.velocity = Vector2.zero;
    }

    public void Disappear()
    {
        gameObject.SetActive(false);

        IsDestroyed?.Invoke();
    }

    private void Jump()
    {
        _rigidbody.velocity = new Vector2(_speed, _force);

        RotationSpeed = _rotationSpeed;

        transform.rotation = _maxRotation;
    }
}