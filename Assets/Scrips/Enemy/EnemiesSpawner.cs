using System;
using System.Collections;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField] private EnemyMissileSpawner _enemyMissileSpawner;
    [SerializeField] private TouchDetector _inputTrigger;
    [SerializeField] private TouchDetector _outputTrigger;
    [SerializeField] private EnemiesPool _enemiesPool;
    [SerializeField] private float _minOffsetOfPositionFirst;
    [SerializeField] private float _maxOffsetOfPositionFirst;
    [SerializeField] private float _minOffsetOfPositionSecond;
    [SerializeField] private float _maxOffsetOfPositionSecond;

    private Coroutine _coroutineForLaunchEnemies;
    private int _startNunberOfCounter = 0;
    private int _numberOfEnemy = 2;
    private int _enemyCounter;
    private int _indexOfFirstEnemy = 0;
    private int _indexOfSocondEnemy = 1;

    public event Action<Vector2> CoordinatesIsGot;
    
    private void OnEnable()
    {
        _inputTrigger.CordinatesHasGot += LaunchEnemies;

        _outputTrigger.IsTouched += PootEnemyToPool;

        _enemiesPool.ObjectIsInPool += UnsubscribeFromEvent;
    }

    public void PootEnemyToPool(Unit unit)
    {
        _enemiesPool.PutObjectToPool(unit);
    }

    private void LaunchEnemies(Vector2 vector)
    {
        if (_coroutineForLaunchEnemies != null)
        {
            _coroutineForLaunchEnemies = null;
        }

        _coroutineForLaunchEnemies = StartCoroutine(CreateEnemies(vector));
    }

    private IEnumerator CreateEnemies(Vector2 vector)
    {
        _enemyCounter = _startNunberOfCounter;

        while (_enemyCounter < _numberOfEnemy)
        {
            Create(vector);

            _enemyCounter++;

            yield return null;
        }
    }

    private void Create(Vector2 vector)
    {
        Enemy enemy = _enemiesPool.GetObjectFromPool();

        SetStartCoordinates(enemy, vector);

        enemy.Mover.Fly();

        enemy.Shoot(_enemyMissileSpawner.LaunchMissiles(enemy));
    }

    private void SetStartCoordinates(Enemy enemy, Vector2 vector)
    {
        if (_enemyCounter == _indexOfFirstEnemy)
        {
            SetCoordinates(enemy, vector, _minOffsetOfPositionFirst, _maxOffsetOfPositionFirst);
        }

        if (_enemyCounter == _indexOfSocondEnemy)
        {
            SetCoordinates(enemy, vector, _minOffsetOfPositionSecond, _maxOffsetOfPositionSecond);
        }
    }

    private void SetCoordinates(Enemy enemy, Vector2 vector, float one, float two)
    {
        enemy.transform.position = new Vector2(vector.x, vector.y + GetRandomOffset(one, two));
    }

    private float GetRandomOffset(float minCoordinate, float maxCoordinate)
    {
        return UnityEngine.Random.Range(minCoordinate, maxCoordinate);
    }

    private void UnsubscribeFromEvent(Enemy enemy)
    {
        enemy.StopShoot();
    }

    private void OnDisable()
    {
        _inputTrigger.CordinatesHasGot -= LaunchEnemies;

        _outputTrigger.IsTouched -= PootEnemyToPool;

        _enemiesPool.ObjectIsInPool -= UnsubscribeFromEvent;
    }
}