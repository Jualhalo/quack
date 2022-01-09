using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timer;
    private float startTime;
    private float time;

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        time = Time.time - startTime;

        string minutes = ((int) time / 60).ToString();
        string seconds = (time % 60).ToString("f3");

        timer.text = minutes + ":" + seconds;
    }
}
