using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{

    public float timeLeft;
    public bool timerOn = false;

    public TMP_Text timerText;

    void Start()
    {
        timerOn = true;
    }

    void Update()
    {
        if(timerOn && timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            updateTimer(timeLeft);

        } else
        {
            timeLeft = 0;
            timerOn = false;
            GameManager.SharedInstance.GameOver();
        }
    }

    private void updateTimer(float currentTime)
    {
        currentTime += 1;
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        timerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
