using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public static Timer _instance { get; private set; }

    public TMP_Text text;
    
    private float timer = 0f;
    private string date;

    private void Awake()
    {
        if (_instance == null) _instance = this;
        else Destroy(this);
    }

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
        PlayerPrefs.SetString("Date", date.Split()[0]);
        PlayerPrefs.SetFloat("Score", timer);
    }

}
