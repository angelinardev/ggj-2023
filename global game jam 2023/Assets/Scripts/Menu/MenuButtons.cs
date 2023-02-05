using System.Collections;
using System.Collections.Generic;
using UnityEditor.Purchasing;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    public Slider sound, music;

    public GameObject menu, options, credits;

    private float soundVolume, musicVolume;


    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Options()
    {
        DisableAll();
        options.SetActive(true);
    }

    public void SetSound()
    {
        soundVolume = sound.value;
    }

    public void SetMusic()
    {
        musicVolume = music.value;
    }

    public void Credits()
    {
        DisableAll();
        credits.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        DisableAll();
        menu.SetActive(true);
    }

    private void DisableAll()
    {
        menu.SetActive(false);
        options.SetActive(false);
        credits.SetActive(false);
    }
}
