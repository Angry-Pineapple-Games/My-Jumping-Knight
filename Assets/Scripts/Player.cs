using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Parameters

    public Vector2 pos;
    public enum Direction {up, right, left, down}

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        pos = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(Direction direction)
    {
        switch (direction)
        {

            case Direction.up:
                pos.y++;
                transform.Translate(0, 0, -10);
                break;
            case Direction.down:
                pos.y--;
                transform.Translate(0, 0, 10);
                break;
            case Direction.right:
                pos.x++;
                transform.Translate(-10, 0, 0);
                break;
            case Direction.left:
                pos.x--;
                transform.Translate(10, 0, 0);
                break;
            default:
                Debug.Log("Error at Player.Move(): default case in switch");
                break;
        }
        //trasladar al transform (debera cambiarse por animacion de salto)
        
    }
}
