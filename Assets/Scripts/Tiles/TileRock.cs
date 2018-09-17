using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRock : TileWallBase {

    void Start()
    {
        TileType = TileTypes.Rock;
    }
    // Use this for initialization
    public override void Initialize()
    {
        base.Initialize();
    }
	
	// Update is called once per frame
}
