using System;
using UnityEngine;

public class MissileCollisionDetector : MonoBehaviour
{
    public event Action<Unit> IsTouched;
    public event Action<Unit> CharacterIsTouched;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Unit character))
        {
            if (character is Enemy)
            {
                CharacterIsTouched?.Invoke(character);
                
                if (gameObject.TryGetComponent(out Unit unit))
                {
                    if (unit is PlayerMissile)
                    {
                        IsTouched?.Invoke(unit);
                    }
                }
            }

            if (character is Player player)
            {
                if (gameObject.TryGetComponent(out Unit unit))
                {
                    if (unit is EnemyMissile)
                    {
                        player.Disappear();

                        IsTouched?.Invoke(unit);
                    }
                }
            }
        }
    }
}