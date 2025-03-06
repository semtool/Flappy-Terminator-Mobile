using System;
using UnityEngine;

public class MissileCollisionDetector : MonoBehaviour
{
    public event Action<Unit> IsTouched;

     private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            if (gameObject.TryGetComponent(out Unit unit))
            {
                if (unit is PlayerMissile)
                {
                    IsTouched?.Invoke(unit);
                }
            }
        }

        if (collision.gameObject.TryGetComponent(out Player player))
        {
            if (gameObject.TryGetComponent(out Unit unit))
            {
                if (unit is EnemyMissile)
                {
                    IsTouched?.Invoke(unit);
                }
            }
        }
    }
}