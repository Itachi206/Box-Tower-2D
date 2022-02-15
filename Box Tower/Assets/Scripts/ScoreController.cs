using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreController : MonoBehaviour
{
    public static ScoreController instance;
    private TextMeshProUGUI scoreText;
    private int score = 0;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        
        scoreText = GetComponent<TextMeshProUGUI>();
        
    }

    private void start()
    {
        RefreshUI();
    }

    public void IncreaseScore(int increment)
    {
        score += increment;
        RefreshUI();
    }

    private void RefreshUI()
    {
        scoreText.text = "Score: " + score;
    }
}