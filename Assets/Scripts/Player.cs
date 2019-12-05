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
    public Vector3 lastTile;

    private Animator animator;
    public Renderer modelRenderer;
    public GameObject shieldObject;

    public int currentTileId;

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
        if (!invincible)
        {
            if (shield)
            {
                shield = false;
                shieldObject.SetActive(false);
            }
            else if (health > 0)
            {
                health--;
                animator.SetTrigger("Damage");
                invincible = true;
            }
            else
            {
                Debug.Log("GameOver");
                animator.SetTrigger("Damage");
            }
            Debug.Log("Ouch");
        }
        
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
        shieldObject.SetActive(true);
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
