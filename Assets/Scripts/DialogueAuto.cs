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
    private float readingTime = 2.0f;
    private bool isPlayerInRange;
    private bool didDialogueStart;
    private int lineIndex;
    private bool hasDialoguePlayed = false;

    private GameObject playerObject;
    private MueveChabelito playerMovement; // Asumimos que así se llama tu script de movimiento

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

        // Desactivar movimiento del jugador si está disponible
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

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

        foreach (char letter in dialogueLines[lineIndex].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSecondsRealtime(typingTime);
        }

        yield return new WaitForSecondsRealtime(readingTime);
        NextDialogueLine();
    }

    private IEnumerator EndDialogue()
    {
        yield return new WaitForSecondsRealtime(1.0f);

        dialoguePanel.SetActive(false);
        didDialogueStart = false;
        hasDialoguePlayed = true;
        dialogueMark.SetActive(false);

        // Reactivar movimiento del jugador si estaba desactivado
        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasDialoguePlayed)
        {
            isPlayerInRange = true;
            dialogueMark.SetActive(true);

            playerObject = collision.gameObject;
            playerMovement = playerObject.GetComponent<MueveChabelito>();
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
