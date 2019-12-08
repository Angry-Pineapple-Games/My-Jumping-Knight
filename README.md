# My Jumping Knight: Documento de Diseño de Juego
![](https://raw.githubusercontent.com/Angry-Pineapple-Games/My-Jumping-Knight/master/UnityProject/Bocetos/Logo/logo-decorated.png)
> Angry Pineapple Games

> Versión del Documento: 1.2

> Plantilla de GDD adaptada por: [Benjamin “HeadClot” Stanley](https://docs.google.com/document/d/1-I08qX76DgSFyN1ByIGtPuqXh7bVKraHcNIA25tpAzE/ "Enlace al documento de plantilla")

> Escrito con [StackEdit](https://stackedit.io/).

## 1. Historial de Versiones
* 0.1: Esqueleto del GDD y primera idea conceptual.
* 0.2: Corrección de errores y apartados de gameplay en detalle.
* 1.0: Redacción de todos los apartados detallados.
* 1.1: Inclusión de los modelos de negocio y monetización.
* 1.2: Cambio en imágenes y ciertos apartados.
## 2. Concepto de Juego
### 2.1. Género y Setting
My Jumping Knight es un juego de *running by tapping* en 3D con vista isométrica en el que el jugador deberá manejar a un caballero que se mueve casilla a casilla por un tablero lleno de trampas hasta alcanzar la salida, todo ello con ambientación medieval.

![](https://raw.githubusercontent.com/Angry-Pineapple-Games/My-Jumping-Knight/master/UnityProject/Assets/UI%20Elements/casco.png "Icono del caballero")

Permite además enfrentarse a otros jugadores mediante la subida y descarga del recorrido que ha hecho el jugador en distintas iteraciones del juego, evitando así lag y dando lugar a un enfrentamiento muy preciso. De la misma forma, también se pueden poner datos de dos jugadores a competir entre ellos.

### 2.2. Mecánicas de juego principales
* **Avance en casillas**: El personaje no se moverá libremente por el escenario, sino que avanzará a una casilla adyacente a la que se encuentra, según dónde le indique el jugador al pulsar en pantalla o controlar el teclado.
* **Limitación del movimiento**: El jugador tendrá un límite de 280 movimientos para superar el nivel. Si consume todos sus movimientos, perderá la partida.
* **Trampas**: Habrá diferentes trampas en el escenario que tendrán efectos negativos sobre el jugador, como restarle una vida.
* **Power Ups**: Se encontrarán esparcidos por el mapa varios power ups que el personaje podrá utilizar.
* **Competición**: Se incentivará al jugador a competir tanto consigo mismo como con otros jugadores. Se desarrolla esta idea en los subapartados de "Puntuación" y "Modos de Juego".
* **Desarrollo por niveles**: Se ha tomado la decisión de dividir el juego en tres niveles con dificultad incremental y diferente ambientación para poder fomentar el factor competitivo del juego.
* **Entrenamiento**: El objetivo principal del modo individual es entrenar al caballero. Para ello, el jugador deberá repetir varias veces el nivel para obtener un recorrido mejor que los que ha hecho hasta ahora, y este recorrido se subirá a un servidor para que compita con otros jugadores y bots.
### 2.3. Plataformas
El juego se desarrollará para ser jugado en web, tanto en dispositivos móviles y tablets como en ordenador.
### 2.4. Idiomas
Estará disponible en los idiomas inglés y español.
### 2.5. Alcance y escala del proyecto
-   **Escala económica y de tiempo**:
    -   **Presupuesto inicial**: 0 euros.
    -   **Fecha de inicio**: 4 de noviembre de 2019.
    -   **Fecha de finalización**: 8 de diciembre de 2019.
-   **Equipo**:
    -   **Mario Aceituno Cordero**: Concept art, texturizado y diseño GUI.
    -   **Javier Albaráñez Martínez**: Programación.
    -   **César Carbajo García**: Programación.
    -   **Juan Antonio Martín García**: Gestión de proyecto, game design, documentación, guión y diseño narrativo, community management y diseño GUI.
    -   **Laura Suonpera Lozano**: 3D Art, modelado, texturizado y diseño GUI.
### 2.6. Influencias
* **Redungeon**: Juego de running con desarrollo procedimental en el que se ha tomado inspiración para la mecánica de movimiento y las trampas.
* **Crossy Road**: Juego de estilo *Frogger* procedimental donde se avanza haciendo *taps* en la pantalla. Ha sido una influencia a la hora de escoger el enfoque que se quería hacer al juego para ofrecer al jugador una experiencia inmediata y de fácil aprendizaje.
* **Crypt of the Necrodancer**: Otro juego donde el movimiento se basa en desplazamientos por una cuadrícula pudiendo avanzar hacia zonas adyacentes dentro del grid.
## 3. Elevator Pitch
*My Jumping Knight* es un juego competitivo que reta al jugador a encontrar el camino más corto hasta la meta de la manera más rápida posible, haciendo uso de su ingenio y su habilidad.
Se orienta a jugadores competitivos, que disfruten de juegos de ingenio y dinámicos, y que cuenten con periodos pequeños de tiempo para jugar.
Se puede competir contra otra gente sin conexión a internet, y brinda la posibilidad de usar las mejores partidas de cada usuario para enfrentarse a otros jugadores, aunque no se cuente con tiempo para ello, gracias al modo de juego automático.
## 4. Gameplay
### 4.1. Gameplay resumido
Mediante interacción del jugador se avanzará por un tablero con forma irregular que estará lleno de trampas que el personaje deberá esquivar. Si no consigue esquivar alguna de estas trampas, perderá una vida, teniendo un total de tres. Las trampas plantearán retos de habilidad y también retos de inteligencia, recompensando a dos tipos de jugadores distintos y premiando enormemente al jugador equilibrado que sepa entender cuándo debe avanzar rápido y cuándo debe a pararse a resolver un pequeño puzle. También habrá power ups que el jugador podrá coger para obtener un beneficio.
### 4.2. Gameplay en detalle
#### 4.2.1. Flujo de recorrido del usuario
El jugador comenzará con una pantalla con el logo del juego y un botón de comienzo. Acto seguido, se le dará la posibilidad de iniciar sesión y/o registrarse, y tras ello llegará a la pantalla principal de menú, que permitirá escoger entre Jugar, Tutorial, Opciones y Créditos.
![](https://raw.githubusercontent.com/Angry-Pineapple-Games/My-Jumping-Knight/master/UnityProject/Game%20Design/Capturas%20GDD/3_main.png "Pantalla del menú principal")Al escoger en opciones, se podrá cambiar el idioma y desactivar el sonido y/o la música del juego. En créditos se podrá ver información del equipo y las licencias utilizadas. Al escoger tutorial, comenzará una partida a una pequeña pantalla donde se explicará al jugador cómo jugar al juego y cómo funciona su dinámica.
![](https://raw.githubusercontent.com/Angry-Pineapple-Games/My-Jumping-Knight/master/UnityProject/Game%20Design/Capturas%20GDD/6_tuto.png "Pantalla de tutorial")
En caso de escoger jugar, tendrá que elegir entre modo individual o multijugador. En multijugador, a su vez, entre partida automática (caballero contra caballero, sin control del jugador) o manual. Y tras ello, escoger entre uno de los tres niveles para jugar.
![](https://raw.githubusercontent.com/Angry-Pineapple-Games/My-Jumping-Knight/master/UnityProject/Game%20Design/Capturas%20GDD/7_single.png "Pantalla de selección de nivel")
Al perder o ganar un nivel, podrá volver al menú de selección de nivel o al menú principal.

El usuario, además, podrá retroceder a las pantallas anteriores del menú.
![](https://raw.githubusercontent.com/Angry-Pineapple-Games/My-Jumping-Knight/master/UnityProject/Game%20Design/Diagrama%20de%20flujo%20de%20pantallas.png "Flujo de recorrido del usuario V2")
#### 4.4.2. Información en pantalla
Durante la partida, el jugador podrá apreciar varios elementos en la interfaz: un reloj que muestra el tiempo transcurrido, unos corazones que indican la vida restante, unos orbes que indican hacia dónde puede moverse el jugador (se situarán en las casillas adyacentes a la casilla en donde esté situado el jugador), su nombre de usuario y unas botas acompañadas de un número que indica la cantidad de pasos restante.
![](https://raw.githubusercontent.com/Angry-Pineapple-Games/My-Jumping-Knight/master/UnityProject/Game%20Design/Capturas%20GDD/11_lv1.png "Interfaz")
#### 4.4.3. Desarrollo de una partida
El jugador comenzará en la parte inicial del nivel y deberá sortear obstáculos y trampas para llegar al final. Algunas secuencias de trampas requerirán de habilidad, mientras que otras funcionarán a modo de puzles y exigirán al jugador pensar la solución lo más rápido posible.
#### 4.4.4. Trampas
* **Pinchos**: Los pinchos son una trampa oculta que saltarán en cuanto el jugador se sitúe encima de la casilla en donde estos se encuentren.

![](https://raw.githubusercontent.com/Angry-Pineapple-Games/My-Jumping-Knight/master/UnityProject/Bocetos/Traps/spikes-trap-color.png "Bloque con pinchos")

* **Sierra**: Trampa que se transporta lateralmente de un lado a otro. El jugador tendrá que contemporizar para pasar por el camino sin entrar en contacto con ella.

![](https://raw.githubusercontent.com/Angry-Pineapple-Games/My-Jumping-Knight/master/UnityProject/Bocetos/Traps/saw-trap-color.png "Bloque con sierra")

* **Flechas**: Las flechas se disparan continuamente en un intervalo de tiempo concreto, por lo que el jugador deberá contemporizar para esquivarlas.

![](https://raw.githubusercontent.com/Angry-Pineapple-Games/My-Jumping-Knight/master/UnityProject/Bocetos/Traps/arrow-trap-color.png "Hoja de detalles del lanzaflechas y la flecha")

* **Cuchilla giratoria**: El eje se situará en una casilla y la cuchilla girará alrededor de él, moviéndose por las casillas adyacentes a dicho eje, obligando al jugador a sortear el obstáculo.

![](https://raw.githubusercontent.com/Angry-Pineapple-Games/My-Jumping-Knight/master/UnityProject/Bocetos/Traps/blade-trap-color.png "Posibles diseños de la cuchilla giratoria")

* **Portales**: No son una trampa en si misma, pero habrá casillas que, en cuanto detecten que el jugador se sitúe sobre ellas, le teletransportarán a otra posición en la que se encuentre el portal emparejado.

* **Botones y puertas**: Las puertas bloquean el paso del jugador, mientras que los botones (generalmente situados en otra zona del mapa), accionan las puertas y permiten al jugador pasar a través de ellas.

![](https://raw.githubusercontent.com/Angry-Pineapple-Games/My-Jumping-Knight/master/UnityProject/Game%20Design/Capturas%20GDD/13_lv3.png "Pantallazo del tercer nivel, con portal y botón")

#### 4.4.5. Power Ups
* **Corazón**: El corazón, como es habitual en cualquier juego con sistema de vidas, recuperará una vida al jugador. Se puede coger un corazón aunque se conserven las tres vidas, pero este no tendrá efecto.
* **Escudo**: El escudo prevendrá el daño de la próxima trampa con la que interactúe el jugador, es decir, que evitará que el jugador pierda una vida en su siguiente encuentro con una trampa.
* **Reloj de arena**: El reloj de arena hará que el tiempo (salvo el caballero) vaya más despacio durante unos segundos, por lo que el jugador podrá tomar ventaja para sortear ciertas trampas o avanzar más rápido por alguna sección del escenario.
![](https://raw.githubusercontent.com/Angry-Pineapple-Games/My-Jumping-Knight/master/UnityProject/Bocetos/Power-Ups/hourglass-color.png "Diseño del reloj de arena")
#### 4.4.6. Cámara
La cámara se colocará con una vista isométrica, situándose encima del personaje y ligeramente detrás y a la derecha. Seguirá el movimiento al personaje, permitiendo ver siempre parte del camino que hay alrededor de este.
#### 4.4.7. Controles
En ordenador se podrá controlar con teclado (teclas direccionales o WASD) y por ratón (pulsando en una casilla a la que se pueda mover el personaje), mientras que en dispositivos táctiles se controlará pulsando (un *tap*) sobre la casilla a la que se pueda mover el personaje.
#### 4.4.8. Puntuación
Al finalizar cada partida, se comprobará el tiempo que ha tardado el jugador en finalizar el nivel, los pasos restantes y sus vidas restantes, lo cual determinará un rango que va desde C hasta S+. Además, este rango y el tiempo se compararán con el TOP10 de mejores registros locales del jugador, y se incluirán en dicho TOP10 en caso de que proceda. Se subirá al servidor el mejor recorrido que ha hecho un jugador basándose en su rango.
![](https://raw.githubusercontent.com/Angry-Pineapple-Games/My-Jumping-Knight/master/UnityProject/Game%20Design/Capturas%20GDD/10_victory.png "Pantalla de victoria, con rango A obtenido")
#### 4.4.9. Modos de juego
* **Individual**: El jugador podrá jugar los 3 niveles del juego, desbloqueándolos uno a uno e intentando sacar el mejor rango y tiempo posible.
* **Multijugador**: El jugador se enfrentará a un caballero de otro jugador que se ha descargado, intentando finalizar el nivel antes de que lo haga el otro. El matchmaking se realizará aplicando lógica difusa: al jugador que está buscando jugar online se le asignará un jugador cuya puntuación media en las partidas sea similar a la suya.
* **Automático**: El caballero de un jugador se enfrentará de manera automática contra el caballero de otro jugador que se haya descargado y emparejado por el mismo criterio de matchmaking que en el modo multijugador. No habrá intervención directa del jugador en estas partidas.
#### 4.4.10. Sistema de guardado
El sistema de guardado del jugador permitirá recordar sus mejores puntuaciones, así como los niveles que ha desbloqueado. Para ello, se guardará una estructura de datos asociada a su nombre de usuario.
## 5. Posibles ampliaciones
* **Niveles extra**: El juego base cuenta con tres niveles, pero podría expandirse hasta donde la creatividad y el presupuesto alcanzase.
* **Más trampas y power-ups**: Al igual que los niveles, las trampas y power-ups podrían ampliarse añadiendo nuevas mecánicas con las que complicar futuros niveles. Se detallarán más adelante ejemplos de estas ampliaciones.
* **Enemigos**: En caso de contar con más tiempo para ampliar el juego, sería muy interesante añadir varios enemigos con comportamientos distintos: algunos perseguirían al protagonista, otros le lanzarían objetos, otros robarían power-ups... Añadir enemigos le daría una nueva capa de complejidad al juego.
* **Skins**: Distintas paletas de colores y/o trajes del caballero para darle una apariencia distinta y permitir al jugador personalizarlo ligeramente acorde con sus gustos.
* **Personajes controlables con habilidades**: Se dispondrá de varios personajes desbloqueables, cada uno de ellos con su propia historia y con una habilidad que los distinga del resto. Se detallarán estos personajes y habilidades en posteriores versiones del documento.
* **Más jugadores en una misma partida**: Se podrían enfrentar varios bots en una misma partida, para dar una mayor sensación de dinamismo. 
* **Stream de partidas, votaciones y chat**: En los enfrentamientos entre bots, se podrá habilitar la posibilidad de que otros jugadores entren a ver la partida, así como se contará con un chat en el que los jugadores podrán comentar la partida. Además, antes del comienzo de la simulación, los jugadores podrán votar por el bot que consideran que va a ganar, y se recompensará a los que acierten.
* **Nivel procedimental y pseudo-infinito**: Los niveles del juego no pueden ser procedimentales tal como se han planteado, ya que no tendría sentido incentivar la competitividad de jugador vs bot o bot vs bot en niveles que no estén preestablecidos (o, al menos, se requeriría de un sistema de aprendizaje mucho más complejo que el que se plantea en este proyecto, por lo que está fuera de la escala). Sin embargo, sí sería interesante contar con un nivel exclusivo del modo individual, sin límite de pasos, que permitiese a los jugadores simplemente jugar e intentar alcanzar la mayor distancia posible en un casillero que sería completamente distinto en cada una de las partidas.
* **Editor de niveles**: Se pondría a disposición de los jugadores un sistema que permitiese crear sus propios niveles en un grid y compartirlos en la nube para que otros jugadores pudiesen descargárselos. Se podría puntuar estos niveles (destacando en un hub los mejor puntuados) y marcarlos como favoritos para poder acceder fácilmente a ellos.
* **Retos de temporada con recompensas**: Una vez un jugador ha terminado los niveles de _My Jumping Knight_, su mayor objetivo en el juego es entrenar a su caballero para mejorar y competir, por lo que el primer objetivo de cara a ampliar al juego sería hacer un sistema de eventos de temporada en los que se incentive a los jugadores a competir. Para ello, cada dos semanas se reiniciarán los datos del servidor y se avisará en la pantalla de título de que un nuevo reto tiene lugar y que los jugadores tendrán que plantear nuevas formas de entrenar a su caballero. Estos retos serán reglas que reformularán la manera de disfrutar el título, como pueden ser: completar el nivel sin pasar dos veces por la misma casilla, sin perder vidas, lo más lento posible, obteniendo todos los power-ups del nivel... De esta manera, el entrenamiento del caballero en el modo individual cambiaría totalmente en cada nuevo reto de temporada, ya que se debería afrontar otra manera de jugar e incentivaría a los jugadores a seguir disfrutando del juego.
## 6. Modelo de Negocio
-   **Socios clave**: Plataformas donde publicar el juego (Facebook Instant Games, Itch.io) y plataformas donde publicitar el juego y la empresa (Github Pages, Twitter, Instagram, Verkami, Patreon).
-   **Actividades clave**: Comunicación activa en redes sociales, solución de problemas con rapidez gracias a herramientas como [Mantis Bug Tracker](https://www.mantisbt.org/index.php) y focalización en la experiencia de juego: el jugador no va a encontrarse ningún tipo de publicidad dentro del propio juego, sino externo a él (en redes sociales, en la web...). Incentivación a jugar habitualmente y/o gastar dinero para conseguir contenido jugable, manteniendo al jugador cercano al producto.
-   **Recursos clave**: Unity, Adobe Animate con licencia de estudiante, 3DS Max con licencia de estudiante, IDE PyCharm para crear el servidor. Plataformas online para la obtención de sonidos y música con licencia libre. Recursos humanos: programadores, artistas, modeladora, game designer, guionista, community manager.
-   **Propuesta de valor**: Ofrecer un juego que pueda ser disfrutado tanto a nivel individual como a nivel social, que incentive la competición con otros jugadores, pero también con uno mismo. Se ofrece una experiencia similar a otras del mercado, pero con toques únicos como la competición entre bots, que nos destaquen con algo distintivo por delante de otros competidores.
-   **Relación con los clientes**: Comunicación activa en redes sociales para obtener fidelidad y transparencia en las metas de donaciones.
-   **Canales**: Distribución en Facebook Instant Games e Itch.io.
-   **Segmento de clientes**: Se busca a un público que quiera disfrutar de partidas rápidas y que disfrute de ver cómo su progreso como jugador se refleja en otros apartados del juego, pudiendo echar una partida y luego enviar a su caballero a competir contra otros.
-   **Fuente de ingresos**: Especificado en el apartado 7 del documento.
-   **Estructura de costes**: Para el juego base, se parte de un presupuesto nulo, aprovechando las licencias de estudiante para los distintos programas utilizados. Para las ampliaciones, los costes se especifican en el apartado 7 del documento.
## 7. Monetización
Se ha optado por un modelo de monetización híbrido entre el modelo *freemium* y el de *crowdfunding*. 

Por una parte, todo el contenido del juego será totalmente gratuito, y se podrá ir desbloqueando utilizando una moneda _ingame_ llamada **gema**, que será el equivalente a un céntimo, de manera que por cada 100 gemas se tendrá el equivalente a un euro. Todo el contenido adicional estará bloqueado y requerirá previo paso por caja (con gemas) para poder obtenerlo. Habrá varias formas de conseguir gemas:
* **Compra directa**: Para el jugador que quiera acelerar las cosas, se podrán comprar gemas que permitan comprar inmediatamente la cantidad equivalente a los euros que quiera. El equivalente a 1 euro son 100 gemas.
* **Ranking de temporada**: Cada temporada online, el jugador recibirá una cantidad de gemas proporcional a su posición en la tabla de clasificaciones, recompensando así a los jugadores más destacados.
* ***Login* habitual**: Para recompensar a los jugadores fieles que entren al juego cada día, se le otorgará una cantidad muy pequeña de gemas cada día a todo jugador que haya entrado también al día anterior. Además, si un jugador entra todos los días de la semana, el domingo recibirá una cantidad adicional de gemas. Se obtendrán 3 gemas por cada *login* diario y 5 adicionales si se ha hecho *login* todos los dias de la semana
* **Juego individual**: En el juego individual se conseguirán gemas finalizando el nivel 3 con una puntuación de rango A o superior (otra vez, una cantidad proporcional al rango), pero solamente una vez al día y una cantidad pequeña, evitando así que los jugadores puedan explotar este método para la obtención de sus gemas. Se obtendrán 3 gemas por un rango A, 5 por un rango A+, 8 por un rango S y 12 por un rango S+.

En adición a la compra de gemas, también se podrá comprar directamente un objeto que desbloqueará los niveles 2, 3 y el modo online desde un inicio, sin necesidad de terminar el juego. Este paquete saldrá por el precio de 9.99€ y, pese a que su compra no será muy habitual, no supone un daño tenerlo disponible para cualquier usuario que quiera comprarlo.

De esta manera, no se privará a ningún jugador de obtener ningún tipo de contenido del juego incluso aunque solo tenga interés por jugar al modo individual, pero se podrán obtener beneficios económicos gracias a jugadores menos pacientes que quieran acceder a ese contenido cuanto antes. 

Este es un método probado en otros juegos como *Hearthstone*, *Magic the Gathering: Arena*, *Redungeon* o *Fire Emblem Heroes*, por lo que cabría esperar una buena acogida siempre que el contenido de pago fuese de la calidad que merece.

Por otro lado, de cara a ampliar el juego, se requerirá de un presupuesto adicional que se obtendrá mediante *crowdfunding*. El contenido adicional que se desarrolle con este sistema se implementará en el juego y se tendrá que desbloquear a base de la compra con gemas, aunque, como es evidente, para cualquier *backer* que haya donado algo igual o superior a un baremo establecido, se le desbloqueará desde un inicio, ya que ha contribuido previamente. 

Se establece también que no todo el contenido adicional será de pago ni requerirá de mecenazgo: la intención es que el sistema de eventos de temporada no esté de inicio, pero que sea una ampliación gratuita que se incorpore a corto plazo, ya que es la mayor incentivación para los jugadores competitivos, que el segmento de mercado que mejor se quiere cubrir. El mantenimiento y la ampliación del juego durarían dos años desde su lanzamiento hasta el finde esta campaña, por lo que se desarrollará todo lo que se pueda dados los recursos económicos en ese período de tiempo. Las metas serán las siguientes:
1. **Paquete 1 de 3 niveles extra, con nuevas trampas y power-ups:**: Se implementarán 3 niveles extra con nuevas mecánicas. Se podrán comprar individualmente por 400 gemas o en un paquete con los 3 por 1000 gemas. Se desarrollará cuando se alcance la meta de **20.000 euros.**
2. **Paquete 1 con 2 personajes extra y 2 skins para cada uno de los personajes (incluyendo al caballero original):** Se incluirán 2 personajes extra y se añadirá una habilidad exclusiva para el caballero para que no sea un personaje estrictamente peor que el resto. Además, se incluirán 2 skins adicionales para cada uno de los 3 personajes. Se podrán comprar las skins por separado por 100 gemas, cada uno de los personajes por 600 gemas, un paquete de personaje + todas sus skins por 700 gemas o el paquete con todo el contenido adicional de esta ampliación (los 2 personajes y un total de 6 skins) por 1400 gemas. Se desarrollará cuando se alcance la meta de **50.000 euros.**
3. **Modo bot vs bot ampliado para más jugadores:** El modo previamente explicado. Se podrá obtener por el precio de 2000 gemas. Se desarrollará cuando se alcance la meta de **70.000 euros.**
4. **Paquete 2 de 3 niveles extra, con enemigos como novedad:** Se implementarán 3 niveles extra que contarán con enemigos con distintas IAs que determinen su comportamiento. Estos niveles adicionales se podrán comprar individualmente por 400 gemas o en un paquete con los 3 por 1000 gemas. Se desarrollará cuando se alcance la meta de **100.000 euros.**
5. **Nivel procedimental y pseudo-infinito para el modo multijugador:** El modo previamente explicado. Se podrá obtener por el precio de 2000 gemas. Se desarrollará cuando se alcance la meta de **120.000 euros.**
6. **Paquete 2 con 2 personajes extra, 2 skins para ellos y 1 skin extra para todos los personajes anteriores:** Se incluirán 2 personajes extra y una buena cantidad de skins. Como en la anterior ampliación, se podrán comprar los personajes por separado por 600 gemas cada uno, las skins por 100 gemas, el paquete de personaje + skins por 700, los dos personajes sin skins por 1000 y todo el paquete (2 personajes, un total de 7 skins) por 1500 gemas. Se desarrollará cuando se alcance la meta de **150.000 euros.**
7. **Editor de niveles ingame:** El modo previamente explicado. Se podrá obtener por el precio de 2000 gemas. Se desarrollará cuando se alcance la meta de **185.000 euros.**
8. **Paquete 3 de 3 niveles extra, con nuevas trampas, power-ups y enemigos:** Se implementarán 3 niveles extra con nuevas mecánicas y enemigos, siendo los niveles definitivos del juego en los que exprimir todo su potencial. Se podrán comprar individualmente por 400 gemas o en un paquete con los 3 por 1000 gemas. Se desarrollará cuando se alcance la meta de **210.000 euros.**
9. **Sistema de streaming, votos y chat:** El modo previamente explicado. Se podrá obtener por el precio de 2000 gemas. Se desarrollará cuando se alcance la meta de **240.000 euros.**
10.  **Paquete 3 con 3 personajes extra, 2 skins para ellos y 1 skin extra para todos los personajes anteriores:** Se incluirán 3 personajes extra y una buena cantidad de skins. Como en la anterior ampliación, se podrán comprar los personajes por separado por 600 gemas cada uno, las skins por 100 gemas, el paquete de personaje + skins por 700, los tres personajes sin skins por 1500 y todo el paquete (3 personajes, un total de 11 skins) por 2500 gemas. Se desarrollará cuando se alcance la meta de **260.000 euros.**

En cuanto a las recompensas a los *backers*, seguirán este modo:
1. **Donar al menos 1€:** Se incluirá al *backer* en los créditos del juego.
2. **Donar al menos 10€:**  Se incluirá al *backer* en los créditos del juego y podrá obtener un paquete de niveles o de dos personajes sin *skins* automáticamente.
3. **Donar al menos 20€:** Se incluirá al *backer* en los créditos del juego y podrá obtener dos paquetes de niveles o de dos personajes sin *skins* automáticamente **o** un modo adicional (ampliación de bot vs bot, modo streaming, editor ingame o nivel procedimental).
4. **Donar al menos 35€:** Se incluirá al *backer* en los créditos del juego y podrá obtener dos paquetes de niveles o de dos personajes sin *skins* automáticamente **y** un modo adicional (ampliación de bot vs bot, modo streaming, editor ingame o nivel procedimental).
5. **Donar al menos 50€:** El *backer* obtendrá todos los beneficios de la recompensa anterior además de una camiseta exclusiva con el logo del juego y una mención destacada en los créditos.
6. **Donar al menos 80€:** El *backer* obtendrá todos los beneficios de la recompensa anterior además de poder obtener un modo adicional y una taza exclusiva con el logo de la empresa.
7. **Donar al menos 100€:** El *backer* obtendrá todos los beneficios de la recompensa anterior, un póster de tamaño estándar y una mención especial en los créditos.
8. **Donar al menos 120€:  ** El *backer* obtendrá todos los beneficios de la recompensa anterior y podrá obtener un modo de juego y un paquete de niveles o personajes adicional.
9. **Donar al menos 180€:** El *backer* obtendrá todos los beneficios anteriores, todo el contenido adicional del juego y tendrá una mención de honor en los créditos.
10. **Donar al menos 300€:** Además de todo lo anterior, uno de los personajes adicionales se diseñará a petición del *backer* (esta opción estará limitada al número de personajes que queden por incluir, y no se aceptarán diseños inapropiados, pudiendo devolver la donación en caso de requerirlo). 
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
- Implementación del _switch_ de sonido

### 8.5. Código del Multijugador
-   Implementación de subida y guardado en servidor de la info
-   Implementación del registro de movimiento
-   Implementación de descarga desde el servidor
-   Implementación de matchmaking
-   Implementación del modo multijugador
- Implementación de registro de usuarios

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

#### Milestone 2 - 18/ 11 / 2019

##### Objetivo: Primer prototipo jugable

-   **Programación**: Implementación de clase Player. Implementación de transición entre escenas. Implementación de mecánica de trampas. Implementación de la interfaz. Implementación del nivel 1. Implementación de descarga desde el servidor.
-   **Game Design**:  Diseño de tutorial, de niveles 2 y 3 y de todo el flujo de juego.
-   **Arte y assets**: Arte de decorados de tiles y cubos. Modelado de tile/cubo y trampas.
-   **Marketing y gestión**: Finalización definitiva del GDD. Devlog en itch.io, subida a Facebook. Marketing en redes sociales. Reestructuración de la página web y portfolio del grupo. Lanzamiento de encuesta de satisfacción.

#### Milestone 3 - 25 / 11 / 2019

##### Objetivo: Finalización del grueso del proyecto

-   **Programación**:  Implementación de todas las pantallas. Implementación del registro de puntuación. Implementación de niveles 2 y 3. Implementación del registro de movimiento descargado desde la nube. Implementación del matchmaking.
- **Game Design**: Reestructuración de nivel 3, si precisa.
-   **Arte y assets**: Modelado de esfera direccional y de los powerups. Rig y animación del protagonista. Texturizado de todos los elementos. 
-   **Marketing y gestión**: Cambios estéticos y estructurales en la web, devlog de Itch.io, subida a Facebook. Recogida de datos de la encuesta.

#### Milestone 4 - 2 / 12 / 2019

##### Objetivo: Finalización del desarrollo
-   **Programación**: Reemplazo de placeholders. Flujo de juego definitivo. Enlace del multijugador con el engine.
-   **Arte y assets**: Modelado de extras estéticos. Diseño de GUI. 
-   **Marketing y gestión**: Creación del tráiler, campaña de marketing de lanzamiento en redes sociales. Devlog Itch.io y subida a Facebook. Aplicación del feedback en el proyecto. Cambios finales a la web.

#### Milestone 5 - 5 / 12 / 2019

##### Objetivo: Pequeños detalles, corrección de errores y publicación
-   **Programación**: Solución de posibles bugs.
-   **Arte y assets**: Arte y modelos extras para posibles ampliaciones.
-   **Marketing y gestión**: Subida definitiva del juego a las redes. Preparación de la presentación. Retoques finales a web y a Itch.io.

## 10. Referencias y licencias
Se han utilizado los siguientes *assets* bajo licencia Creative Commons 4.0:
* Canciones: Call to Adventure, Death and Axes, Mall, Rainbows y Teddy Bear Waltz. Todas ellas pertenecientes a [Kevin Macleod](https://incompetech.com/).
* Sonido: [Annulet of absorption, de CosmicCD](https://freesound.org/people/CosmicD/sounds/133008/).
* Skyboxes: [David Schmeltekopf, Grass Depot](http://grassdepartment.com/unityas/index.php/portfolio/dramatic-skies-skybox-collection/).

## 11. Contacto y enlaces de interés

### Angry Pineapple Games

>[Web portfolio de la compañía](https://angry-pineapple-games.github.io/apg-portfolio/)

> [Twitter](https://twitter.com/AngryPineappleG "Twitter de la compañía")
 
> [Youtube](https://www.youtube.com/channel/UC-beom-Ex559oRHYl8knUAQ "Canal de Youtube de la compañía")

> [Facebook](https://www.facebook.com/juanantonio.martingarcia.33671?ref=bookmarks "Facebook de la compañía")

> [Itch.io](https://angrypineapplegames.itch.io/ "Itch.io de la compañía")

### My Jumping Knight
> [Itch.io](https://angrypineapplegames.itch.io/my-jumping-knight "Itch.io con enlace al juego")

> [Github Pages](https://angry-pineapple-games.github.io/My-Jumping-Knight/ "Github pages con el juego funcional")

>[Facebook Instant Games (solo accesible con inicio de sesión al estar oculto para el desarrollador)](https://fb.gg/play/1192728987604922 "Enlace con Facebook funcional")

>[Github del proyecto](https://github.com/Angry-Pineapple-Games/My-Jumping-Knight)
