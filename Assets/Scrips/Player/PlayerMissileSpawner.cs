using UnityEngine;

public class PlayerMissileSpawner : MonoBehaviour
{
    [SerializeField] private TouchDetector _tauchDetector;
    [SerializeField] private Player _player;
    [SerializeField] private Launcher _startPoint;
    [SerializeField] private PlayerMissilePool _missilesPool;

    private InputReader _inputReader;

    private void Awake()
    {
        _inputReader = _player.GetComponent<InputReader>();
    }

    private void OnEnable()
    {
        _inputReader.ShotWasFired += Launch;

        _tauchDetector.IsTouched += _missilesPool.PutObjectToPool;

        _missilesPool.ObjectIsInPool += UnsubscribeFromEvent;
    }

    private void Launch()
    {
        PlayerMissile missile = _missilesPool.GetObjectFromPool();

        SetStartDirection(missile);

        missile.Mover.Fly();

        missile.Detector.IsTouched += _missilesPool.PutObjectToPool;
    }

    private void SetStartDirection(PlayerMissile missile)
    {
        var position = _startPoint.transform.position;
        missile.transform.rotation = _player.transform.rotation;
        missile.transform.position = position;
    }

    private void UnsubscribeFromEvent(PlayerMissile missile)
    {
        missile.Detector.IsTouched -= _missilesPool.PutObjectToPool;
    }

    private void OnDisable()
    {
        _inputReader.ShotWasFired -= Launch;

        _tauchDetector.IsTouched -= _missilesPool.PutObjectToPool;

        _missilesPool.ObjectIsInPool -= UnsubscribeFromEvent;
    }
}