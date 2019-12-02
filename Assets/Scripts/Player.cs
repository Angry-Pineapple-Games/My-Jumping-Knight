using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Parameters

    public Vector2 pos;
    public Gamemanager.Direction orientation;
    private int health = 3;
    private bool shield = false;

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

    public void Move(Gamemanager.Direction direction)
    {
        switch (direction)
        {
            case Gamemanager.Direction.up:
                pos.y++;
                transform.Translate(0, 0, -10);
                break;
            case Gamemanager.Direction.down:
                pos.y--;
                transform.Translate(0, 0, 10);
                break;
            case Gamemanager.Direction.right:
                pos.x++;
                transform.Translate(-10, 0, 0);
                break;
            case Gamemanager.Direction.left:
                pos.x--;
                transform.Translate(10, 0, 0);
                break;
            default:
                Debug.Log("Error at Player.Move(): default case in switch");
                break;
        }
        //trasladar al transform (debera cambiarse por animacion de salto)
    }
    public void Fall(Gamemanager.Direction direction)
    {
        Debug.Log("oops, i fell");
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "SpikesActivator")
        {
            Spikes spikes = collider.gameObject.GetComponentInChildren<Spikes>();
            if (!spikes.isActive())
            {
                spikes.Activate();
            }
        }

        if(collider.gameObject.tag == "Hazard")
        {
            GetHit();
        }

        if(collider.gameObject.tag == "Powerup")
        {
            Powerup powerup = collider.gameObject.GetComponent<Powerup>();
            powerup.GetPowerUp(this);
        }
    }

    public void GetHit()
    {
        if (shield)
        {
            shield = false;
        }
        else if (health > 0)
        {
            health--;
        }
        else
        {
            Debug.Log("GameOver");
        }
        Debug.Log("Ouch");
    }

    public void healHealth()
    {
        if(health < 3)
        {
            health++;
        }
        Debug.Log("healed");
    }

    public void obtainShield()
    {
        shield = true;
    }

    public void obtainHourglass()
    {
        Debug.Log("Got clock");
    }

    #region getters and setters

    public int getHealth()
    {
        return health;
    }

    #endregion
}
