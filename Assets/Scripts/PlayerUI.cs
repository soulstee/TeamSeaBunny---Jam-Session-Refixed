using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScore : MonoBehaviour {

    public TextMeshProUGUI scoreText;

    public static float score = 0;
    public static float accuracy;
    public static float totalScore;

    public Text winText;
    public Text lossText;
    public GameObject player;

    public void SetScore(float add){
        score += add;
        UpdateScoreText();
    }

    public void UpdateScoreText(){
        scoreText.text = score.ToString();
    }

    // player win, loss state
    /*private void OnTriggerEnter(Collider2D collision) {
        if (collision.tag == "Win") {
            winText.gameObject.SetActive(true);
            Time.timeScale = 0;
        }

        if (collision.tag == "Loss") {
            lossText.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }*/
}
