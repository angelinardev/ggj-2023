using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public static Timer _instance { get; private set; }

    public TMP_Text text;
    
    private float timer = 0f;
    private string date;

    private void Start()
    {
        date = DateTime.Now.ToString();
        Debug.Log(date);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        text.text = "Time: " + MathF.Round(timer).ToString();
    }

    // on win, call this function
    public void SaveScore()
    {
        PlayerPrefs.SetString("Date", date);
        PlayerPrefs.SetFloat("Score", timer);
    }

}
