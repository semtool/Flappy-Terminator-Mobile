using System;
using UnityEngine;

public class ÑharacterCollisionDetector : MonoBehaviour
{
    public event Action<Unit> IsTouched;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerMissile playerMissile))
        {
            if (gameObject.TryGetComponent(out Unit unit))
            {
                if (unit is Enemy)
                {
                    IsTouched?.Invoke(unit);
                }
            }
        }

        if (collision.gameObject.TryGetComponent(out EnemyMissile enemyMissile))
        {
            if (gameObject.TryGetComponent(out Unit unit))
            {
                if (unit is Player player)
                {
                    player.Disappear();
                }
            }
        }
    }
}