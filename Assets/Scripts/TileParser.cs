using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TileParser
{
    public List<float> GetTilesFromFile(TextAsset asset)
    {
        List<float> tileIds = new List<float>();
        string[] lines = asset.text.Split(';');
        for(int i = lines.Length - 1; i >= 0; i--)
        {
            string[] content = lines[i].Split(',');
            for(int j = 0; j < content.Length; j++)
            {
                tileIds.Add(float.Parse(content[j], System.Globalization.CultureInfo.InvariantCulture.NumberFormat));
            }
        }
        return tileIds;
    }
}
