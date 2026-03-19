using UnityEngine;
using TMPro;
using System.Collections;

public class newAnimControl : MonoBehaviour
{
    [Header("References")]
    public GrammarUIController grammarUIController;
    public Animator anim;
    public TMP_Text outputText;
    public DialogueLine[] lines;
    public GameObject textActiveObject;

    private int currentIndex = 0;

    [Header("Animation Return Timing")]
    public float angryDuration = 1.5f;
    public float happyDuration = 1.5f;
    public float talkingDuration = 2f;

    [Header("Timing")]
    public float textVisibleTime = 3.5f;

    private Coroutine returnToIdleCoroutine;
    private Coroutine clearTextCoroutine;
    private string quest;
    private Quaternion startingRotation;

    public enum DialogueAnimation
    {
        Idle,
        Angry,
        Happy,
        Talking
    }

    [System.Serializable]
    public class DialogueLine
    {
        [TextArea(2, 4)]
        public string text;
        public DialogueAnimation animation;
    }

    void Awake()
    {
        if (anim == null)
            anim = GetComponent<Animator>();

        startingRotation = transform.rotation;
    }

    void Start()
    {
        ClearText();
        PlayIdle();
        GenerateQuestFromGrammar();
    }

    public void GenerateQuestFromGrammar()
    {
        if (grammarUIController == null)
        {
            Debug.LogWarning("GrammarUIController reference is missing.");
        } else
        {
            quest = grammarUIController.GenerateStory();
            Debug.Log("Generated Quest: " + quest);
        }
    }

    public void ShowNextLine()
    {
        if (lines == null || lines.Length == 0)
            return;

        DialogueLine currentLine = lines[currentIndex];
        string lineText = currentLine.text;

        if (!string.IsNullOrEmpty(quest) && lineText.Contains("{quest}"))
        {
            lineText = lineText.Replace("{quest}", quest);
        }

        outputText.text = lineText;
        transform.rotation = startingRotation;
        if (textActiveObject != null)
        {
            textActiveObject.SetActive(true);
        }

        PlayDialogueAnimation(currentLine.animation);

        if (clearTextCoroutine != null)
        {
            StopCoroutine(clearTextCoroutine);
        }

        clearTextCoroutine = StartCoroutine(ClearTextAfterDelay());

        if (currentIndex < lines.Length - 1)
        {
            currentIndex++;
        }
    }

    private void PlayDialogueAnimation(DialogueAnimation animationToPlay)
    {
        switch (animationToPlay)
        {
            case DialogueAnimation.Angry:
                PlayAngry();
                break;

            case DialogueAnimation.Happy:
                PlayHappy();
                break;

            case DialogueAnimation.Talking:
                PlayTalking();
                break;

            default:
                PlayIdle();
                break;
        }
    }
    private IEnumerator ClearTextAfterDelay()
    {
        yield return new WaitForSeconds(textVisibleTime);
        ClearText();
    }

    private void ClearText()
    {
        outputText.text = "";
        if (textActiveObject != null)
        {
            textActiveObject.SetActive(false);
        }
    }

    public void PlayIdle()
    {
        StopReturnCoroutine();
        ResetAllTriggers();
        anim.SetTrigger("idle");
    }

    public void PlayAngry()
    {
        StopReturnCoroutine();
        ResetAllTriggers();
        anim.SetTrigger("angry");
        returnToIdleCoroutine = StartCoroutine(ReturnToIdleAfterDelay(angryDuration));
    }

    public void PlayHappy()
    {
        StopReturnCoroutine();
        ResetAllTriggers();
        anim.SetTrigger("happy");
        returnToIdleCoroutine = StartCoroutine(ReturnToIdleAfterDelay(happyDuration));
    }

    public void PlayTalking()
    {
        StopReturnCoroutine();
        ResetAllTriggers();
        anim.SetTrigger("talking");
        returnToIdleCoroutine = StartCoroutine(ReturnToIdleAfterDelay(talkingDuration));
    }

    private System.Collections.IEnumerator ReturnToIdleAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        PlayIdle();
    }

    private void StopReturnCoroutine()
    {
        if (returnToIdleCoroutine != null)
        {
            StopCoroutine(returnToIdleCoroutine);
            returnToIdleCoroutine = null;
        }
    }

    private void ResetAllTriggers()
    {
        anim.ResetTrigger("idle");
        anim.ResetTrigger("angry");
        anim.ResetTrigger("happy");
        anim.ResetTrigger("talking");
    }
}