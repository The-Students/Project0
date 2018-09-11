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

public enum Direction
{
    North = 0,
    East = 1,
    South = 2,
    West = 3,
    NorthWest = 4,
    NorthEast = 5,
    SouthWest = 6,
    SouthEast = 7
}