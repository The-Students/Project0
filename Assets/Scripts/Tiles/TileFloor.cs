using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileFloor : TileFlatBase {

	// Use this for initialization
	protected override void Start ()
    {
        base.Start();
        TileType = TileTypes.Path;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
