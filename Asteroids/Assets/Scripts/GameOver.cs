using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private float loadingTime = 0.05f;
    public void MenuPressed()
    {
        Invoke(nameof(LoadMenu), loadingTime);
    }

    private void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
