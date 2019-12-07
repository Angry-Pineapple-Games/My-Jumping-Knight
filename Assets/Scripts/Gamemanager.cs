using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    #region InEditorParameters
    public Player P1;
    public Player P2;
    public string currentLevel;
    public float minRankSPlus;
    public int gridH;
    public int gridW;
    public TextAsset levelTxt;
    public string userName = "Anon";
    public string oponentName = "Anon";
    public int countDown;
    
    public bool multiplayer = false;
    public bool autoplay = false;
    public int numDoors = 0;
    public int numPortals = 0;
    #endregion

    #region Ranks
    [SerializeField] private float MAX_TIME_SPLUS;
    [SerializeField] private float MAX_TIME_S;
    [SerializeField] private float MAX_TIME_APLUS;
    [SerializeField] private float MAX_TIME_A;
    [SerializeField] private float MAX_TIME_B;
    #endregion

    #region Prefabs
    public Tile TilePrefab;
    public Tile TilePrefab2;
    public Tile ArrowTilePrefab;
    public Tile SpikesTilePrefab;
    public Tile SawTilePrefab;
    public Tile EmptySawTilePrefab;
    public Tile BladeTilePrefab;
    public Tile HeartTilePrefab;
    public Tile ShieldTilePrefab;
    public Tile HourglassTilePrefab;
    public Tile DoorTilePrefab;
    public Tile ButtonTilePrefab;
    public Tile PortalTilePrefab;

    #endregion

    #region Parameters
    private List<Tile> tiles;
    private Door[] doors;
    private DoorButton[] buttons;
    private Door[] doorsP2;
    private DoorButton[] buttonsP2;
    private Portal[] portals;
    private int startTileId = 0;
    private int goalTileId;
    private float globalTimer = 0.0f;
    private float currentTimer = 0.0f;
    private string currentMatch;
    public int stepCounter = 280;
    [HideInInspector]
    public bool start = false;
    [HideInInspector]
    public bool end = false;
    CultureInfo myCIintl = new CultureInfo("en-US", false);
    private Coroutine oponentMove; //rutina que controlará los movimientos del oponente
    private Coroutine playerMove; //rutina que controlará los movimientos del jugador en autoplay
    private ManagerAPI managerAPI;
    private const string GAMEOVER = "GameOverScene";
    private const string VICTORY = "VictoryScene";
    private const string MULTIPLAYERKEY = "MultiplayerGame";
    private const string AUTOPLAYKEY = "AutoplayGame";
    #endregion

    #region UI Elements

    public Text textCountDown;
    public Text textTimer;
    public Text textName;
    public Text textOponent;
    public Text textSteps;

    #endregion

    #region Enumerations
    public enum Direction { up, right, left, down };
    public enum TileType
    {
        none = 0,
        empty = 1,
        arrow = 2,
        saw = 3,
        shield = 4,
        spikes = 5,
        heart = 6,
        blade = 7,
        clock = 8,
        emptySaw = 9,
        goal = 10,
        start = 11,
        button = 12,
        door = 13,
        portal = 14
    };
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("ApiClient(Clone)") != null)
        {
            managerAPI = GameObject.Find("ApiClient(Clone)").GetComponent<ManagerAPI>();
            userName = managerAPI.myUsername;
        }
        Thread.CurrentThread.CurrentCulture = myCIintl;
        currentMatch += userName + " ";
        multiplayer = PlayerPrefs.GetInt(MULTIPLAYERKEY) == 1;
        autoplay = PlayerPrefs.GetInt(AUTOPLAYKEY) == 1;

        //Instanciacion del nivel
        tiles = new List<Tile>();
        TileParser parser = new TileParser();
        List<float> tileIds = parser.GetTilesFromFile(levelTxt);
        int idX;
        int idY;
        doors = new Door[numDoors];
        buttons = new DoorButton[numDoors];
        portals = new Portal[numPortals];
        
        if (multiplayer)
        {
            doorsP2 = new Door[numDoors];
            buttonsP2 = new DoorButton[numDoors];
        }
        for (int i = 0; i < tileIds.Count; i++)
        {
            idX = i % gridW;
            idY = i / gridW;
            switch ((TileType)Mathf.FloorToInt(tileIds[i]))
            {
                case TileType.none:
                    tiles.Add(null);
                    break;
                case TileType.empty:
                    if((idX + (idY % 2)) % 2 == 0)
                    {
                        tiles.Add(createTile(idX, idY, TilePrefab));
                    }
                    else
                    {
                        tiles.Add(createTile(idX, idY, TilePrefab2));
                    }
                    break;
                case TileType.arrow:
                    tiles.Add(createTile(idX, idY, ArrowTilePrefab));
                    if (tileIds[i] == 2.1f)
                    {
                        tiles[i].transform.Rotate(0, 180, 0);
                    }
                    else if (tileIds[i] == 2.2f)
                    {
                        tiles[i].transform.Rotate(0, -90, 0);
                    }
                    else if (tileIds[i] == 2.3f)
                    {
                        tiles[i].transform.Rotate(0, 90, 0);
                    }
                    break;
                case TileType.saw:
                    tiles.Add(createTile(idX, idY, SawTilePrefab));
                    Saw[] saws = tiles[i].GetComponentsInChildren<Saw>();
                    saws[0].tileDistance = (tileIds[i] * 100) % 10;
                    saws[1].tileDistance = (tileIds[i] * 100) % 10;
                    if (tileIds[i] >= 3.1f)
                    {
                        tiles[i].transform.Rotate(0, 180, 0);
                    }
                    else if (tileIds[i] >= 3.2f)
                    {
                        tiles[i].transform.Rotate(0, -90, 0);
                    }
                    else if (tileIds[i] >= 3.3f)
                    {
                        tiles[i].transform.Rotate(0, 90, 0);
                    }
                    break;
                case TileType.shield:
                    tiles.Add(createTile(idX, idY, ShieldTilePrefab));
                    break;
                case TileType.spikes:
                    tiles.Add(createTile(idX, idY, SpikesTilePrefab));
                    break;
                case TileType.heart:
                    tiles.Add(createTile(idX, idY, HeartTilePrefab));
                    break;
                case TileType.blade:
                    tiles.Add(createTile(idX, idY, BladeTilePrefab));
                    break;
                case TileType.clock:
                    tiles.Add(createTile(idX, idY, HourglassTilePrefab));
                    break;
                case TileType.emptySaw:
                    tiles.Add(createTile(idX, idY, EmptySawTilePrefab));
                    if (tileIds[i] == 9.1f)
                    {
                        tiles[i].transform.Rotate(0, 90, 0);
                    }
                    break;
                case TileType.goal:
                    tiles.Add(createTile(idX, idY, TilePrefab));
                    goalTileId = i;
                    break;
                case TileType.start:
                    tiles.Add(createTile(idX, idY, TilePrefab));
                    startTileId = i;
                    break;
                case TileType.door:
                    tiles.Add(createTile(idX, idY, DoorTilePrefab));
                    int doorIndex = Mathf.RoundToInt((tileIds[i] * 10) % 10);
                    Door[] bothDoors = tiles[i].GetComponentsInChildren<Door>();
                    foreach(Door d in bothDoors)
                    {
                        if(d.tag == "Hazard")
                        {
                            doors[doorIndex - 1] = d;
                        }else if(multiplayer && d.tag == "HazardP2")
                        {
                            doorsP2[doorIndex - 1] = d;
                        }
                    }
                    break;
                case TileType.button:
                    tiles.Add(createTile(idX, idY, ButtonTilePrefab));
                    int buttonIndex = Mathf.RoundToInt((tileIds[i] * 10) % 10);
                    DoorButton[] bothButtons = tiles[i].GetComponentsInChildren<DoorButton>();
                    foreach(DoorButton db in bothButtons)
                    {
                        if(db.tag == "Button")
                        {
                            buttons[buttonIndex - 1] = db;
                        } else if(multiplayer && db.tag == "ButtonP2")
                        {
                            buttonsP2[buttonIndex - 1] = db;
                        }
                    }
                    break;
                case TileType.portal:
                    tiles.Add(createTile(idX, idY, PortalTilePrefab));
                    int portalIndex = Mathf.RoundToInt((tileIds[i] * 10) % 10) - 1;
                    Portal newPortal = tiles[i].GetComponentInChildren<Portal>();
                    newPortal.tileId = i;
                    if (portals[portalIndex] == null)
                    {
                        portals[portalIndex] = newPortal;
                    }
                    else
                    {
                        portals[portalIndex].otherPortal = newPortal;
                        newPortal.otherPortal = portals[portalIndex];
                    }
                    break;
                default:
                    tiles.Add(null);
                    break;
            }
        }
        if(numDoors > 0)
        {
            for(int i = 0; i < numDoors; i++)
            {
                buttons[i].door = doors[i];
                if (multiplayer)
                {
                    buttonsP2[i].door = doorsP2[i];
                }
            }
        }
        idX = startTileId % gridW;
        idY = startTileId / gridW;
        P1.currentTileId = startTileId;
        P1.transform.Translate(-10 * idX, 0, -10 * idY);
        if (multiplayer)
        {
            P2.currentTileId = startTileId;
            P2.transform.Translate(-10 * idX, 0, -10 * idY);
        }
        else
        {
            P2.gameObject.SetActive(false);
        }

        if (autoplay)
        {
            MovementSphere[] movementSpheres = P1.GetComponentsInChildren<MovementSphere>();
            foreach(MovementSphere movementSphere in movementSpheres)
            {
                movementSphere.gameObject.SetActive(false);
            }
        }

        if (!multiplayer)
        {
            GameObject[] multiplayerHazards = GameObject.FindGameObjectsWithTag("HazardP2");
            foreach(GameObject hazard in multiplayerHazards)
            {
                hazard.SetActive(false);
            }
            GameObject[] powerupObjects = GameObject.FindGameObjectsWithTag("Powerup");
            foreach(GameObject powerupObject in powerupObjects)
            {
                powerupObject.GetComponent<Powerup>().multiplayer = false;
            }
        }

        StartCoroutine(StartCountDown());
    }

    // Update is called once per frame
    void Update()
    {
        if (start && !end)
        {
            globalTimer += Time.deltaTime;
            textTimer.text = "" + Mathf.FloorToInt(globalTimer);
            currentTimer += Time.deltaTime;
            //Inputs
            if (!autoplay)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
                {
                    InputUp(P1);
                }
                if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
                {
                    InputDown(P1);
                }
                if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
                {
                    InputRight(P1);
                }
                if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
                {
                    InputLeft(P1);
                }
            }
            
            if (!end && (P1.currentTileId == goalTileId))
            {
                EndMatch();
            }
            if(stepCounter <= 0)
            {
                GameOver(P1);
            }
        }
    }

    #region Inputs
    public void InputUp(Player player)
    {
        if (player.tag == "Player1")
        {
            currentMatch += currentTimer + " " + 0 + " ";
            currentTimer = 0.0f;
            stepCounter--;
            textSteps.text = "" + stepCounter;
        }
            
        if (!player.jumping && !player.falling)
        {
            Tile lastTile = tiles[player.currentTileId];
            if (player.currentTileId + gridW < tiles.Count)
            {
                Tile nextTile = tiles[player.currentTileId + gridW];

                if (nextTile == null)
                {
                    player.SetTargetTile(new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z - 10));
                    player.SetLastTile(new Vector3(lastTile.transform.position.x, player.transform.position.y, lastTile.transform.position.z));
                    player.Fall(Direction.up);
                }
                else if ((player.tag == "Player1" && nextTile.walkable) || (player.tag == "Player2" && nextTile.walkableP2))
                {
                    player.currentTileId += gridW;
                    player.SetTargetTile(new Vector3(nextTile.transform.position.x, player.transform.position.y, nextTile.transform.position.z));
                    player.Move(Direction.up);
                }
            }
            else
            {
                player.SetTargetTile(new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z - 10));
                player.SetLastTile(new Vector3(lastTile.transform.position.x, player.transform.position.y, lastTile.transform.position.z));
                player.Fall(Direction.up);
            }
        }
    }

    public void InputDown(Player player)
    {
        if (player.tag == "Player1")
        {
            currentMatch += currentTimer + " " + 3 + " ";
            currentTimer = 0.0f;
            stepCounter--;
            textSteps.text = "" + stepCounter;
        }
        if (!player.jumping && !player.falling)
        {
            Tile lastTile = tiles[player.currentTileId];
            if (player.currentTileId - gridW >= 0)
            {

                Tile nextTile = tiles[player.currentTileId - gridW];
                if (nextTile == null)
                {
                    player.SetTargetTile(new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 10));
                    player.SetLastTile(new Vector3(lastTile.transform.position.x, player.transform.position.y, lastTile.transform.position.z));
                    player.Fall(Direction.down);
                }
                else if ((player.tag == "Player1" && nextTile.walkable) || (player.tag == "Player2" && nextTile.walkableP2))
                {
                    player.currentTileId -= gridW;
                    player.SetTargetTile(new Vector3(nextTile.transform.position.x, player.transform.position.y, nextTile.transform.position.z));
                    player.Move(Direction.down);
                }
            }
            else
            {
                player.SetTargetTile(new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 10));
                player.SetLastTile(new Vector3(lastTile.transform.position.x, player.transform.position.y, lastTile.transform.position.z));
                player.Fall(Direction.down);
            }
        }
    }

    public void InputRight(Player player)
    {
        if (player.tag == "Player1")
        {
            currentMatch += currentTimer + " " + 1 + " ";
            currentTimer = 0.0f;
            stepCounter--;
            textSteps.text = "" + stepCounter;
        }
        if (!player.jumping && !player.falling)
        {
            Tile lastTile = tiles[player.currentTileId];
            if ((player.currentTileId + 1) % gridW != 0)
            {
                Tile nextTile = tiles[player.currentTileId + 1];
                if (nextTile == null)
                {
                    player.SetTargetTile(new Vector3(player.transform.position.x - 10, player.transform.position.y, player.transform.position.z));
                    player.SetLastTile(new Vector3(lastTile.transform.position.x, player.transform.position.y, lastTile.transform.position.z));
                    player.Fall(Direction.right);
                }
                else if ((player.tag == "Player1" && nextTile.walkable) || (player.tag == "Player2" && nextTile.walkableP2))
                {
                    player.currentTileId++;
                    player.SetTargetTile(new Vector3(nextTile.transform.position.x, player.transform.position.y, nextTile.transform.position.z));
                    player.Move(Direction.right);
                }
            }
            else
            {
                player.SetTargetTile(new Vector3(player.transform.position.x - 10, player.transform.position.y, player.transform.position.z));
                player.SetLastTile(new Vector3(lastTile.transform.position.x, player.transform.position.y, lastTile.transform.position.z));
                player.Fall(Direction.right);
            }
        }
    }

    public void InputLeft(Player player)
    {
        if (player.tag == "Player1")
        {
            currentMatch += currentTimer + " " + 2 + " ";
            currentTimer = 0.0f;
            stepCounter--;
            textSteps.text = "" + stepCounter;
        }
        if (!player.jumping && !player.falling)
        {
            Tile lastTile = tiles[player.currentTileId];
            if (((player.currentTileId - 1) % gridW != (gridW - 1)) && (player.currentTileId - 1) >= 0)
            {
                Tile nextTile = tiles[player.currentTileId - 1];
                if (nextTile == null)
                {
                    player.SetTargetTile(new Vector3(player.transform.position.x + 10, player.transform.position.y, player.transform.position.z));
                    player.SetLastTile(new Vector3(lastTile.transform.position.x, player.transform.position.y, lastTile.transform.position.z));
                    player.Fall(Direction.left);
                }
                else if ((player.tag == "Player1" && nextTile.walkable) || (player.tag == "Player2" && nextTile.walkableP2))
                {
                    player.currentTileId--;

                    player.SetTargetTile(new Vector3(nextTile.transform.position.x, player.transform.position.y, nextTile.transform.position.z));
                    player.Move(Direction.left);
                }
            }
            else
            {
                player.SetTargetTile(new Vector3(player.transform.position.x + 10, player.transform.position.y, player.transform.position.z));
                player.SetLastTile(new Vector3(lastTile.transform.position.x, player.transform.position.y, lastTile.transform.position.z));
                player.Fall(Direction.left);
            }
        }
    }

    #endregion

    Tile createTile(int xId, int yId, Tile prefab)
    {
        Tile tile = Instantiate(prefab, this.transform);
        tile.transform.Translate(-10 * xId, 0, -10 * yId);
        return tile;
        
    }

    public void addMatchToFile()
    {
        using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(Application.dataPath + "/PresetMatches.txt", true))
        {
            file.WriteLine(currentMatch);
        }
    }

    public void GameOver(Player player)
    {
        if(player.tag == "Player1")
        {
            EndMatch();
        }
        else
        {
            player.transform.gameObject.SetActive(false);
        }
        
    }

    /*Gestiona el final de partida, convocando las llamadas al servidor si procede*/
    public void EndMatch()
    {
        end = true;
        currentMatch += globalTimer + " " + P1.getHealth();
        if (!autoplay)
        {
            managerAPI.SaveRecordLevelUser(currentLevel, currentMatch, minRankSPlus, P1.getHealth(), globalTimer);
            //addMatchToFile();
            managerAPI.myGlobalTime = globalTimer;
        }
        if (P1.getHealth() <= 0 || stepCounter <= 0 || (multiplayer && globalTimer < float.Parse(managerAPI.oponentGlobalTime)))
            SceneManager.LoadScene(GAMEOVER);
        else
            SceneManager.LoadScene(VICTORY);
    }

    /*Cuenta atrás para el comienzo de la partida y prepara lo necesario del oponente
     llamar con StartCoroutine(método) cuando se deba empezar la cuenta atrás*/
    IEnumerator StartCountDown()
    {
        
        for (int i = countDown; i > 0; i--)
        {
            textCountDown.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        start = true;
        GameObject.Destroy(textCountDown);
        if(multiplayer && managerAPI != null)
        {
            string[] moveP2 = managerAPI.GetRandomOponent();
            oponentName = managerAPI.oponentUsername;
            oponentMove = StartCoroutine(BotMove(moveP2, P2));
            textOponent.text = oponentName;
        }
        if (autoplay)
        {
            string[] moveP1 = managerAPI.GetBestPlayerLevel(currentLevel);
            playerMove = StartCoroutine(BotMove(moveP1, P1));
        }
        textName.text = userName;
    }

    /*Realiza los movimientos del oponente*/
    IEnumerator BotMove(string[] move, Player player)
    {
        for (int i = 0; i < move.Length; i += 2)
        {
            yield return new WaitForSeconds(float.Parse(move[i]));
            Direction action = (Direction)int.Parse(move[i + 1]);
            if (action == Direction.up) { InputUp(player); }
            else if (action == Direction.right) { InputRight(player); }
            else if (action == Direction.left) { InputLeft(player); }
            else if (action == Direction.down) { InputDown(player); }
        }
    }
}
