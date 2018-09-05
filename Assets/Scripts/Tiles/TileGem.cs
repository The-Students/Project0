using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGem : TileWallBase {

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        TileType = TileTypes.Gem;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
