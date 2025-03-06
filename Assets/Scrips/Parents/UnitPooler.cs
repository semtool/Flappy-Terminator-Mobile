using System;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(ObjectPool<>))]
public class UnitPooler<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _prefabObject;
    [SerializeField] private int _capacity;
    [SerializeField] private int _maxSize;

    private ObjectPool<T> _objectPool;

    public event Action<T> ObjectIsInPool;

    public Vector3 Position { get; private set; }

    private void Awake()
    {
        _objectPool = new ObjectPool<T>(
            createFunc: () => Instantiate(_prefabObject),
            actionOnGet: (obj) => obj.gameObject.SetActive(true),
            PootToPool,
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: true,
            defaultCapacity: _capacity,
            maxSize: _maxSize);
    }

    public void PutObjectToPool(Unit unit)
    {
        if (unit is T poolObject)
        {
            ObjectIsInPool?.Invoke(poolObject);

            _objectPool.Release(poolObject);
        }
    }

    public T GetObjectFromPool()
    {
        return _objectPool.Get();
    }

    private void PootToPool(T obj)
    {
        obj.gameObject.SetActive(false);
    }
}