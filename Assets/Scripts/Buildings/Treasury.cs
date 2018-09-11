using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasury : TileBuilding
{
    public int Capacity;

	// Use this for initialization
	public override void Initialize()
    {
        base.Initialize();
        _buildingType = BuildingType.TREASURY;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
