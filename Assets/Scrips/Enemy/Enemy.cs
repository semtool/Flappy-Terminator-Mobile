using System.Collections;
using UnityEngine;

public class Enemy : Unit
{
    private Coroutine _coroutine;

    public void Shoot(IEnumerator shootingCoroutine )
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(shootingCoroutine);
        }
    }

    public void StopShoot()
    {
        StopCoroutine(_coroutine);

        _coroutine = null;
    }
}