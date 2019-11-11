using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{

    public Player P1;
    public List<Tile> tiles;
    public int currentTileId;
    //public Vector2 gridSize;
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
            P1.Move(Player.Direction.up);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            P1.Move(Player.Direction.down);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            P1.Move(Player.Direction.right);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            P1.Move(Player.Direction.left);
        }
    }
}
