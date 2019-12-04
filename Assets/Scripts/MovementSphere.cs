using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSphere : MonoBehaviour
{
    public Gamemanager gamemanager;
    public Player player;
    public Gamemanager.Direction direction;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseUp()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray)){
            switch (direction)
            {
                case Gamemanager.Direction.up:
                    gamemanager.InputUp(player);
                    break;
                case Gamemanager.Direction.down:
                    gamemanager.InputDown(player);
                    break;
                case Gamemanager.Direction.right:
                    gamemanager.InputRight(player);
                    break;
                case Gamemanager.Direction.left:
                    gamemanager.InputLeft(player);
                    break;
                default:
                    Debug.Log("Error in Movement Sphere.");
                    break;
            }
        };
    }
}
