using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWinLoss : MonoBehaviour {

    public Text winText;
    public Text lossText;

    public GameObject player;
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    private void OnTriggerEnter(Collider2D collision) {
        if (collision.tag == "Win") {
            winText.gameObject.SetActive(true);
            Time.timeScale = 0;
        }

        if (collision.tag == "Loss") {
            lossText.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
