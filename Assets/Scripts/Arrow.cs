using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 0.1f;
    public float limit = 100f;
    public float speedChange = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.localPosition.z < limit)
        {
            transform.Translate(0, 0, speed * speedChange);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void SlowDown()
    {
        speedChange *= 0.5f;
    }

    private void RestoreTime()
    {
        speedChange *= 2f;
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
