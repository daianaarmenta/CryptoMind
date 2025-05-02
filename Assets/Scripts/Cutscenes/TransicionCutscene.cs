/*
 * Autor: Raúl Maldonado Pineda - A01276808
 * Script: CutsceneEndManager.cs
 * Descripción: Este script maneja el comportamiento del personaje una vez finaliza una cutscene (Timeline).
 *              Se asegura de que el personaje se detenga, desactive sus scripts de control, y cargue
 *              automáticamente la siguiente escena.
 */

using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CutsceneEndManager : MonoBehaviour
{
    // Referencia al PlayableDirector que ejecuta la cutscene
    [SerializeField] private PlayableDirector timeline;

    private GameObject personaje;               // Referencia al objeto del personaje en la escena
    private Animator personajeAnimator;         // Referencia al componente Animator del personaje
    private Rigidbody2D rb;                     // Referencia al Rigidbody2D del personaje (para movimiento)

    void Start()
    {
        // Buscar el objeto llamado "personaje" en la escena
        personaje = GameObject.Find("personaje");

        if (personaje != null)
        {
            // Obtener los componentes necesarios del personaje
            personajeAnimator = personaje.GetComponent<Animator>();
            rb = personaje.GetComponent<Rigidbody2D>();

            // Asegurar que el personaje esté activo y visible
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

    // Se llama automáticamente cuando la timeline termina
    private void OnTimelineFinished(PlayableDirector director)
    {
        if (personaje != null)
        {
            // Detener el movimiento del personaje y congelarlo completamente
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;                    // Detener movimiento lineal
                rb.angularVelocity = 0f;                       // Detener rotación
                rb.bodyType = RigidbodyType2D.Static;          // Congelar físicas
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

        // Realizar la transición a la siguiente escena
        TransicionEscena transicion = FindFirstObjectByType<TransicionEscena>();
        if (transicion != null)
        {
            transicion.IrASiguienteEscena();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    // Evitar referencias colgadas al destruir el objeto
    private void OnDestroy()
    {
        if (timeline != null)
            timeline.stopped -= OnTimelineFinished;
    }
}
