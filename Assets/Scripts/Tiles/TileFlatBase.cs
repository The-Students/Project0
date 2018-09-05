using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileFlatBase : TileBase {

	// Use this for initialization
	protected override void Start ()
    {
        base.Start();
        IsFlat = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
