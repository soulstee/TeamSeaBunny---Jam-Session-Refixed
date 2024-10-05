using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Import this to manage scene transitions

public class dialogue : MonoBehaviour
{
    // UI References
    [SerializeField]
    private GameObject dialogueCanvas;

    [SerializeField]
    private TMP_Text speakerText;

    [SerializeField]
    private TMP_Text dialogueText;

    [SerializeField]
    private Image portraitImage;

    [SerializeField]
    private GameObject choiceCanvas; // New canvas for choices
    [SerializeField]
    private TMP_Text choiceText; // Text for the choices
    [SerializeField]
    private Button yesButton; // Yes button
    [SerializeField]
    private Button noButton; // No button

    // Dialogue Content
    [SerializeField]
    private string[] speaker;

    [SerializeField]
    [TextArea]
    private string[] dialogueWords;

    [SerializeField]
    private Sprite[] portrait;

    private bool dialogueActivated;
    private int step;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact") && dialogueActivated)
        {
            if (step < speaker.Length)
            {
                // Show the next line of dialogue
                speakerText.text = speaker[step];
                dialogueText.text = dialogueWords[step];
                portraitImage.sprite = portrait[step];
                step++;
                
                // // If we reached the end of the dialogue
                // if (step >= speaker.Length)
                // {
                //     //After the last dialogue line, show the choice
                //     ShowChoice();
                // }
            }
            else
            {
                // Close the dialogue canvas if the step exceeds the dialogue length
                ShowChoice();
                step = 0; // Reset step if desired
            }
        }
    }

    private void ShowChoice()
    {
        dialogueCanvas.SetActive(false); // Hide the dialogue canvas
        choiceCanvas.SetActive(true); // Activate the choice canvas
        choiceText.text = "Do you want to proceed?"; // Set choice text
        yesButton.onClick.RemoveAllListeners(); // Clear previous listeners
        yesButton.onClick.AddListener(OnYes);
        noButton.onClick.RemoveAllListeners(); // Clear previous listeners
        noButton.onClick.AddListener(OnNo);
    }

    private void OnYes()
    {
        // Load the new scene
        SceneManager.LoadScene("SampleScene"); // Replace with your target scene name
    }

    private void OnNo()
    {
        choiceCanvas.SetActive(false); // Hide the choice canvas
        dialogueCanvas.SetActive(true); // Reactivate dialogue canvas
        //step = 0; // Reset step or set it to an appropriate value
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            dialogueActivated = true; // Enable dialogue when the player enters the trigger
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        dialogueActivated = false; // Disable dialogue when the player exits the trigger
        dialogueCanvas.SetActive(false); // Hide dialogue canvas
        choiceCanvas.SetActive(false); // Hide choice canvas when exiting
    }
}
