using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    public float tileDistance = 4f;
    public float speed = 0.5f;
    private float speedChange = 1f;
    public int startDirection = -1;
    private int direction = -1;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(transform.localPosition.x) * transform.lossyScale.x >= tileDistance * 10f * transform.localScale.x)
        {
            direction = - startDirection;
        }
        if(transform.localPosition.x * startDirection <= 0)
        {
            direction = startDirection;
        }
        transform.Translate(direction * speed * speedChange, 0f, 0f);
    }

    private void SlowDown()
    {
        speedChange = 0.5f;
        animator.speed = 0.5f;
    }

    private void RestoreTime()
    {
        speedChange = 1f;
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
