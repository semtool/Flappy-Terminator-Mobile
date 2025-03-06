using System;
using UnityEngine;

public class TouchDetector : MonoBehaviour
{
    public event Action<Vector2> CordinatesHasGot;
    public event Action<Unit> IsTouched;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out SpawnTrigger trigger))
        {
            CordinatesHasGot?.Invoke(collision.gameObject.transform.position);
        }

        if (collision.gameObject.TryGetComponent(out Unit unit))
        {
            if (unit is Enemy enemy)
            {
                IsTouched?.Invoke(enemy);
            }

            if (unit is PlayerMissile rocket)
            {
                IsTouched?.Invoke(rocket);
            }

            if (unit is EnemyMissile missile)
            {
                IsTouched?.Invoke(missile);
            }
        }
    }
}