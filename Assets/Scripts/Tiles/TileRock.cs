using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRock : TileWallBase {

    // Use this for initialization
    public override void Initialize()
    {
        base.Initialize();
        TileType = TileTypes.Rock;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
