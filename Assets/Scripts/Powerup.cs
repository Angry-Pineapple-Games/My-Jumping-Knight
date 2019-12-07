using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    bool player1 = false;
    bool player2 = false;
    public bool multiplayer = true;
    public MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetPowerUp(Player player)
    {
        if(player.tag == "Player1")
        {
            if (!player1)
            {
                player1 = true;
                if (player2 || !multiplayer)
                {
                    this.transform.gameObject.SetActive(false);
                }
                else
                {
                    meshRenderer.material.color = new Color(0f, 0f, 1f, 0.5f);
                }
                DoEffect(player);
            }
        }else if(player.tag == "Player2")
        {
            if (!player2)
            {
                player2 = true;
                if (player1)
                {
                    this.transform.gameObject.SetActive(false);
                }
                DoEffect(player);
            }
        }
    }
    public virtual void DoEffect(Player player) { }
}
