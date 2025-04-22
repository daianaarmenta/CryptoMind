using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueAuto : MonoBehaviour
{
    [SerializeField] private GameObject dialogueMark;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;
    [SerializeField] private AudioClip typingSound;
    private AudioSource typingAudioSource;
    private bool isSoundPlaying = false;

    private float typingTime = 0.05f;
    private float readingTime = 1.0f;
    private bool isPlayerInRange;
    private bool didDialogueStart;
    private int lineIndex;
    private bool hasDialoguePlayed = false;

    private GameObject playerObject;
    private MoverPersonaje playerMovement;

    void Start()
    {
        typingAudioSource = GetComponent<AudioSource>();   
    }
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

        // Desactivar movimiento del jugador si est� disponible
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

        if (typingAudioSource != null && typingSound != null)
        {
            typingAudioSource.clip = typingSound;
            typingAudioSource.loop = true;
            typingAudioSource.Play();
            isSoundPlaying = true;
        }

        foreach (char letter in dialogueLines[lineIndex].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSecondsRealtime(typingTime);
        }

        if (typingAudioSource != null && isSoundPlaying)
        {
            typingAudioSource.Stop();
            isSoundPlaying = false;
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

        if(typingAudioSource != null && !isSoundPlaying)
        {
            typingAudioSource.Stop();
            isSoundPlaying = false;
        }

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
            playerMovement = playerObject.GetComponent<MoverPersonaje>();
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
