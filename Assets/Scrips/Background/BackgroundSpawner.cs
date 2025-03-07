using UnityEngine;

public class BackgroundSpawner : MonoBehaviour
{
    [SerializeField] private SlideCollisionDetector _firstSlide;
    [SerializeField] private SlideCollisionDetector _secondSlide;

    private float _slideSize = 9.2f;

    private void OnEnable()
    {
        _firstSlide.IsTouched += MoveSecondSlide;

        _secondSlide.IsTouched += MoveFirstSlide;
    }

    public void MoveSecondSlide(Vector2 vector)
    {
        _secondSlide.transform.position = new Vector2(vector.x + _slideSize, vector.y);
    }

    public void MoveFirstSlide(Vector2 vector)
    {
        _firstSlide.transform.position = new Vector2(vector.x + _slideSize, vector.y);
    }

    private void OnDisable()
    {
        _firstSlide.IsTouched -= MoveSecondSlide;

        _secondSlide.IsTouched -= MoveFirstSlide;
    }
}