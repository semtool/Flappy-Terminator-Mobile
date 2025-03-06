using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private KeyCode Jump = KeyCode.Space;
    private KeyCode Shoot = KeyCode.E;

    public event Action ShotWasFired;
    public event Action IsJumped;

    private void Update()
    {
        if (Input.GetKeyDown(Jump))
        {
            IsJumped?.Invoke();
        }

        if (Input.GetKeyDown(Shoot))
        {
            ShotWasFired?.Invoke();
        }
    }
}