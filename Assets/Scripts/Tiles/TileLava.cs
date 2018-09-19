using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileLava : TileLiquidBase {

    void Start()
    {
        TileType = TileTypes.Lava;
    }
    // Use this for initialization
    public override void Initialize()
    {
        base.Initialize();
    }

    // Update is called once per frame
}
