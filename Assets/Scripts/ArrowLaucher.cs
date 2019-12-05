using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowLaucher : MonoBehaviour
{
    public Arrow arrowPrefab;
    public float timeInterval;
    float timePassed = 0.0f;
    private float speedChange = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed >= timeInterval * speedChange)
        {
            Arrow newArrow = Instantiate(arrowPrefab, this.transform);
            newArrow.speedChange = 1 / speedChange;
            timePassed = 0f;
        }
    }

    private void SlowDown()
    {
        speedChange = 2f;
    }

    private void RestoreTime()
    {
        speedChange = 1f;
    }

    private void OnEnable()
    {
        if (tag == "Hazard")
        {
            Player.OnTimeStopped += SlowDown;
            Player.OnTimeStarted += RestoreTime;
        }
        if (tag == "HazardP2")
        {
            Player.OnTimeStoppedP2 += SlowDown;
            Player.OnTimeStartedP2 += RestoreTime;
        }
    }

    private void OnDisable()
    {
        if (tag == "Hazard")
        {
            Player.OnTimeStopped -= SlowDown;
            Player.OnTimeStarted -= RestoreTime;
        }
        if (tag == "HazardP2")
        {
            Player.OnTimeStoppedP2 -= SlowDown;
            Player.OnTimeStartedP2 -= RestoreTime;
        }
    }
}
