/*
 * Autor: Ra�l Maldonado Pineda - A01276808
 * Script: TransicionFinal.cs
 * Descripci�n: Este script maneja el comportamiento al finalizar una cutscene (Timeline) en Unity.
 *              Se asegura de que el personaje permanezca visible e inm�vil, desactiva sus scripts de control
 *              y transiciona autom�ticamente a la escena "Cr�ditos" una vez finalizada la Timeline.
 */

using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class TransicionFinal : MonoBehaviour
{
    // Referencia al PlayableDirector que ejecuta la cutscene
    [SerializeField] private PlayableDirector timeline;

    private GameObject personaje;               // Referencia al objeto del personaje en la escena
    private Animator personajeAnimator;         // Referencia al componente Animator del personaje
    private Rigidbody2D rb;                     // Referencia al Rigidbody2D del personaje (para movimiento)
    private Collider2D col;                     // Referencia al Collider2D del personaje (opcional)

    void Start()
    {
        // Buscar el objeto llamado "personaje" en la escena
        personaje = GameObject.Find("personaje");

        if (personaje != null)
        {
            // Obtener los componentes necesarios del personaje
            personajeAnimator = personaje.GetComponent<Animator>();
            rb = personaje.GetComponent<Rigidbody2D>();
            col = personaje.GetComponent<Collider2D>();

            // Asegurar que el personaje est� activo y visible
            personaje.SetActive(true);

            // Activar el Animator si existe
            if (personajeAnimator != null)
                personajeAnimator.enabled = true;
        }

        // Registrar el evento al finalizar la timeline
        if (timeline != null)
        {
            timeline.stopped += OnTimelineFinished;
        }
    }

    // Se llama autom�ticamente cuando la timeline termina
    private void OnTimelineFinished(PlayableDirector director)
    {
        if (personaje != null)
        {
            // Detener el movimiento del personaje y congelarlo completamente
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;                    // Detener movimiento lineal
                rb.angularVelocity = 0f;                       // Detener rotaci�n
                rb.bodyType = RigidbodyType2D.Static;          // Congelar f�sicas
            }

            // Desactivar todos los scripts de comportamiento del personaje (excepto este)
            MonoBehaviour[] scripts = personaje.GetComponents<MonoBehaviour>();
            foreach (var script in scripts)
            {
                if (script != this && script.enabled)
                {
                    script.enabled = false;
                }
            }

            // Detener animaciones si hay un Animator
            if (personajeAnimator != null)
            {
                personajeAnimator.enabled = false;
            }
        }

        // Realizar la transici�n a la escena de cr�ditos
        TransicionEscena transicion = FindFirstObjectByType<TransicionEscena>();
        if (transicion != null)
            transicion.IrAEscena("Creditos");
        else
            SceneManager.LoadScene("Creditos");
    }

    // Evitar referencias colgadas al destruir el objeto
    private void OnDestroy()
    {
        if (timeline != null)
            timeline.stopped -= OnTimelineFinished;
    }
}
