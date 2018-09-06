using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileWater : TileLiquidBase {

    // Use this for initialization
    public override void Initialize()
    {
        base.Initialize();
        TileType = TileTypes.Water;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
