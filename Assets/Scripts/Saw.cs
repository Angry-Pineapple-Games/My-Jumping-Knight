using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    public float tileDistance = 4f;
    public float speed = 0.5f;
    public int startDirection = -1;
    private int direction = -1;

    // Start is called before the first frame update
    void Start()
    {
        
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
        transform.Translate(direction * speed, 0f, 0f);
    }
}
