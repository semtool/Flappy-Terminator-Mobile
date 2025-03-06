using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _offsetPosition;

    private void Update()
    {
        var position = transform.position;

        position.x = _player.transform.position.x + _offsetPosition;

        transform.position = position;
    }
}