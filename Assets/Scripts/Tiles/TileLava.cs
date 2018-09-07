using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileLava : TileLiquidBase {

    // Use this for initialization
    public override void Initialize()
    {
        base.Initialize();
        TileType = TileTypes.Lava;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
