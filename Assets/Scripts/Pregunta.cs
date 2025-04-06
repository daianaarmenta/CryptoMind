using UnityEngine;
[System.Serializable]

/* Autora: Daiana Andrea Armenta Maya 
 * Descripción: Clase que representa una pregunta en el juego.
 * Contiene el texto de la pregunta, las respuestas posibles y el índice de la respuesta correcta.
 */

public class Pregunta
{
    public string textoPregunta;  // El texto de la pregunta
    public string[] respuestas;   // Las respuestas posibles
    public int respuestaCorrecta; // El índice de la respuesta correcta
}

