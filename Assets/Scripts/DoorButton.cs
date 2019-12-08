using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour
{
    public Door door;
    public Material activeMaterial;
    public MeshRenderer meshRenderer;
    private bool active = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenMyDoor()
    {
        if (!active)
        {
            door.Open();
            meshRenderer.material = activeMaterial;
            active = true;
        }
    }

}
