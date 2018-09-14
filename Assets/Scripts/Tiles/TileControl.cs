using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class TileControl : NetworkBehaviour {

    [SerializeField]
    private TileManager _tileManager;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {

        
        for (int y = _tileManager.GetMapVar() - 1; y >= 0; --y)
        {
            for (int x = 0; x < _tileManager.GetMapVar(); ++x)
            {
                if (_tileManager.TileScripts[x, y])
                {
                    if (_tileManager.TileScripts[x, y].GetIsDestroyed())
                    {
                        TileBase[] Adjacent = _tileManager.TileScripts[x, y].GetAdjacent();

                        //TO-DO: MIGHT NOT WORK CHECK LATER
                        var name = _tileManager.Tiles[x, y].name;
                        var pos = _tileManager.Tiles[x, y].transform.position;
                        var rot = _tileManager.Tiles[x, y].transform.rotation;
                        var scale = _tileManager.Tiles[x, y].transform.localScale;
                        var parent = _tileManager.Tiles[x, y].transform.parent;

                        Destroy(_tileManager.Tiles[x, y]);

                        _tileManager.Tiles[x, y] = Instantiate(_tileManager.TileScripts[x, y].BreaksInto);
                        _tileManager.Tiles[x, y].transform.parent = parent;
                        _tileManager.Tiles[x, y].name = name;
                        _tileManager.Tiles[x, y].transform.position = pos;
                        _tileManager.Tiles[x, y].transform.rotation = rot;
                        _tileManager.Tiles[x, y].transform.localScale = scale;
                        
                        _tileManager.TileScripts[x, y] = _tileManager.Tiles[x, y].GetComponent<TileBase>();
                        _tileManager.TileScripts[x, y].Initialize();

                        if (Adjacent[(int)Direction.North])
                        {
                            _tileManager.TileScripts[x, y].SetAdjacent(Direction.North, Adjacent[(int)Direction.North]);
                            Adjacent[(int)Direction.North].SetAdjacent(Direction.South, _tileManager.TileScripts[x, y]);
                        }
                        if (Adjacent[(int)Direction.East])
                        {
                            _tileManager.TileScripts[x, y].SetAdjacent(Direction.East, Adjacent[(int)Direction.East]);
                            Adjacent[(int)Direction.East].SetAdjacent(Direction.West, _tileManager.TileScripts[x, y]);
                        }
                        if (Adjacent[(int)Direction.South])
                        {
                            _tileManager.TileScripts[x, y].SetAdjacent(Direction.South, Adjacent[(int)Direction.South]);
                            Adjacent[(int)Direction.South].SetAdjacent(Direction.North, _tileManager.TileScripts[x, y]);
                        }
                        if (Adjacent[(int)Direction.West])
                        {
                            _tileManager.TileScripts[x, y].SetAdjacent(Direction.West, Adjacent[(int)Direction.West]);
                            Adjacent[(int)Direction.West].SetAdjacent(Direction.East, _tileManager.TileScripts[x, y]);
                        }
                        if (Adjacent[(int)Direction.NorthWest])
                        {
                            _tileManager.TileScripts[x, y].SetAdjacent(Direction.NorthWest, Adjacent[(int)Direction.NorthWest]);
                            Adjacent[(int)Direction.NorthWest].SetAdjacent(Direction.SouthEast, _tileManager.TileScripts[x, y]);
                        }
                        if (Adjacent[(int)Direction.NorthEast])
                        {
                            _tileManager.TileScripts[x, y].SetAdjacent(Direction.NorthEast, Adjacent[(int)Direction.NorthEast]);
                            Adjacent[(int)Direction.NorthEast].SetAdjacent(Direction.SouthWest, _tileManager.TileScripts[x, y]);
                        }
                        if (Adjacent[(int)Direction.SouthEast])
                        {
                            _tileManager.TileScripts[x, y].SetAdjacent(Direction.SouthEast, Adjacent[(int)Direction.SouthEast]);
                            Adjacent[(int)Direction.SouthEast].SetAdjacent(Direction.NorthWest, _tileManager.TileScripts[x, y]);
                        }
                        if (Adjacent[(int)Direction.SouthWest])
                        {
                            _tileManager.TileScripts[x, y].SetAdjacent(Direction.SouthWest, Adjacent[(int)Direction.SouthWest]);
                            Adjacent[(int)Direction.SouthWest].SetAdjacent(Direction.NorthEast, _tileManager.TileScripts[x, y]);
                        }

                        _tileManager.TileScripts[x, y].UpdateMesh();
                        _tileManager.TileScripts[x, y].UpdateAdjacent();
                    }
                }
            }
        }
    }
}
