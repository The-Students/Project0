using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDirt : TileWallBase {

    void Start()
    {
        TileType = TileTypes.Dirt;
    }

    // Use this for initialization
    public override void Initialize()
    {
        base.Initialize();
    }

    // Update is called once per frame
}
