using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : TileBuilding
{

	// Use this for initialization
	protected override void Start ()
    {
        base.Start();
        _buildingType = BuildingType.CORE;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
