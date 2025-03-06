using System.Collections;
using UnityEngine;

public class EnemyMissileSpawner : MonoBehaviour
{
    [SerializeField] private TouchDetector _detector;
    [SerializeField] private EnemyMissilePool _enemyMissilepool;

    private WaitForSeconds _wait;
    private int _launchInterval = 1;

    private void Awake()
    {
        _wait = new WaitForSeconds(_launchInterval);
    }

    private void OnEnable()
    {
        _detector.IsTouched += _enemyMissilepool.PutObjectToPool;
        _enemyMissilepool.ObjectIsInPool += UnsubscribeFromEvent;
    }

    public IEnumerator LaunchMissiles(Vector2 vector)
    {
        while (true)
        {
            yield return _wait;

            CreateMissile(vector);
        }
    }

    private void CreateMissile(Vector2 vector)
    {
        EnemyMissile missile = _enemyMissilepool.GetObjectFromPool();

        missile.transform.position = vector;

        missile.Detector.IsTouched += _enemyMissilepool.PutObjectToPool;

        missile.Mover.Fly();
    }

    private void UnsubscribeFromEvent(EnemyMissile missile)
    {
        missile.Detector.IsTouched -= _enemyMissilepool.PutObjectToPool;
    }

    private void OnDisable()
    {
        _enemyMissilepool.ObjectIsInPool -= UnsubscribeFromEvent;
    }
}