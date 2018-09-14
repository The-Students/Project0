using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileFloor : TileFlatBase {

    void Start()
    {
        TileType = TileTypes.Path;
    }

    // Use this for initialization
    public override void Initialize()
    {
        base.Initialize();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
