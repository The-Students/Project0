using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileWallBase : TileBase {

    // Use this for initialization
    public override void Initialize()
    {
        base.Initialize();
        IsFlat = false;
	}
	
	// Update is called once per frame
}
