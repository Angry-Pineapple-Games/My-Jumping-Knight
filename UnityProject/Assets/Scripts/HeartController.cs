using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartController : MonoBehaviour
{
    public RawImage heart1;
    public RawImage heart2;
    public RawImage heart3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealthUI(int health)
    {
        switch (health)
        {
            case 3:
                ShowHeart(heart3);
                break;
            case 2:
                HideHeart(heart3);
                ShowHeart(heart2);
                break;
            case 1:
                HideHeart(heart2);
                ShowHeart(heart1);
                break;
            case 0:
                HideHeart(heart1);
                break;
            default:
                Debug.Log("Too much health");
                break;
        }
    }

    void HideHeart(RawImage heart)
    {
        heart.CrossFadeAlpha(0.3f, 0.1f, false);
    }

    void ShowHeart(RawImage heart)
    {
        heart.CrossFadeAlpha(1.0f, 0.1f, false);
    }
}
