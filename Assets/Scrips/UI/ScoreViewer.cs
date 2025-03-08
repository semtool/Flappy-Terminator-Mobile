using UnityEngine;
using TMPro;

public class ScoreViewer : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private EnemiesSpawner _spawner;
    [SerializeField] private PlayerMissileSpawner _playerMissileSpawner; 

    private int _fragsCounter = 0;

    private void OnEnable()
    {
        _playerMissileSpawner.EnemyIsDestroyed += ShowScore;
    }

    private void ShowScore()
    {
        _fragsCounter++;

        _text.text = _fragsCounter.ToString();
    }

    private void OnDisable()
    {
        _playerMissileSpawner.EnemyIsDestroyed -= ShowScore;
    }
}