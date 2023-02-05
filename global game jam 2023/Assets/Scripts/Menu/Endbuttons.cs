using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MapDS;
using System.IO;
using TMPro;

public class Endbuttons : MonoBehaviour
{
    public TMP_Text score;

    private string date;
    private float time;

    private StreamReader reader;
    private string path;
    private List<Map> leaderboard;

    private StreamWriter writer;

    private void Start()
    {
        leaderboard = new List<Map>();
        path = Application.persistentDataPath + "\\scores.txt";

        if (PlayerPrefs.HasKey("Score"))
        {
            date = PlayerPrefs.GetString("Date");
            time = PlayerPrefs.GetFloat("Score");
        }
        else time = float.MaxValue;
        score.text = "Your score:\n" + time.ToString();

        reader = new StreamReader(path);

        string line;
        while ((line = reader.ReadLine()) != null)
        {
            string date = line.Split(',')[0];
            float score = float.Parse(line.Split(',')[1]);
            leaderboard.Add(new Map(date, score));
        }

        reader.Close();

        Map temp = new Map("0", 0);
        leaderboard.Add(new Map(date, time));

        //bubble sort it
        for(int o = 0; o <= leaderboard.Count - 2; o++)
        {
            for(int i = 0; i < o; i++)
            {
                if (leaderboard[i].IsGreaterThan(leaderboard[i + 1]))
                {
                    temp = leaderboard[i + 1];
                    leaderboard[i + 1] = leaderboard[i];
                    leaderboard[i] = temp;
                }
            }
        }

        writer = new StreamWriter(path);

        //save only top 5
        for(int i = 0; i < leaderboard.Count && i < 6; i++)
        {
            writer.WriteLine(leaderboard[i].ToString());
        }

        writer.Close();
    }



    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
