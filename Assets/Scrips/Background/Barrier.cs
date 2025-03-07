using UnityEngine;

public class Barrier : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            player.Rotator.SetNormalPosition();

            player.Jumper.RemoveInertia();
        }
    }
}