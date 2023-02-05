using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pauseScreen;

    private bool paused;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
        }
        if (paused)
            PauseGame();
        else PlayGame();
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
        pauseScreen.SetActive(true);
    }

    public void PlayGame()
    {
        Time.timeScale = 1f;
        pauseScreen.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
