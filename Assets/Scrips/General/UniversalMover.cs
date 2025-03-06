using System.Collections;
using UnityEngine;

public class UniversalMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Vector2 vector;

    public void Fly()
    {
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        while (enabled)
        {
            transform.Translate(vector * _speed * Time.deltaTime, Space.Self);

            yield return null;
        }
    }
}