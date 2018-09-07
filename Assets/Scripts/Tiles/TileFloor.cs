using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileFloor : TileFlatBase {

    // Use this for initialization
    public override void Initialize()
    {
        base.Initialize();
        TileType = TileTypes.Path;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
