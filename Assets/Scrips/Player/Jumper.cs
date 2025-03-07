using UnityEngine;

[RequireComponent(typeof(InputReader))]
public class Jumper : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _force;

    private Rigidbody2D _rigidbody;
    private InputReader _inputReader;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        _inputReader = GetComponent<InputReader>();
    }

    private void OnEnable()
    {
        _inputReader.IsJumped += Jump;
    }

    public void RemoveInertia()
    {
        _rigidbody.velocity = Vector2.zero;
    }

    private void Jump()
    {
        _rigidbody.velocity = new Vector2(_speed, _force);
    }

    private void OnDisable()
    {
        _inputReader.IsJumped -= Jump;
    }
}