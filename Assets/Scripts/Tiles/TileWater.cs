using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileWater : TileLiquidBase {

    void Start()
    {
        TileType = TileTypes.Water;
    }

    // Use this for initialization
    public override void Initialize()
    {
        base.Initialize();
    }

    // Update is called once per frame
}
