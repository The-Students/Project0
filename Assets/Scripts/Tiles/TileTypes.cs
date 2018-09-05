using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileTypes
{
    Empty = -1,
    Dirt = 0,
    Rock = 1,
    Gem = 2,
    Path = 3,
    Lava = 4,
    Water = 5,
    Building = 6
}

public struct EmptySides
{
    public bool North;
    public bool East;
    public bool South;
    public bool West;
    public int GetAmount()
    {
        int temp = 0;
        if (North) temp++;
        if (East) temp++;
        if (South) temp++;
        if (West) temp++;
        return temp;
    }
}