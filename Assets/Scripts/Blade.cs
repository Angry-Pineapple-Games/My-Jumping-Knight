using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SlowDown()
    {
        animator.speed = 0.5f;
    }

    private void RestoreTime()
    {
        animator.speed = 1f;
    }

    private void OnEnable()
    {
        Player.OnTimeStopped += SlowDown;
        Player.OnTimeStarted += RestoreTime;
    }

    private void OnDisable()
    {
        Player.OnTimeStopped -= SlowDown;
        Player.OnTimeStarted -= RestoreTime;
    }
}
