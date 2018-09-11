using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : TileBuilding
{

	// Use this for initialization
	public override void Initialize ()
    {
        base.Initialize();
        _buildingType = BuildingType.CORE;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
