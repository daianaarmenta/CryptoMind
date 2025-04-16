using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private GameObject dialogueMark;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;
    private float typingTime = 0.05f;
    private bool isPlayerInRange;
    private bool didDialogueStart;
    private int lineIndex;
    private bool isTyping;

    private static bool isAnyDialogueActive = false;

    private MentorFantasma mentorFantasma;

    void Start()
    {
        mentorFantasma = GetComponent<MentorFantasma>();
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!didDialogueStart)
            {
                StartDialogue();
                Debug.Log($"Texto mostrado: [{dialogueText.text}]");
                Debug.Log($"Texto esperado: [{dialogueLines[lineIndex]}]");
            }
            else if (isTyping)
            {
                StopAllCoroutines();
                dialogueText.text = dialogueLines[lineIndex];
                isTyping = false;
            }
            else
            {
                NextDialogueLine();
                Debug.Log($"Avanzando a la línea: {lineIndex}");
            }
        }
    }

    private void StartDialogue()
    {
        if (isAnyDialogueActive) return;

        isAnyDialogueActive = true;
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
            dialoguePanel.SetActive(false);
            didDialogueStart = false;
            isAnyDialogueActive = false;
            dialogueMark.SetActive(true);
            Time.timeScale = 1f;

        }
    }

    private IEnumerator ShowLine()
    {
        isTyping = true;
        dialogueText.text = string.Empty;

        foreach (char letter in dialogueLines[lineIndex].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSecondsRealtime(typingTime);
        }

        isTyping = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
            dialogueMark.SetActive(true);
            if (mentorFantasma != null)
            {
                mentorFantasma.Aparecer();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            dialogueMark.SetActive(false);
            if (mentorFantasma != null)
                {
                    mentorFantasma.Desaparecer();
                }

            if (didDialogueStart)
            {
                StopAllCoroutines();
                dialoguePanel.SetActive(false);
                dialogueText.text = "";
                didDialogueStart = false;
                isTyping = false;
                isAnyDialogueActive = false;
                Time.timeScale = 1f;
            }
        }
    }
}


