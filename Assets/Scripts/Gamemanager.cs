﻿using System.Collections;
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
    public Tile noTilePrefab;
    #endregion

    #region Parameters
    private List<Tile> tiles;
    private int currentTileId;
    #endregion

    #region Enumerations
    public enum Direction { up, right, left, down };
    public enum TileType { none = 0, empty = 1, arrow = 2};
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        TileParser parser = new TileParser();
        List<int> tileIds = parser.GetTilesFromFile(levelTxt);
        int idX;
        int idY;
        for (int i = 0; i < tileIds.Count; i++)
        {
            idX = i % gridW;
            idY = i / gridW;
            switch ((TileType)tileIds[i])
            {
                case TileType.none:
                    tiles.Add(createTile(idX, idY, noTilePrefab));
                    break;
                case TileType.empty:
                    tiles.Add(createTile(idX, idY, TilePrefab));
                    break;
                case TileType.arrow:
                    tiles.Add(createTile(idX, idY, ArrowTilePrefab));
                    break;
                default:
                    Debug.Log("Id de tile erroneo.");
                    break;
            }
        }
            
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

                if (nextTile.hole)
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
                if (nextTile.hole)
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
                if (nextTile.hole)
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
                if (nextTile.hole)
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
    }



    Tile createTile(int xId, int yId, Tile prefab)
    {
        Tile tile = Instantiate(prefab, this.transform);
        tile.transform.Translate(-10 * xId, 0, -10 * yId);
        return tile;
    }
}
