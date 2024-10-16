using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Import this to manage scene transitions

public class dialogue : MonoBehaviour
{
    public string rhythmLevel;
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
    private GameObject dialogueNote;

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
    private SpeakerImage[] portrait;

    public Sprite empty;

    private bool dialogueActivated;
    private int step;
    public static bool inDialogue;
    public bool finishedDialogue = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && dialogueActivated && !finishedDialogue)
        {
            if (dialogueNote != null)
            {
                dialogueNote.SetActive(false);
            }

            if (dialogueCanvas != null)
            {
                dialogueCanvas.SetActive(true);
            }

            if (step < speaker.Length)
            {
                // Show the next line of dialogue
                if (speakerText != null)
                {
                    speakerText.text = speaker[step];
                }

                if (dialogueText != null && portrait[step] != null)
                {
                    dialogueText.text = "";
                    dialogueText.transform.parent.gameObject.SetActive(false);
                    dialogueText = portrait[step].dialogueText;
                    dialogueText.transform.parent.gameObject.SetActive(true);
                    speakerText.rectTransform.anchoredPosition = new Vector2(dialogueText.transform.parent.gameObject.GetComponent<RectTransform>().anchoredPosition.x-280f, dialogueText.transform.parent.gameObject.GetComponent<RectTransform>().anchoredPosition.y+70f);
                    dialogueText.text = dialogueWords[step];
                }

                if (portraitImage != null)
                {
                    //portraitImage.sprite = empty;
                }

                if (portrait[step] != null)
                {
                    portraitImage = portrait[step].image;
                    if (portraitImage != null)
                    {
                        portraitImage.sprite = portrait[step].sprite;
                    }
                }

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
    { //Hide the dialogue canvas
        finishedDialogue = true;

        if (choiceCanvas != null)
        {
            choiceCanvas.SetActive(true); // Activate the choice canvas
        }

        if (choiceText != null)
        {
            choiceText.text = "Do you want to proceed?"; // Set choice text
        }

        if (yesButton != null)
        {
            yesButton.onClick.RemoveAllListeners(); // Clear previous listeners
            yesButton.onClick.AddListener(OnYes);
        }

        if (noButton != null)
        {
            noButton.onClick.RemoveAllListeners(); // Clear previous listeners
            noButton.onClick.AddListener(OnNo);
        }
    }

    private void OnYes()
    {
        // Load the new scene
        SceneManager.LoadScene(rhythmLevel); // Replace with your target scene name
    }

    private void OnNo()
    {
        finishedDialogue = false;
        if (choiceCanvas != null)
        {
            choiceCanvas.SetActive(false); // Hide the choice canvas
        }

        if (dialogueCanvas != null)
        {
            dialogueCanvas.SetActive(false); // Reactivate dialogue canvas
        }
        //step = 0; // Reset step or set it to an appropriate value
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            dialogueActivated = true; // Enable dialogue when the player enters the trigger

            if (dialogueNote != null)
            {
                dialogueNote.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        finishedDialogue = false;
        dialogueActivated = false; // Disable dialogue when the player exits the trigger

        if (dialogueNote != null)
        {
            step = 0;
            dialogueNote.SetActive(false);
        }

        if (dialogueCanvas != null)
        {
            dialogueCanvas.SetActive(false); // Hide dialogue canvas
        }

        if (choiceCanvas != null)
        {
            choiceCanvas.SetActive(false); // Hide choice canvas
        }
    }

}

[System.Serializable]
public class SpeakerImage
{
    public Sprite sprite;
    public Image image;
    public TextMeshProUGUI dialogueText;
    public bool left;
}
