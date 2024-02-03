using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerScoreManager : MonoBehaviour
{
    private int score = 0;
    private TextMeshProUGUI scoreUI;

    private void Awake()
    {
        scoreUI = GetComponent<TextMeshProUGUI>();
    }

    // Start is called before the first frame update
    void Start()
    {
        RefreshUI();
    }

    private void RefreshUI()
    {
        scoreUI.text = "Score: " + score;
    }

    public void UpdateScore(int _score) { 
        score += _score;
        RefreshUI();
    }

}
