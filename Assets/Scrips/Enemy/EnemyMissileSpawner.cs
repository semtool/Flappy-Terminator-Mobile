using System.Collections;
using UnityEngine;

public class EnemyMissileSpawner : MonoBehaviour
{
    [SerializeField] private TouchDetector _detector;
    [SerializeField] private EnemyMissilePool _enemyMissilepool;
    [SerializeField] private float _launchInterval;

    private WaitForSeconds _wait;

    private void Awake()
    {
        _wait = new WaitForSeconds(_launchInterval);
    }

    private void OnEnable()
    {
        _detector.IsTouched += _enemyMissilepool.PutObjectToPool;

        _enemyMissilepool.ObjectIsInPool += UnsubscribeFromEvent;
    }

    public IEnumerator LaunchMissiles(Enemy enemy)
    {
        while (true)
        {
            var spawnPosition = enemy.transform.position;

            CreateMissile(spawnPosition);

            yield return _wait;
        }
    }

    public void CreateMissile(Vector2 vector)
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