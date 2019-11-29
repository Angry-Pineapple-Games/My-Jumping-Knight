using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Parameters

    public Vector2 pos;
    public Gamemanager.Direction orientation;

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
        if (collider.gameObject.tag == "Spikes")
        {
            Spikes spikes = collider.gameObject.GetComponent<Spikes>();
            if (spikes.isActive())
            {
                GetHit();
            }
        }
    }

    public void GetHit()
    {
        Debug.Log("Ouch");
    }
}
