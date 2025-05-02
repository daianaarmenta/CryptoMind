/*
 * Autor: Raúl Maldonado Pineda - A01276808
 * Script: DialogueAuto.cs
 * Descripción: Este script gestiona el inicio y el flujo de un sistema de diálogo automático en el juego. Muestra
 *              los textos del diálogo de forma progresiva, con efectos de escritura y una breve pausa entre cada línea.
 *              También desactiva el movimiento del jugador mientras se muestra el diálogo.
 */

using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueAuto : MonoBehaviour
{
    // Referencias a los objetos en la escena
    [SerializeField] private GameObject dialogueMark;      // Marca que indica que el jugador puede interactuar
    [SerializeField] private GameObject dialoguePanel;     // Panel donde se muestra el texto del diálogo
    [SerializeField] private TMP_Text dialogueText;        // Componente de texto que muestra las líneas de diálogo
    [SerializeField] private string[] dialogueKeys;        // Claves de localización para las líneas del diálogo
    private string[] dialogueLines;                        // Las líneas de diálogo ya traducidas

    // Sonido que se reproduce mientras se muestra el texto (efecto de tipeo)
    [SerializeField] private AudioClip typingSound;
    private AudioSource typingAudioSource;                 // Fuente de audio para reproducir el sonido de tipeo
    private bool isSoundPlaying = false;                   // Indicador para saber si el sonido está sonando

    // Variables de control del diálogo
    private float typingTime = 0.05f;                      // Tiempo entre la aparición de cada letra
    private float readingTime = 1.0f;                      // Tiempo que el jugador espera antes de continuar a la siguiente línea
    private bool isPlayerInRange;                          // Indica si el jugador está cerca del área de diálogo
    private bool didDialogueStart;                         // Indica si el diálogo ya ha comenzado
    private int lineIndex;                                 // Índice de la línea de diálogo actual
    private bool hasDialoguePlayed = false;                // Indica si el diálogo ya se ha reproducido completamente

    // Referencias al jugador y su movimiento
    private GameObject playerObject;
    private MoverPersonaje playerMovement;

    // Se ejecuta al inicio del juego
    void Start()
    {
        typingAudioSource = GetComponent<AudioSource>();   // Se obtiene la fuente de audio para el sonido de tipeo

        // Se obtienen las líneas de diálogo a partir de las claves de localización
        dialogueLines = new string[dialogueKeys.Length];
        for (int i = 0; i < dialogueKeys.Length; i++)
        {
            dialogueLines[i] = LanguageManager.instance.GetText(dialogueKeys[i]);  // Obtención de las líneas traducidas
        }
    }

    // Se ejecuta cada frame
    void Update()
    {
        // Inicia el diálogo si el jugador está dentro del área de activación y no se ha reproducido antes
        if (isPlayerInRange && !hasDialoguePlayed)
        {
            if (!didDialogueStart)
            {
                StartDialogue();  // Inicia el diálogo
            }
        }
    }

    // Inicia el diálogo
    private void StartDialogue()
    {
        didDialogueStart = true;      // Marca que el diálogo ha comenzado
        dialoguePanel.SetActive(true); // Activa el panel de diálogo
        dialogueMark.SetActive(false); // Desactiva la marca de interacción
        lineIndex = 0;                // Resetea el índice de las líneas de diálogo

        // Desactiva el movimiento del jugador mientras el diálogo está en curso
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        StartCoroutine(ShowLine());   // Muestra la primera línea del diálogo
    }

    // Avanza a la siguiente línea del diálogo
    private void NextDialogueLine()
    {
        lineIndex++;
        if (lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());  // Muestra la siguiente línea
        }
        else
        {
            StartCoroutine(EndDialogue());  // Finaliza el diálogo si no hay más líneas
        }
    }

    // Muestra una línea del diálogo con un efecto de escritura
    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;  // Limpia el texto actual

        // Reproduce el sonido de tipeo
        if (typingAudioSource != null && typingSound != null)
        {
            typingAudioSource.clip = typingSound;
            typingAudioSource.loop = true;
            typingAudioSource.Play();
            isSoundPlaying = true;
        }

        string localizedLine = LanguageManager.instance.GetText(dialogueLines[lineIndex]);  // Obtiene la línea traducida
        foreach (char letter in localizedLine.ToCharArray())
        {
            dialogueText.text += letter;   // Agrega letra por letra al texto
            yield return new WaitForSecondsRealtime(typingTime); // Espera un tiempo entre cada letra
        }

        // Detiene el sonido de tipeo cuando la línea está completamente escrita
        if (typingAudioSource != null && isSoundPlaying)
        {
            typingAudioSource.Stop();
            isSoundPlaying = false;
        }

        yield return new WaitForSecondsRealtime(readingTime); // Pausa para leer la línea
        NextDialogueLine();   // Pasa a la siguiente línea
    }

    // Finaliza el diálogo
    private IEnumerator EndDialogue()
    {
        yield return new WaitForSecondsRealtime(1.0f); // Pausa al final

        dialoguePanel.SetActive(false); // Desactiva el panel de diálogo
        didDialogueStart = false;      // Marca que el diálogo ha finalizado
        hasDialoguePlayed = true;      // Evita que el diálogo se reproduzca nuevamente
        dialogueMark.SetActive(false); // Desactiva la marca de interacción

        // Detiene el sonido de tipeo si no está sonando
        if (typingAudioSource != null && !isSoundPlaying)
        {
            typingAudioSource.Stop();
            isSoundPlaying = false;
        }

        // Reactiva el movimiento del jugador
        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }
    }

    // Se ejecuta cuando el jugador entra en el área de activación del diálogo
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasDialoguePlayed)  // Si el jugador entra en el área
        {
            isPlayerInRange = true;  // Marca que el jugador está en rango
            dialogueMark.SetActive(true);  // Muestra la marca de interacción

            playerObject = collision.gameObject;  // Obtiene el objeto del jugador
            playerMovement = playerObject.GetComponent<MoverPersonaje>();  // Obtiene el componente de movimiento del jugador
        }
    }

    // Se ejecuta cuando el jugador sale del área de activación del diálogo
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))  // Si el jugador sale del área
        {
            isPlayerInRange = false;   // Marca que el jugador ya no está en rango
            dialogueMark.SetActive(false);  // Desactiva la marca de interacción
        }
    }
}
