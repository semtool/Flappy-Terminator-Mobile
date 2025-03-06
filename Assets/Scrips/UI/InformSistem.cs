using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InformSistem : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Player _player;
    [SerializeField] private Informer _informer;

    private void Start()
    {
        RestartButtonTurnOff();
    }

    private void OnEnable()
    {
        _player.IsDestroyed += RestartButtonTurnOn;

        _button.onClick.AddListener(RestartScene);
    }

    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        RestartButtonTurnOff();
    }

    private void RestartButtonTurnOn()
    {
        _button.gameObject.SetActive(true);
        _informer.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    private void RestartButtonTurnOff()
    {
        _button.gameObject.SetActive(false);
        _informer.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    private void OnDisable()
    {
        _player.IsDestroyed -= RestartButtonTurnOn;

        _button.onClick.RemoveListener(RestartScene);
    }
}