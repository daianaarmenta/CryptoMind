using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueAuto : MonoBehaviour
{
    [SerializeField] private GameObject dialogueMark;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;
    private float typingTime = 0.05f;
    private float readingTime = 2.0f; // Tiempo adicional para leer cada línea
    private bool isPlayerInRange;
    private bool didDialogueStart;
    private int lineIndex;
    private bool hasDialoguePlayed = false;

    void Update()
    {
        if (isPlayerInRange && !hasDialoguePlayed)
        {
            if (!didDialogueStart)
            {
                StartDialogue();
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
        if (lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            StartCoroutine(EndDialogue());
        }
    }

    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;

        // Mostrar letra por letra
        foreach (char letter in dialogueLines[lineIndex].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSecondsRealtime(typingTime);
        }

        // Esperar antes de pasar al siguiente diálogo
        yield return new WaitForSecondsRealtime(readingTime);
        NextDialogueLine();
    }

    private IEnumerator EndDialogue()
    {
        yield return new WaitForSecondsRealtime(1.0f); // Espera un segundo antes de cerrar
        dialoguePanel.SetActive(false);
        didDialogueStart = false;
        hasDialoguePlayed = true;
        dialogueMark.SetActive(false);
        Time.timeScale = 1f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasDialoguePlayed)
        {
            isPlayerInRange = true;
            dialogueMark.SetActive(true);
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
}
