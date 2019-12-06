using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Animator animator;
    Tile tile;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        tile = GetComponentInParent<Tile>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open()
    {
        tile.walkable = true;
        animator.SetTrigger("Open");
    }
}
