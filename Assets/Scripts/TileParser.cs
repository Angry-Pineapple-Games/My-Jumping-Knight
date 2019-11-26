using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TileParser
{
    public List<int> GetTilesFromFile(TextAsset asset)
    {
        List<int> tileIds = new List<int>();
        string[] content = asset.text.Split(',');
        for(int i = 0; i < content.Length; i++)
        {
            tileIds.Add(int.Parse(content[i]));
        }

        return tileIds;
    }
}
