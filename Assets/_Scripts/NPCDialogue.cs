using UnityEngine;
using TMPro;

public class NPCDialogue : MonoBehaviour
{
    public GameObject dialogueUI;
    public TextMeshProUGUI dialogueText;

    public string[] lines;
    private int currentLine = 0;

    public void Interact()
    {
        dialogueUI.SetActive(true);
        dialogueText.text = lines[currentLine];
    }

    public void NextLine()
    {
        currentLine++;
        if (currentLine >= lines.Length)
        {
            dialogueUI.SetActive(false);
            currentLine = 0;
        }
        else
        {
            dialogueText.text = lines[currentLine];
        }
    }
}