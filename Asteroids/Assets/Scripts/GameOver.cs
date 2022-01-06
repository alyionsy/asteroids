using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private readonly float loadingTime = 0.05f;
    public void MenuPressed()
    {
        Invoke(nameof(LoadMenu), loadingTime);
    }

    private void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void RestartPressed()
    {
        Invoke(nameof(LoadGame), loadingTime);
    }

    private void LoadGame()
    {
        SceneManager.LoadScene("Asteroids");
    }
}
