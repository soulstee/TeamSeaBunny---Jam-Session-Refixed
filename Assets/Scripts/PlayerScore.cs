using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScore : MonoBehaviour
{

    public TextMeshProUGUI scoreText;

    public static float score = 0;
    public static float accuracy;
    public static float totalScore;

    public void SetScore(float add){
        score += add;
        UpdateScoreText();
    }

    public void UpdateScoreText(){
        scoreText.text = score.ToString();
    }
}
