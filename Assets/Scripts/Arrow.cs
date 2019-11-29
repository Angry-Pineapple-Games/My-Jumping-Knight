using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 0.1f;
    public float limit = 100f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.localPosition.z < limit)
        {
            transform.Translate(0, 0, speed);
        }
        else
        {
            Destroy(gameObject);
        }
        
        
    }
}
