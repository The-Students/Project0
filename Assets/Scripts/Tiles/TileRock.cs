using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRock : TileWallBase {

    // Use this for initialization
    protected override void Start ()
    {
        base.Start();
        TileType = TileTypes.Rock;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
