using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{
    #region InEditorParameters
    public Player P1;
    public Player P2;
    public int gridH;
    public int gridW;
    public int stepCounter;
    public TextAsset levelTxt;
    public int countDown;
    public Text textCountDown;
    #endregion

    #region Prefabs
    public Tile TilePrefab;
    public Tile ArrowTilePrefab;
    public Tile SpikesTilePrefab;
    public Tile SawTilePrefab;
    public Tile EmptySawTilePrefab;
    public Tile BladeTilePrefab;
    public Tile HeartTilePrefab;
    public Tile ShieldTilePrefab;
    public Tile HourglassTilePrefab;

    #endregion

    #region Parameters
    private List<Tile> tiles;
    private int startTileId = 0;
    private int goalTileId;
    private Coroutine oponentMove; //rutina que controlará los movimientos del oponente
    private ManagerAPI managerAPI;
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
        start = 11
    };
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        //Instanciacion del nivel
        tiles = new List<Tile>();
        TileParser parser = new TileParser();
        List<float> tileIds = parser.GetTilesFromFile(levelTxt);
        int idX;
        int idY;
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
                    tiles.Add(createTile(idX, idY, TilePrefab));
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
                    tiles[i].GetComponentInChildren<Saw>().tileDistance = (tileIds[i] * 100) % 10;
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
                default:
                    Debug.Log("Id de tile erroneo." + tileIds[i]);

                    break;
            }
        }
        idX = startTileId % gridW;
        idY = startTileId / gridW;
        P1.currentTileId = startTileId;
        P1.transform.Translate(-10 * idX, 0, -10 * idY);
        P2.currentTileId = startTileId;
        P2.transform.Translate(-10 * idX, 0, -10 * idY);

    }

    // Update is called once per frame
    void Update()
    {
        //Inputs
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            InputUp(P1);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            InputDown(P1);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            InputRight(P1);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            InputLeft(P1);
        }

        if (P1.currentTileId == goalTileId)
        {
            Debug.Log("Goal");
        }
    }

    #region Inputs
    public void InputUp(Player player)
    {
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
                    stepCounter++;
                }
                else if (nextTile.walkable)
                {
                    player.currentTileId += gridW;
                    player.SetTargetTile(new Vector3(nextTile.transform.position.x, player.transform.position.y, nextTile.transform.position.z));
                    player.Move(Direction.up);
                    stepCounter++;
                }
            }
            else
            {
                player.SetTargetTile(new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z - 10));
                player.SetLastTile(new Vector3(lastTile.transform.position.x, player.transform.position.y, lastTile.transform.position.z));
                player.Fall(Direction.up);
                stepCounter++;
            }
        }
    }

    public void InputDown(Player player)
    {
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
                    stepCounter++;
                }
                else if (nextTile.walkable)
                {
                    player.currentTileId -= gridW;
                    player.SetTargetTile(new Vector3(nextTile.transform.position.x, player.transform.position.y, nextTile.transform.position.z));
                    player.Move(Direction.down);
                    stepCounter++;
                }
            }
            else
            {
                player.SetTargetTile(new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 10));
                player.SetLastTile(new Vector3(lastTile.transform.position.x, player.transform.position.y, lastTile.transform.position.z));
                player.Fall(Direction.down);
                stepCounter++;
            }
        }
    }

    public void InputRight(Player player)
    {
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
                    stepCounter++;
                }
                else if (nextTile.walkable)
                {
                    player.currentTileId++;
                    player.SetTargetTile(new Vector3(nextTile.transform.position.x, player.transform.position.y, nextTile.transform.position.z));
                    player.Move(Direction.right);
                    stepCounter++;
                }
            }
            else
            {
                player.SetTargetTile(new Vector3(player.transform.position.x - 10, player.transform.position.y, player.transform.position.z));
                player.SetLastTile(new Vector3(lastTile.transform.position.x, player.transform.position.y, lastTile.transform.position.z));
                player.Fall(Direction.right);
                stepCounter++;
            }
        }
    }

    public void InputLeft(Player player)
    {
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
                    stepCounter++;
                }
                else if (nextTile.walkable)
                {
                    player.currentTileId--;

                    player.SetTargetTile(new Vector3(nextTile.transform.position.x, player.transform.position.y, nextTile.transform.position.z));
                    player.Move(Direction.left);
                    stepCounter++;
                }
            }
            else
            {
                player.SetTargetTile(new Vector3(player.transform.position.x + 10, player.transform.position.y, player.transform.position.z));
                player.SetLastTile(new Vector3(lastTile.transform.position.x, player.transform.position.y, lastTile.transform.position.z));
                player.Fall(Direction.left);
                stepCounter++;
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

    public void GameOver()
    {
        Debug.Log("GameOver");
    }

    /*Cuenta atrás para el comienzo de la partida y prepara lo necesario del oponente
     llamar con StartCoroutine(método) cuando se deba empezar la cuenta atrás*/
    IEnumerator StartCountDown()
    {
        string[] move = managerAPI.GetRandomOponent();
        for (int i = countDown; i > 0; i--)
        {
            textCountDown.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        oponentMove = StartCoroutine(OponentMove(move));
        GameObject.Destroy(textCountDown);
    }

    /*Realiza los movimientos del oponente*/
    IEnumerator OponentMove(string[] move)
    {
        for (int i = 0; i < move.Length; i += 2)
        {
            yield return new WaitForSeconds(float.Parse(move[i]));
            Direction action = (Direction)int.Parse(move[i + 1]);
            if (action == Direction.up) { InputUp(P2); }
            else if (action == Direction.right) { InputRight(P2); }
            else if (action == Direction.left) { InputLeft(P2); }
            else if (action == Direction.down) { InputDown(P2); }
        }
    }
}
