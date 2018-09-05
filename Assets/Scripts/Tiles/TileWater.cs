using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileWater : TileLiquidBase {

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        TileType = TileTypes.Water;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
