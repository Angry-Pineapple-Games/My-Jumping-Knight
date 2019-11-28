using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TileParser
{
    public List<int> GetTilesFromFile(TextAsset asset)
    {
        List<int> tileIds = new List<int>();
        string[] lines = asset.text.Split(';');
        for(int i = lines.Length - 1; i >= 0; i--)
        {
            string[] content = lines[i].Split(',');
            for(int j = 0; j < content.Length; j++)
            {
                tileIds.Add(int.Parse(content[j]));
            }
        }
        return tileIds;
    }
}
