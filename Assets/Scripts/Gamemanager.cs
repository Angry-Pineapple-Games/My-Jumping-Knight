using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    #region InEditorParameters
    public Player P1;
    public int gridH;
    public int gridW;
    public int stepCounter;
    public TextAsset levelTxt;
    
    #endregion

    #region Prefabs
    public Tile TilePrefab;
    public Tile ArrowTilePrefab;
    public Tile SpikesTilePrefab;
    public Tile SawTilePrefab;
    public Tile EmptySawTilePrefab;
    public Tile BladeTilePrefab;
    #endregion

    #region Parameters
    private List<Tile> tiles;
    private int currentTileId;
    private int startTileId = 0;
    private int goalTileId;
    #endregion

    #region Enumerations
    public enum Direction { up, right, left, down };
    public enum TileType {
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
        start = 11 };
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
                    if(tileIds[i] == 2.1f)
                    {
                        tiles[i].transform.Rotate(0, 180, 0);
                    }else if(tileIds[i] == 2.2f)
                    {
                        tiles[i].transform.Rotate(0, -90, 0);
                    }else if(tileIds[i] == 2.3f)
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
                    tiles.Add(createTile(idX, idY, TilePrefab));
                    break;
                case TileType.spikes:
                    tiles.Add(createTile(idX, idY, SpikesTilePrefab));
                    break;
                case TileType.heart:
                    tiles.Add(createTile(idX, idY, TilePrefab));
                    break;
                case TileType.blade:
                    tiles.Add(createTile(idX, idY, BladeTilePrefab));
                    break;
                case TileType.clock:
                    tiles.Add(createTile(idX, idY, TilePrefab));
                    break;
                case TileType.emptySaw:
                    tiles.Add(createTile(idX, idY, EmptySawTilePrefab));
                    if(tileIds[i] == 9.1f)
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
        currentTileId = startTileId;
        P1.transform.Translate(-10 * idX, 0, -10 * idY);
            
    }

    // Update is called once per frame
    void Update()
    {
        //Inputs
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(currentTileId + gridW < tiles.Count)
            {
                Tile nextTile = tiles[currentTileId + gridW];

                if (nextTile == null)
                {
                    P1.Fall(Direction.up);
                    stepCounter++;
                }
                else if (nextTile.walkable)
                {
                    currentTileId += gridW;
                    P1.Move(Direction.up);
                    stepCounter++;
                }
            }
            else
            {
                P1.Fall(Direction.up);
                stepCounter++;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(currentTileId - gridW >= 0)
            {
                Tile nextTile = tiles[currentTileId - gridW];
                if (nextTile == null)
                {
                    P1.Fall(Direction.down);
                    stepCounter++;
                }
                else if (nextTile.walkable)
                {
                    currentTileId -= gridW;
                    P1.Move(Direction.down);
                    stepCounter++;
                }
            }
            else
            {
                P1.Fall(Direction.down);
                stepCounter++;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if((currentTileId + 1) % gridW  != 0)
            {
                Tile nextTile = tiles[currentTileId + 1];
                if (nextTile == null)
                {
                    P1.Fall(Direction.right);
                    stepCounter++;
                }
                else if (nextTile.walkable)
                {
                    currentTileId ++;
                    P1.Move(Direction.right);
                    stepCounter++;
                }
            }
            else
            {
                P1.Fall(Direction.right);
                stepCounter++;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(((currentTileId - 1) % gridW != (gridW - 1)) && (currentTileId -1) >= 0)
            {
                Tile nextTile = tiles[currentTileId -1];
                if (nextTile == null)
                {
                    P1.Fall(Direction.left);
                    stepCounter++;
                }
                else if (nextTile.walkable)
                {
                    currentTileId--;
                    P1.Move(Direction.left);
                    stepCounter++;
                }
            }
            else
            {
                P1.Fall(Direction.left);
                stepCounter++;
            }

        }
        if(currentTileId == goalTileId)
        {
            Debug.Log("Goal");
        }
    }



    Tile createTile(int xId, int yId, Tile prefab)
    {
        Tile tile = Instantiate(prefab, this.transform);
        tile.transform.Translate(-10 * xId, 0, -10 * yId);
        return tile;
    }
}
