using System;
using UnityEngine;

[RequireComponent(typeof(Jumper))]
[RequireComponent(typeof(Rotator))]
public class Player : Unit
{
    public event Action IsDestroyed;

    public Rotator Rotator { get; private set; }

    public Jumper Jumper { get; private set; }

    private void Awake()
    {
        Rotator = GetComponent<Rotator>();

        Jumper = GetComponent<Jumper>();
    }

    public void Disappear()
    {
        gameObject.SetActive(false);

        IsDestroyed?.Invoke();
    }

    public void Reset()
    {
        Jumper.RemoveInertia();

        Rotator.SetNormalPosition();
    }
}