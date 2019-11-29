using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowLaucher : MonoBehaviour
{
    public Arrow arrowPrefab;
    public float timeInterval;
    float timePassed = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed >= timeInterval)
        {
            Instantiate(arrowPrefab, this.transform);
            timePassed = 0f;
        }
    }
}
