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
    private bool clock = false;
    float timePassed = 0.0f;
    public float timeInterval = 8f;
    
    #endregion

    #region Events

    public delegate void StopTime();
    public static event StopTime OnTimeStopped;

    public delegate void StartTime();
    public static event StartTime OnTimeStarted;

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        pos = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (clock)
        {
            timePassed += Time.deltaTime;
            if(timePassed >= timeInterval)
            {
                timePassed = 0.0f;
                restoreTime();
            }
        }
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
        if(OnTimeStopped != null)
            OnTimeStopped();
        clock = true;
        Debug.Log("Got clock");
    }

    public void restoreTime()
    {
        if (OnTimeStarted != null)
            OnTimeStarted();
        clock = false;
        Debug.Log("Time back");
    }

    #region getters and setters

    public int getHealth()
    {
        return health;
    }

    #endregion
}
