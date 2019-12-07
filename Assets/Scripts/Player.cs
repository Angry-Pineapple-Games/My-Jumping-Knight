using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Parameters

    public Vector2 pos;
    public Gamemanager.Direction orientation;
    public Gamemanager gamemanager;
    private int health = 3;
    private bool shield = false;
    private bool clock = false;
    private bool invincible = false;
    float timePassed = 0.0f;
    public float timeInterval = 8f;
    float iFramesSecs = 0.0f;
    int iFrames = 0;
    public float totalIFrames = 3f;
    private Vector3 targetTile;
    private Vector3 fallPos;
    public bool jumping = false;
    public float speed = 1f;
    public bool falling = false;
    private bool teleporting = false;
    public Vector3 lastTile;
    public HeartController heartsUI;

    private Animator animator;
    public Renderer modelRenderer;
    public GameObject shieldObject;

    public int currentTileId;

    #endregion

    #region Events

    public delegate void StopTime();
    public static event StopTime OnTimeStopped;
    public delegate void StopTimeP2();
    public static event StopTimeP2 OnTimeStoppedP2;

    public delegate void StartTime();
    public static event StartTime OnTimeStarted;
    public delegate void StartTimeP2();
    public static event StartTimeP2 OnTimeStartedP2;

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        pos = new Vector2(0, 0);
        animator = GetComponentInChildren<Animator>();
        shieldObject.SetActive(false);
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
        if (invincible)
        {
            iFramesSecs += Time.deltaTime;
            iFrames++;
            if (iFrames % 10 == 0)
            {
                modelRenderer.enabled = false;
            }
            else
            {
                modelRenderer.enabled = true;
            }
            if(iFramesSecs >= totalIFrames)
            {
                iFramesSecs = 0.0f;
                iFrames = 0;
                invincible = false;
                modelRenderer.enabled = true;
            }
        }
        if (jumping && targetTile != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetTile, speed);
            if(Vector3.Distance(transform.position, targetTile) <= 0.001f)
            {
                jumping = false;
            }
        }
        if(falling && !jumping && targetTile != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, fallPos, speed);
            if (Vector3.Distance(transform.position, fallPos) <= 0.001f)
            {
                GetHit();
                transform.position = lastTile;
                falling = false;
            }
        }
    }

    #region Movement
    public void Move(Gamemanager.Direction direction)
    {
        Rotate(direction);
        animator.SetTrigger("Jump");
        jumping = true;
    }
    public void Fall(Gamemanager.Direction direction)
    {
        Rotate(direction);
        animator.SetTrigger("Jump");
        falling = true;
        jumping = true;
    }

    public void Rotate(Gamemanager.Direction direction)
    {
        switch (direction)
        {
            case Gamemanager.Direction.up:
                transform.GetChild(0).transform.localEulerAngles = new Vector3(0, 180, 0);
                break;
            case Gamemanager.Direction.down:
                transform.GetChild(0).transform.localEulerAngles = new Vector3(0, 0, 0);
                break;
            case Gamemanager.Direction.right:
                transform.GetChild(0).transform.localEulerAngles = new Vector3(0, -90, 0);
                break;
            case Gamemanager.Direction.left:
                transform.GetChild(0).transform.localEulerAngles = new Vector3(0, 90, 0);
                break;
            default:
                Debug.Log("Error at Player.Rotate(): default case in switch");
                break;
        }
    }
    #endregion

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "SpikesActivator")
        {
            if (tag == "Player1")
            {
                Spikes spikes = collider.gameObject.GetComponentInChildren<Spikes>();
                if (!spikes.isActive())
                {
                    spikes.Activate();
                }
            }
        }

        if(collider.gameObject.tag == "SpikesActivatorP2")
        {
            if (tag == "Player2")
            {
                Spikes spikes = collider.gameObject.GetComponentInChildren<Spikes>();
                if (!spikes.isActive())
                {
                    spikes.Activate();
                }
            }
        }

        if(collider.gameObject.tag == "Hazard")
        {
            if(tag == "Player1")
            {
                GetHit();
            }
        }

        if(collider.gameObject.tag == "HazardP2")
        {
            if (tag == "Player2")
            {
                GetHit();
            }
        }

        if(collider.gameObject.tag == "Powerup")
        {
            Powerup powerup = collider.gameObject.GetComponent<Powerup>();
            powerup.GetPowerUp(this);
        }

        if(collider.gameObject.tag == "Button")
        {
            if(tag == "Player1")
            {
                DoorButton doorButton = collider.gameObject.GetComponent<DoorButton>();
                doorButton.OpenMyDoor();
            }
        }

        if (collider.gameObject.tag == "ButtonP2")
        {
            if (tag == "Player2")
            {
                DoorButton doorButton = collider.gameObject.GetComponent<DoorButton>();
                doorButton.OpenMyDoor();
            }
        }

        if (collider.gameObject.tag == "Portal")
        {
            if (!teleporting)
            {
                Portal portal = collider.gameObject.GetComponent<Portal>();
                Teleport(portal.otherPortal);
                teleporting = true;
            }
            else
            {
                teleporting = false;
            }
            
        }
    }

    public void GetHit()
    {
        if (!invincible)
        {
            if (shield)
            {
                shield = false;
                shieldObject.SetActive(false);
            }
            else if (health > 1)
            {
                health--;
                animator.SetTrigger("Damage");
                invincible = true;
            }
            else
            {
                health--;
                gamemanager.GameOver(this);
            }
            if (heartsUI != null)
                heartsUI.UpdateHealthUI(health);
        }
        
    }

    public void Teleport(Portal portal)
    {
        currentTileId = portal.tileId;
        transform.position = new Vector3(portal.transform.position.x, transform.position.y, portal.transform.position.z);
        jumping = false;
    }

    #region PowerUp Effects
    public void healHealth()
    {
        if(health < 3)
        {
            health++;
        }
        if(heartsUI != null)
            heartsUI.UpdateHealthUI(health);
    }
    
    public void obtainShield()
    {
        shield = true;
        shieldObject.SetActive(true);
    }

    public void obtainHourglass()
    {
        if(tag == "Player1")
        {
            if (OnTimeStopped != null)
                OnTimeStopped();
        }
        if(tag == "Player2")
        {
            if (OnTimeStoppedP2 != null)
                OnTimeStoppedP2();
        }
        clock = true;
        Debug.Log("Got clock");
    }

    public void restoreTime()
    {
        if (tag == "Player1")
        {
            if (OnTimeStarted != null)
                OnTimeStarted();
        }
        if (tag == "Player2")
        {
            if (OnTimeStartedP2 != null)
                OnTimeStartedP2();
        }
        clock = false;
        Debug.Log("Time back");
    }
    #endregion

    #region getters and setters

    public int getHealth()
    {
        return health;
    }

    public void SetTargetTile(Vector3 target)
    {
        targetTile = target;
        fallPos = new Vector3(target.x, target.y - 50, target.z);
    }

    public void SetLastTile(Vector3 target)
    {
        lastTile = target;
    }

    #endregion
}
