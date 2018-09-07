using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDirt : TileWallBase {

    // Use this for initialization
    public override void Initialize()
    {
        base.Initialize();
        TileType = TileTypes.Dirt;
    }

    // Update is called once per frame
    void Update ()
    {
		
	}
}
