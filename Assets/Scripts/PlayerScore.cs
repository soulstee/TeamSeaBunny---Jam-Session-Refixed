using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{

    public static int scoreValue = 0;
    Text score;

    // Start is called before the first frame update
    void Start()
    {
        score = GetComponent<Text>();  // Fetch the Text component attached to the GameObject
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "Score: " + scoreValue;  // Update the text property of the Text component
    }
}
