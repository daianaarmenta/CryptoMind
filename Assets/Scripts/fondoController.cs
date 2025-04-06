using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
/*Autora: Daiana Andrea Armenta Maya 
 * Descripción: Clase que controla el movimiento de un fondo en Unity.
 * Permite desplazar la textura del fondo en función de los valores de x e y.
 */
public class fondoController : MonoBehaviour
{
    public RawImage img;
    public float x;
    public float y;

    void Update()
    {
        img.uvRect = new Rect(img.uvRect.position + new Vector2(x,y) * Time.deltaTime, img.uvRect.size);
        
    }
}

