using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private float loadingTime = 0.05f;
    public void PlayPressed()
    {
        Invoke(nameof(LoadGame), loadingTime);
    }

    private void LoadGame()
    {
        SceneManager.LoadScene("Asteroids");
    }

    public void ExitPressed()
    {
        Invoke(nameof(Exit), loadingTime);
    }

    private void Exit()
    {
        Debug.Log("Exit pressed!");
        Application.Quit();
    }
}
