using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class GameBase : NetworkBehaviour {
    public TileManager tileManager;

	// Use this for initialization
	void Start ()
    {
        tileManager.Initialize(10, "Assets/Maps/Map.bin");
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
