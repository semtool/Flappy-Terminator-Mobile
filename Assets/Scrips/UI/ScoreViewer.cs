using UnityEngine;
using TMPro;

public class ScoreViewer : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private EnemiesSpawner _spawner;

    private int _fragsCounter = 0;

    private void OnEnable()
    {
        _spawner.EnemyIsDestroyed += ShowScore;
    }

    private void ShowScore()
    {
        _fragsCounter++;

        _text.text = _fragsCounter.ToString();
    }
}