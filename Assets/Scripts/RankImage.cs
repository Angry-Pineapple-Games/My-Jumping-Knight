using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankImage : MonoBehaviour
{
    public Sprite RankC;
    public Sprite RankB;
    public Sprite RankA;
    public Sprite RankAPlus;
    public Sprite RankS;
    public Sprite RankSPlus;

    // Start is called before the first frame update
    void Start()
    {
        Image image = GetComponent<Image>();
        string rank = PlayerPrefs.GetString("lastRank");
        switch (rank)
        {
            case "C":
                image.sprite = RankC;
                break;
            case "B":
                image.sprite = RankB;
                break;
            case "A":
                image.sprite = RankC;
                break;
            case "A+":
                image.sprite = RankAPlus;
                break;
            case "S":
                image.sprite = RankS;
                break;
            case "S+":
                image.sprite = RankSPlus;
                break;
            default:
                Debug.Log("No rank found");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
