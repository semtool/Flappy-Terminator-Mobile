using UnityEngine;

[RequireComponent(typeof(InputReader))]
public class Rotator : MonoBehaviour
{
    [SerializeField] private float _minRotationZ;
    [SerializeField] private float _maxRotationZ;
    [SerializeField] private float _rotationSpeed;

    private float _angleOfRest = 0;
    private float _realRotationSpeed;
    private InputReader _inputReader;
    private Quaternion _minRotation;
    private Quaternion _maxRotation;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();

        _minRotation = Quaternion.Euler(0, 0, _minRotationZ);

        _maxRotation = Quaternion.Euler(0, 0, _maxRotationZ);
    }

    private void OnEnable()
    {
        _inputReader.IsJumped += SetRotationParameters;
    }

    private void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, _minRotation, _realRotationSpeed * Time.deltaTime);
    }

    private void SetRotationParameters()
    {
        _realRotationSpeed = _rotationSpeed;
        transform.rotation = _maxRotation;
    }

    public void SetNormalPosition()
    {
        transform.rotation = Quaternion.Euler(_angleOfRest, _angleOfRest, _angleOfRest);

        _realRotationSpeed = _angleOfRest;
    }

    private void OnDisable()
    {
        _inputReader.IsJumped -= SetRotationParameters;
    }
}