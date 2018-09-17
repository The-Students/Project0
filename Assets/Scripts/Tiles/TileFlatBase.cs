using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileFlatBase : TileBase {
    // Use this for initialization
    public override void Initialize()
    {
        base.Initialize();
        IsFlat = true;
	}
	
	// Update is called once per frame
}
