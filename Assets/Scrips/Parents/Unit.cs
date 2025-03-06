using UnityEngine;

[RequireComponent(typeof(UniversalMover))]
[RequireComponent(typeof(MissileCollisionDetector))]
[RequireComponent(typeof(�haracterCollisionDetector))]
public abstract class Unit : MonoBehaviour
{
    public UniversalMover Mover { get; private set; }

    public MissileCollisionDetector Detector { get; private set; }

    public �haracterCollisionDetector SelfDetector { get; private set; }

    private void Awake()
    {
        Mover = GetComponent<UniversalMover>();

        Detector = GetComponent<MissileCollisionDetector>();

        SelfDetector = GetComponent<�haracterCollisionDetector>();
    }
}