# My Jumping Knight: Documento de Diseño de Juego
> Angry Pineapple Games

> Versión del Documento: 0.1

> Plantilla de GDD adaptada por: [Benjamin “HeadClot” Stanley](https://docs.google.com/document/d/1-I08qX76DgSFyN1ByIGtPuqXh7bVKraHcNIA25tpAzE/ "Enlace al documento de plantilla")

> Escrito con [StackEdit](https://stackedit.io/).

## 1. Historial de Versiones
* 0.1: Esqueleto del GDD y primera idea conceptual.
## 2. Concepto de Juego
### 2.1. Género y Setting
My Jumping Knight es un juego de *running by tapping* de vista isométrica en el que el jugador deberá manejar a un personaje que se mueve por un tablero lleno de trampas hasta alcanzar la salida con ambientación medieval. Permite además enfrentarse a otros jugadores mediante la subida y descarga del recorrido que ha hecho el jugador en distintas iteraciones del juego, evitando así lag y dando lugar a un enfrentamiento muy preciso.
### 2.2. Mecánicas de juego principales
* **Avance en casillas**: El personaje no se moverá libremente por el escenario, sino que avanzará de manera automática a una casilla adyacente a la que se encuentra, según dónde le indique el jugador.
* **Uso de trampas**: Habrá diferentes trampas en el escenario que tendrán distintos efectos negativos sobre el jugador.
* **Competición**: Se incentivará al jugador a competir tanto consigo mismo como con otros jugadores. Se desarrolla esta idea en los subapartados de "Puntuación" y "Modos de Juego"
* **Desarrollo por niveles**: Se ha tomado la decisión de dividir el juego en tres niveles con dificultad incremental y diferente ambientación para poder fomentar el factor competitivo del juego.
### 2.3. Plataformas
El juego se desarrollará para ser jugado en web, tanto en dispositivos móviles y tablets como en ordenador.
### 2.4. Idiomas
Estará disponible en los idiomas inglés y español.
### 2.5. Alcance y escala del proyecto
-   **Escala económica y de tiempo**:
    -   **Presupuesto inicial**: 0 euros.
    -   **Fecha de inicio**: 4 de noviembre de 2019.
    -   **Fecha estimada de finalización del prototipo**: 5 de diciembre de 2019.
-   **Equipo**:
    -   **Mario Aceituno Cordero**: Concept art, texturizado y diseño GUI.
    -   **Javier Albaráñez Martínez**: Programación.
    -   **César Carbajo García**: Programación.
    -   **Juan Antonio Martín García**: Gestión de proyecto, game design, documentación, community management y diseño GUI.
    -   **Laura Suonpera Lozano**: 3D Art, modelado y diseño GUI.
### 2.6. Influencias
* **Redungeon**: Juego de running con desarrollo procedimental en el que se ha tomado inspiración para la mecánica de movimiento y las trampas.
* **Crossy Road**: Juego de estilo *Frogger* procedimental donde se avanza haciendo *taps* en la pantalla. Ha sido una influencia a la hora de escoger el enfoque que se quería hacer al juego para ofrecer al jugador una experiencia inmediata y de fácil aprendizaje.
* **Crypt of the Necrodancer**: Otro juego donde el movimiento se basa en desplazamientos por una cuadrícula pudiendo avanzar hacia zonas adyacentes dentro del grid.7
## 3. Elevator Pitch
TODO
## 4. Historia y Gameplay
TODO
### 4.1. Sinopsis de la historia
TODO
### 4.2. Historia en detalle
TODO
### 4.3. Gameplay resumido
### 4.4. Gameplay en detalle
#### 4.4.1. Flujo de recorrido del usuario
#### 4.4.2. Información en pantalla
#### 4.4.3. Desarrollo de una partida
#### 4.4.4. Trampas
#### 4.4.5. Desarrollo de los niveles
#### 4.4.6. Cámara
#### 4.4.7. Controles
#### 4.4.8. Puntuación
#### 4.4.9. Modos de juego
* **Individual**: El jugador podrá jugar los 3 niveles del juego, desbloqueándolos uno a uno e intentando sacar el mejor rango y tiempo posible.
* **Multijugador**: El jugador se enfrentará a un fantasma de otro jugador que se ha descargado, intentando finalizar el nivel antes de que lo haga el otro. El matchmaking se realizará aplicando lógica difusa: al jugador que está buscando jugar online se le asignará un jugador cuyas últimas 10 partidas tengan puntuaciones similares a la suya.
#### 4.4.10. Sistema de guardado
El sistema de guardado del jugador permitirá recordar sus mejores puntuaciones y rangos, así como los niveles que ha desbloqueado.
## 5. Modelo de Negocio
TODO
## 6. Posibles ampliaciones
TODO
## 7. Monetización
TODO
## 8. Assets necesarios
### 8.1. Modelos
- Personaje principal
- Bloque o tile base
- Trampa: Pinchos
-   Trampa: Sierra
-   Trampa: Flechas
-   Trampa: Cuchilla giratoria
-   Extras estéticos: Prado
-   Extras estéticos: Bosque
-   Extras estéticos: Castillo
-   Powerup: Corazón
-   Powerup: Reloj de arena
-   Powerup: Escudo
### 8.2. Texturas
-   Skybox de los 3 niveles
-   Cajas/Tiles: Prado (1, 2, 3, 4)
-   Cajas/Tiles: Bosque (1, 2, 3, 4)
-   Cajas/Tiles: Castillo (1, 2, 3, 4)
-   Trampa: Pinchos
-   Trampa: Sierra
-   Trampa: Flechas
-   Trampa: Cuchilla giratoria
### 8.3. Interfaz
-   Botón de Jugar
-   Botón de Configuración
-   Botón de Tutorial
-   Botón de Créditos
-   Boton de Individual
-   Botón de Multijugador
-   Botón de Nivel 1
-   Botón de Nivel 2
-   Botón de Nivel 3
-   Icono de Corazón
-   Icono de Reloj
-   Icono de Ranking: C, B, A, A+, S, S+
-   Cartel de Puntuación
-   Fondos

### 8.4. Código del Engine
-   Implementación de transición de pantallas
-   Implementación de comportamiento de interfaz
-   Implementación de mecánica de movimiento
-   Implementación de mecánicas de trampas
-   Implementación de la cámara
-   Implementación de interacción jugador-trampa
-   Implementación de inicio y fin de partida
-   Implementación de pantalla de configuración
-   Implementación de pantalla de créditos
-   Implementación de pantalla de tutorial
-   Implementación de pantalla de menú principal
-   Implementación de pantalla de selección de modo
-   Implementación de pantalla de selección de nivel
-   Implementación de nivel 1
-   Implementación de nivel 2
-   Implementación de nivel 3
-   Implementación de pantalla de game over (muerte)
-   Implementación de pantalla de game over (victoria)
-   Implementación de registro de puntuación
-   Implementación de powerup corazón
-   Implementación de powerup escudo
-   Implementación de powerup reloj
-   Implementación del _switch_ de idioma

### 8.5. Código del Multijugador
-   Implementación de subida y guardado en servidor de la info
-   Implementación del registro de movimiento
-   Implementación de descarga desde el servidor
-   Implementación de matchmaking
-   Implementación del modo multijugador

### 8.6. Música y Sonido
- Sonido protagonista: Salto
- Sonido protagonista: Caída
- Sonido protagonista: Daño
- Sonido protagonista: Muerte
- Sonido protagonista: Victoria
- Sonido trampa: Pinchos
- Sonido trampa: Flechas
- Sonido trampa: Sierra
- Sonido trampa: Cuchilla giratoria
- Sonido powerup: Corazón
- Sonido powerup: Escudo
- Sonido powerup: Reloj
- Sonido powerup: obtención de powerup
- Sonido UI: botón pulsado
- Sonido: matchmaking encontrado
- Música: menú
- Música: nivel 1
- Música: nivel 2
- Música: nivel 3
- Música: créditos
- Música: cinemáticas
- Música: victoria
- Música: game over
### 8.7. Animaciones
-   Animación de salto del protagonista
-   Animación de trampa: pinchos
-   Animación de trampa: flechas
-   Animación de trampa: sierra
-   Animación de trampa: cuchilla giratoria
## 9. Milestones del proyecto
#### Milestone 1 - 11/ 11/ 2019

##### Objetivo: Concepto de juego y entorno de desarrollo.
-   **Programación**: Proyecto dividido en escenas. Mecánica de movimiento. Diagrama UML. Implementación de la cámara. Implementación de subida y guardado en servidor de la información.
-   **Game Design**: Establecimiento de mecánicas principales, desarrollo del juego, ambientación, reglas básicas, diagrama de flujo, mockup de pantallas. Diseño del nivel 1.
-   **Arte y assets**: Arte conceptual de personaje y trampas, logo del juego, turnaround del personaje. Modelado del personaje.
-   **Marketing y gestión**: Creación cuenta de Instagram, descubrimiento del proyecto Twitter. Versión 1 del GDD.

#### Milestone 2 - 18/ 11 / 2019 [FINALIZADO]

##### Objetivo: Primer prototipo jugable

-   **Programación**: Implementación de clase Player. Implementación de transición entre escenas. Implementación de mecánica de trampas. Implementación de la interfaz. Implementación del nivel 1. Implementación de descarga desde el servidor.
-   **Game Design**:  Diseño de tutorial, de niveles 2 y 3 y de todo el flujo de juego.
-   **Arte y assets**: Arte de decorados de tiles y cubos. Modelado de tile/cubo y trampas.
-   **Marketing y gestión**: Finalización definitiva del GDD. Devlog en itch.io, subida a Facebook. Marketing en redes sociales. Reestructuración de la página web y portfolio del grupo. Lanzamiento de encuesta de satisfacción.

#### Milestone 3 - 25 / 11 / 2019 [FINALIZADO]

##### Objetivo: Finalización del grueso del proyecto

-   **Programación**:  Implementación de todas las pantallas. Implementación del registro de puntuación. Implementación de niveles 2 y 3. Implementación del registro de movimiento descargado desde la nube. Implementación del matchmaking.
- **Game Design**: Reestructuración de nivel 3, si precisa.
-   **Arte y assets**: Modelado de esfera direccional y de los powerups. Rig y animación del protagonista. Texturizado de todos los elementos. 
-   **Marketing y gestión**: Cambios estéticos y estructurales en la web, devlog de Itch.io, subida a Facebook. Recogida de datos de la encuesta.

#### Milestone 4 - 2 / 12 / 2019

##### Objetivo: Detalles finales

-   **Programación**: Reemplazo de placeholders. Flujo de juego definitivo. Enlace del multijugador con el engine.
-   **Arte y assets**: Modelado de extras estéticos. Diseño de GUI. 
-   **Marketing y gestión**: Creación del tráiler, campaña de marketing de lanzamiento en redes sociales. Devlog Itch.io y subida a Facebook. Aplicación del feedback en el proyecto. Cambios finales a la web.

#### Milestone 5 - 5 / 12 / 2019

##### Objetivo: Pequeños detalles, corrección de errores y publicación
-   **Programación**: Solución de posibles bugs.
-   **Arte y assets**: Arte y modelos extras para posibles ampliaciones.
-   **Marketing y gestión**: Subida definitiva del juego a las redes. Preparación de la presentación. Retoques finales a web y a Itch.io.
## 10. Contacto y enlaces de interés
> [Twitter](https://twitter.com/AngryPineappleG "Twitter de la compañía")
 
> [Itch.io](https://angrypineapplegames.itch.io/ "Itch.io de la compañía")

> [Youtube](https://www.youtube.com/channel/UC-beom-Ex559oRHYl8knUAQ "Canal de Youtube de la compañía")

> [Facebook](https://www.facebook.com/juanantonio.martingarcia.33671?ref=bookmarks "Facebook de la compañía")
