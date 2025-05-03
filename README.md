# CryptoMind
CryptoMind es un videojuego educativo desarrollado en Unity por estudiantes del Teconológico de Monterrey para CryptoChicks que combina la diversión de los juegos de aventura con preguntas sobre criptomonedas y blockchain. A meida que avanzas por los niveles, deberás de responder diferentes preguntas para continuar y aprender sobre el mundo de Blockchain. 

## Historia 
En NeonCity, una megaciudad controlada por Central Core, toda la información personal, identidad y decisiones de los ciudadanos está centralizada y manipulada. La gente vive sin cuestionar.
Eve, una joven ciudadana común, descubre por accidente un error en el sistema que la lleva a conocer a Zarek, un hacker forajido que intenta revelar el poder del blockchain: una red descentralizada donde la información es libre e inalterable.
Pero Central Core actúa rápido. Durante su primer encuentro, manipulan los comandos del jugador y hacen que Eve borre sin querer la identidad de Zarek. Desde ese momento, Eve toma la decisión de vencer a CentralCore, sin embargo no lo hará sola, Zarek la ayudará como un holograma para liberar a NeonCity.

## ¿Cómo se juega?
- Controla a tu personaje en u entorno de plataformas en 2D.
- Avanza por los niveles recolectando monedas y matando enemigos.
- Encuentra los checkpoints con "preguntas educativas".
- Habla con Zarek para entender los conceptos.
- Cada respuesta correcta te dará 100 monedas, si fallas se te restará una vida.
- Al ccontestar todas las preguntas en un nivel, desbloquearás el siguiente.
- Accede a la tienda desde cada nivel para comprar vidas y mejoras para tu arma.

## Controles
- `Flechas` o `A/W/S/D` : Mover al personaje
- `Espacio`: Disparar
- `E`: Interactuar con Zarek o con las preguntas.

## Tecnologías 
- Unity (motor de juego)
- C# (lógica y scripts)
- Assets y sonidos de la Unity Asset Store

## Instalación
1. Clona el repositorio:
   ```bash
   git clone https://github.com/daianaarmenta/CryptoMind.git
2. Abre el proyecto en Unity.
3. Haz click en el botón Play para iniciar el juego desde el editor.

## ¿Cómo preparar y desplegar el juego WebGL de Unity con tu propio servidor? 

Esta guía explica paso a paso cómo preparar un proyecto de Unity para ser exportado en formado WebGL y desplegado en un servidor web propio.
## **IMPORTANTE: Cambiar la URL base del servidor antes de subir a WebGL**
Antes de subir el juego a WebGL, debes asegurarte de que la URL base del servidor esté configurada correctamente. La URL base predeterminada en el código de la clase `ServidorCondig` es la siguiente:
```csharp
public const string BaseUrl = "http://18.232.125.185:8080"; // Cambia esto según tu servidor
```
## Pasos para cambiar la URL:
1. Abre la carpeta de `Assets` en el proyecto.
2. Localiza la carpeta de `Scripts`. Busca la carpeta con el nombre `General`.
3. Abre el archivo .cs `ServidorConfig.cs`.
4. Modifica la línea donde se define la variable `BaseUrl` para que apunte a su servidor o el servidor de producción adecuado:
   - Si estás utilizando un servidor local, usa la URL de de servidor local.
   - Si estás desplegando el juego en un servidor remoto, asegúrate de que la URL apunte correctamente a la dirección IP o nombre de dominio del servidor.
## Endpoints
Los siguientes endpoints también se configuran automáticamente según la URL base, pero ten en cuenta que puedes modificar la ruta si es necesario:
- Inicio de sesión: `/unity/login`
- Registro: `/unity/register`
- Carga de progreso: `unity/cargar-progreso`
- Guardar progreso: `unity/guardar-progreso`

## 1. Preparar el proyecto en Unity

Antes de comenzar el proceso de exportación, realiza los siguientes pasos:

- **Corrige todos los errores del proyecto**.
- **Optimiza los recursos**:
  - Reduce el tamaño de las texturas.
  - Comprime los archivos de audio.
  - Elimina archivos no utilizados.

---

## 2.  Configurar el proyecto para WebGL

### a) Instalar el módulo WebGL en Unity

1. Abre **Unity Hub**.
2. Ve a la pestaña **Installs** y selecciona tu versión de Unity.
3. Haz clic en **Add Modules**.
4. Marca **WebGL Build Support**.
5. Haz clic en **Install**.

### b) Cambiar la plataforma a WebGL

1. Abre tu proyecto en Unity.
2. Ve a **File > Build Settings**.
3. Selecciona **WebGL**.
4. Haz clic en **Switch Platform**.

---

## 3.Configurar opciones importantes en Player Settings

Dentro de **Build Settings**, haz clic en **Player Settings** y configura lo siguiente:

### En "Publishing Settings":
- **Compression Format**: selecciona **Disabled**.

### En "Other Settings":
- **Allow downloads over HTTP**: selecciona **Always Allowed**.

---

## 4. Generar el archivo de construcción (Build)

1. Vuelve a **Build Settings**.
2. Asegúrate de que la plataforma seleccionada sea **WebGL**.
3. Haz clic en **Build**.
4. Elige una carpeta vacía (por ejemplo, `ServidorWebApp`) y guarda ahí el build.

Unity generará:
- `index.html`
- Carpeta `Build/`
- Carpeta `TemplateData/`

---

## 5. Subir el juego a tu propio servidor web

1. Usa tu servicio de hosting o servidor propio.
2. Sube los archivos generados en la carpeta del build:
   - `index.html`
   - `Build/`
   - `TemplateData/`
3. Configura tu servidor para que el archivo inicial sea `index.html`.

 **Nota**: No es necesaria una configuración especial de compresión, ya que se desactivó en los ajustes.

## Licencia
Este software está licenciado bajo la licencia MIT. Esto significa que puede ser utilizado, copiado, modificado, fusionado, publicado, distribuido, sublicenciado y/o vendido, siempre y cuando se incluya una copia de esta licencia en todas las copias o partes sustanciales del software. 
El software fue desarrollado por el equipo de CryptoMind en el año 2025. Todos los derechos reservados, salvo disposición contraria en la licencia antes mencionada.

## Recursos de Terceros
Este juego incluye recursos externos con sus respectivos acuerdos de licencia:
- “Warped City Assets Pack” por Ansimuz, descargado de assetstore.unity.com. - Licencia de Standard Unity Asset Store EULA
- “Legacy Collection Assets Pack” por Ansimuz, descargado de itchio.com - License Creative Commons Zero v1.0 Universal
- “Cyber City 16x16 Tileset” por HearMoonLit, descargado de itchio.com - Sin licencia. 
- “Violeta” por Sekuora, descargado de pixabay.com - Licencia de Contenido de Pixabay. 
- “Neon Gaming” por dopestuff, descargado de pixabay.com - Licencia de Contenido de Pixabay.
- “Neon Trip back into the 80's”  por NaturesEye, descargado de pixabay.com - Licencia de Contenido de Pixabay.
- "Old fashioned clock sound" por FreeSound Community, descargado de pixabay.com - Licencia de Contenido de Pixabay.
- “Shopping Centre Sound” por Novifi, descargado de pixabay.com - Licencia de Contenido de Pixabay. 
- “2D Simple Pack” por OArielG, descargado de assetstore.unity.com - Licencia de Standard Unity Asset Store EULA
- “Fast and Intense” por Electronic-Sense, descargado de pixabay.com - Licencia de Contenido de Pixabay.


