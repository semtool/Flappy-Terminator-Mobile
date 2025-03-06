using System;
using UnityEngine;

public class SlideCollisionDetector : MonoBehaviour
{
    public event Action<Vector2>IsTouched;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        { 
            IsTouched?.Invoke(transform.position);
        }
    }
}