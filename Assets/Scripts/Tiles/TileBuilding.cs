﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuildingType
{
    CORE,
    TREASURY,
    LAIR,
    WORKSHOP,
    LAB,
}

public class TileBuilding : TileBase {

    public int _OwnerID { get; set; }

    protected BuildingType _buildingType;

    void Start()
    {
        TileType = TileTypes.Building;
    }

    // Use this for initialization
    public override void Initialize()
    {
        base.Initialize();
    }

    // Update is called once per frame
    void Update ()
    {
		
	}

    void SwitchOwner()
    {
        switch (_OwnerID)
        {
            case 0:
                _OwnerID = 1;
                break;
            case 1:
                _OwnerID = 0;
                break;
            default:
                Debug.Log("wrong owner id Error");
                break;
        }
    }

    public BuildingType GetBuildingType()
    {
        return _buildingType;
    }

    
}
