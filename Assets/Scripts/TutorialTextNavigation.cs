using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTextNavigation : MonoBehaviour
{
    public Text dialogueText;
    public Image dialogueImage;  // Reference to the UI Image that will display images
    public List<string> dialogueLines;  // Text lines for the dialogue
    public List<Sprite> dialogueImages; // Images for the dialogue lines (can be null for lines without images)

    private int currentDialogueIndex = 0;

    void Start()
    {
        // Initialize first dialogue line and image
        ShowDialogueLine(currentDialogueIndex);
    }

    public void NextText()
    {
        // Only go to the next line if there is one available
        if (currentDialogueIndex < dialogueLines.Count - 1)
        {
            currentDialogueIndex++;
            ShowDialogueLine(currentDialogueIndex);
        }
    }

    public void PreviousText()
    {
        // Only go to the previous line if we're not at the beginning
        if (currentDialogueIndex > 0)
        {
            currentDialogueIndex--;
            ShowDialogueLine(currentDialogueIndex);
        }
    }

    void ShowDialogueLine(int index)
    {
        dialogueText.text = dialogueLines[index];

        // Check if there's an image for the current line
        if (dialogueImages != null && dialogueImages.Count > index && dialogueImages[index] != null)
        {
            dialogueImage.sprite = dialogueImages[index];  // Set the image
            dialogueImage.gameObject.SetActive(true);      // Make sure the image is visible
        }
        else
        {
            dialogueImage.gameObject.SetActive(false);     // Hide the image if none is assigned
        }
    }
}
