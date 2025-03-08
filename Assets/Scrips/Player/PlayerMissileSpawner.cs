using UnityEngine;
using System;

public class PlayerMissileSpawner : MonoBehaviour
{
    [SerializeField] private EnemiesSpawner _enemiesSpawner;
    [SerializeField] private TouchDetector _tauchDetector;
    [SerializeField] private Player _player;
    [SerializeField] private Launcher _startPoint;
    [SerializeField] private PlayerMissilePool _missilesPool;

    private InputReader _inputReader;

    public event Action EnemyIsDestroyed;

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

        missile.Detector.CharacterIsTouched += SendEnemyToPool;
    }

    private void SendEnemyToPool(Unit unit)
    {
        _enemiesSpawner.PootEnemyToPool(unit);

        EnemyIsDestroyed?.Invoke();
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

        missile.Detector.CharacterIsTouched -= SendEnemyToPool;
    }

    private void OnDisable()
    {
        _inputReader.ShotWasFired -= Launch;

        _tauchDetector.IsTouched -= _missilesPool.PutObjectToPool;

        _missilesPool.ObjectIsInPool -= UnsubscribeFromEvent;
    }
}