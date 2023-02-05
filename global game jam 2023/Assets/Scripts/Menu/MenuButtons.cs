using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO;
using MapDS;
using Unity.VisualScripting;

public class MenuButtons : MonoBehaviour
{
    public Slider sound, music;

    public GameObject menu, options, credits;
    public TMP_Text scores;

    private float soundVolume, musicVolume;

    private StreamReader reader;
    private string path;
    private List<Map> leaderboard;

    private void Start()
    {
        leaderboard = new List<Map>();

        path = Application.persistentDataPath + "/scores.txt";
        if (!File.Exists(path)) File.Create(path);

        reader = new StreamReader(path);

        string line;
        while((line = reader.ReadLine()) != null)
        {
            string date = line.Split(',')[0];
            float score = float.Parse(line.Split(',')[1]);
            leaderboard.Add(new Map(date, score));
        }

        scores.text = "Highscores:\n\n";
        int i = 1;
        foreach(Map m in leaderboard)
        {
            scores.text += i++.ToString() + ") " + m.ToString() + "\n";
        }

        reader.Close();
    }

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

namespace MapDS
{
    public struct Map
    {
        private string key;
        private float value;

        public Map(string index, float info) { key = index; value = info; }

        public Map GetMap() => new Map(key, value);
        public string GetKey() => key;
        public float GetValue() => value;
        public bool IsGreaterThan(float v) => value > v;
        public bool IsGreaterThan(Map m) => value > m.value;
        public bool IsLessThan(float v) => value < v;
        public bool IsLessThan(Map m) => value < m.value;
        public bool Equals(float v) => value == v;
        public bool Equals(Map m) => value == m.value;

        public override string ToString() => key + ", " + value.ToString("0.00");

    }

}