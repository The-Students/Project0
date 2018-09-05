using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDirt : TileWallBase {

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        TileType = TileTypes.Dirt;
    }

    // Update is called once per frame
    void Update ()
    {
		
	}
}
