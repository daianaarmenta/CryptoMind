using UnityEngine;
using TMPro;
using System.Collections;
using Unity.VisualScripting;

/* Autora: Daiana Andrea Armenta Maya
 * Descripción: Clase que gestiona el diálogo en el juego, mostrando líneas de texto y controlando la interacción del jugador.
 * Contiene métodos para iniciar el diálogo, avanzar a la siguiente línea y mostrar el texto con un efecto de escritura.
 */
public class DialogueController : MonoBehaviour
{
    [SerializeField] private GameObject dialogueMark;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private string[] dialoguekeys;

    private string[] dialogueLines;
    private float typingTime = 0.05f;
    private bool isPlayerInRange;
    private bool didDialogueStart;
    private int lineIndex;
    private bool isTyping;

    public static bool isDialogueOpen = false;

    private static bool isAnyDialogueActive = false;

    private MentorFantasma mentorFantasma;
    [SerializeField] private AudioSource typingAudioSource;
    [SerializeField] private AudioClip typingSound;
    private bool isSoundPlaying = false;
    [SerializeField] private AudioClip notificationSound;

    void Start()
    {
        mentorFantasma = GetComponent<MentorFantasma>();


        if (typingAudioSource != null)
        {
            typingAudioSource.Stop(); // Detener el sonido si está sonando al inicio
        }

        dialogueLines = new string[dialoguekeys.Length]; // Inicializar el array de líneas de diálogo
        // Obtener las líneas de diálogo desde el gestor de lenguaje
        for( int i = 0; i < dialoguekeys.Length; i++){
            dialogueLines[i] = LanguageManager.instance.GetText(dialoguekeys[i]);
        }
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
                // Detener el sonido también
                if (typingAudioSource != null && typingAudioSource.isPlaying)
                {
                    typingAudioSource.Stop();
                    typingAudioSource.loop = false;
                }
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
        isDialogueOpen = true;
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        dialogueMark.SetActive(false);
        lineIndex = 0;
        Time.timeScale = 0f;


        StartCoroutine(ShowLine()); // Iniciar la corutina para mostrar la primera línea de diálogo
    }

    private void NextDialogueLine()
    {
        lineIndex++;
        if (lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine()); // Iniciar la corutina para mostrar la siguiente línea de diálogo
        }
        else
        {
            dialoguePanel.SetActive(false);
            didDialogueStart = false;
            isAnyDialogueActive = false;
            isDialogueOpen = false;
            dialogueMark.SetActive(true);
            Time.timeScale = 1f;

        }
    }

    private IEnumerator ShowLine()
    {
        isTyping = true;
        dialogueText.text = string.Empty; // Limpiar el texto antes de mostrar la nueva línea

        if(typingAudioSource != null && !isSoundPlaying)
        {
            typingAudioSource.clip = typingSound;
            typingAudioSource.loop = true;
            typingAudioSource.Play();
        }

        foreach (char letter in dialogueLines[lineIndex].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSecondsRealtime(typingTime); // Esperar el tiempo de escritura
        }

        if (typingAudioSource != null)
        {
            typingAudioSource.Stop();
            typingAudioSource.loop = false;
        }

        isTyping = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
            dialogueMark.SetActive(true);
            AudioSource.PlayClipAtPoint(notificationSound, transform.position,1f); // Reproducir sonido de notificación
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
                isDialogueOpen = false;
                isTyping = false;
                isAnyDialogueActive = false;
                Time.timeScale = 1f;
            }
        }
    }
}


