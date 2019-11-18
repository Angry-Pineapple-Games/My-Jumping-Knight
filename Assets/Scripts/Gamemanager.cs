using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    #region Parameters
    public Player P1;
    public List<Tile> tiles;
    public int currentTileId;
    public int gridH;
    public int gridW;
    public int stepCounter;
    #endregion

    #region Enumerations
    public enum Direction { up, right, left, down };
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        
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
    }
}
