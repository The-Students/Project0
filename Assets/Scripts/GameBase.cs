﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBase : MonoBehaviour {
    public TileManager tileManager;

	// Use this for initialization
	void Start ()
    {
        tileManager.Initialize(10);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
