using UnityEngine;
using TMPro;
using System.Data.Common;
using System.Collections;
using System;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private GameObject dialogueMark;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4,6)] private string[] dialogueLines;
    private float typingTime = 0.05f;
    private bool isPlayerInRange;
    private bool didDialogueStart;
    private int lineIndex;
    private float fadeDuration = 2f;
    private SpriteRenderer mentorRenderer;

    void Start()
    {
       mentorRenderer = GetComponent<SpriteRenderer>();

        if (mentorRenderer != null)
        {
            Color c = mentorRenderer.color;
            c.a = 0f; 
            mentorRenderer.color = c;
            Debug.Log("Mentor alpha at start: " + mentorRenderer.color.a);
        }
        else
        {
            Debug.LogWarning("Mentor SpriteRenderer not found.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!didDialogueStart)
            {
                StartDialogue();
            }
            else if(dialogueText.text == dialogueLines[lineIndex])
            {
                NextDialogueLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = dialogueLines[lineIndex];
            }
        }
        
    }

    private void StartDialogue()
    {
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        dialogueMark.SetActive(false);
        lineIndex = 0;
        Time.timeScale = 0f;
        StartCoroutine(ShowLine());

    }

    private void NextDialogueLine()
    {
        lineIndex++;
        if(lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            dialoguePanel.SetActive(false);
            didDialogueStart = false;
            dialogueMark.SetActive(true);
            Time.timeScale = 1f;
        }
    }

    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;
        foreach (char letter in dialogueLines[lineIndex].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSecondsRealtime(typingTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isPlayerInRange = true;
        dialogueMark.SetActive(true);

        
        if (mentorRenderer != null)
        {
            Debug.Log("IsPlayerInRange: " + isPlayerInRange);
            Debug.Log("mentorRenderer found? " + (mentorRenderer != null));
            StartCoroutine(FadeInMentor());
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            dialogueMark.SetActive(false);
        }
    }

    private IEnumerator FadeInMentor()
    {
        Color c = mentorRenderer.color;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            c.a = Mathf.Clamp01(elapsed / fadeDuration);
            mentorRenderer.color = c;
            Debug.Log("Current alpha: " + c.a);
            yield return null;
        }
        Debug.Log("Fade-in complete.");
    }
}
