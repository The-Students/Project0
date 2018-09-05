using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileLava : TileLiquidBase {

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        TileType = TileTypes.Lava;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
