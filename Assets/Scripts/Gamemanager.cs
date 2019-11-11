using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{

    public Player P1;
    public List<Tile> tiles;
    public int currentTileId;
    public int gridH;
    public int gridW;
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
                currentTileId += gridW;
                P1.Move(Player.Direction.up);
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(currentTileId - gridW >= 0)
            {
                currentTileId -= gridW;
                P1.Move(Player.Direction.down);
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if((currentTileId + 1) % gridW  != 0)
            {
                currentTileId++;
                P1.Move(Player.Direction.right);
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(((currentTileId - 1) % gridW != (gridW - 1)) && (currentTileId -1) >= 0)
            {
                currentTileId--;
                P1.Move(Player.Direction.left);
            }
            
        }
    }
}
