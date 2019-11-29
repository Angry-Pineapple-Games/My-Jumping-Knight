﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    Animator animator;
    bool active;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate()
    {
        animator.SetBool("activate", true);
    }

    public bool isActive()
    {
        return active;
    }

    public void setActive(bool active)
    {
        this.active = active;
    }
    
}