using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileWallBase : TileBase {

    // Use this for initialization
    protected override void Start ()
    {
        base.Start();
        IsFlat = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
