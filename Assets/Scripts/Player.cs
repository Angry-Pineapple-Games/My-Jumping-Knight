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
    public float timeInterval = 5f;
    private Vector3 targetTile;
    public bool jumping = false;
    public float speed = 0.2f;

    private Animator animator;
    
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
        animator = GetComponentInChildren<Animator>();
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
        if (jumping && targetTile != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetTile, speed);
            if(transform.position == targetTile)
            {
                jumping = false;
            }
        }
    }

    public void Move(Gamemanager.Direction direction)
    {
        switch (direction)
        {
            case Gamemanager.Direction.up:
                pos.y++;
                
                transform.GetChild(0).transform.localEulerAngles = new Vector3(0, 180, 0);
                break;
            case Gamemanager.Direction.down:
                pos.y--;
                
                transform.GetChild(0).transform.localEulerAngles = new Vector3(0, 0, 0);
                break;
            case Gamemanager.Direction.right:
                pos.x++;
                
                transform.GetChild(0).transform.localEulerAngles = new Vector3(0, -90, 0);
                break;
            case Gamemanager.Direction.left:
                pos.x--;
                
                transform.GetChild(0).transform.localEulerAngles = new Vector3(0, 90, 0);
                break;
            default:
                Debug.Log("Error at Player.Move(): default case in switch");
                break;
        }
        //trasladar al transform (debera cambiarse por animacion de salto)
        animator.SetTrigger("Jump");
        jumping = true;
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
            animator.SetTrigger("Damage");
        }
        else
        {
            Debug.Log("GameOver");
            animator.SetTrigger("Damage");
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

    public void SetTargetTile(Tile target)
    {
        Vector3 pos = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
        targetTile = pos;
    }

    #endregion
}
