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
                        var temp = _tileManager.Tiles[x, y];
                        _tileManager.Tiles[x, y] = Instantiate(_tileManager.TileScripts[x, y].BreaksInto);

                        switch (_tileManager.TileScripts[x, y].GetBreaksIntoTileType())
                        {
                            case TileTypes.Empty:
                                _tileManager.TileScripts[x, y] = _tileManager.Tiles[x, y].GetComponent<TileDirt>();
                                break;
                            case TileTypes.Dirt:
                                _tileManager.TileScripts[x, y] = _tileManager.Tiles[x, y].GetComponent<TileDirt>();
                                break;
                            case TileTypes.Rock:
                                _tileManager.TileScripts[x, y] = _tileManager.Tiles[x, y].GetComponent<TileRock>();
                                break;
                            case TileTypes.Gem:
                                _tileManager.TileScripts[x, y] = _tileManager.Tiles[x, y].GetComponent<TileGem>();
                                break;
                            case TileTypes.Path:
                                _tileManager.TileScripts[x, y] = _tileManager.Tiles[x, y].GetComponent<TileFloor>();
                                break;
                            case TileTypes.Lava:
                                _tileManager.TileScripts[x, y] = _tileManager.Tiles[x, y].GetComponent<TileLava>();
                                break;
                            case TileTypes.Water:
                                _tileManager.TileScripts[x, y] = _tileManager.Tiles[x, y].GetComponent<TileWater>();
                                break;
                            case TileTypes.Building:
                                _tileManager.TileScripts[x, y] = _tileManager.Tiles[x, y].GetComponent<TileBuilding>();
                                break;
                        }

                        //Check later
                        Destroy(temp);
                        
                        _tileManager.TileScripts[x, y].Initialize();

                        if (Adjacent[(int)Direction.North]) _tileManager.TileScripts[x, y].SetAdjacent(Direction.North, Adjacent[(int)Direction.North]);
                        if (Adjacent[(int)Direction.East]) _tileManager.TileScripts[x, y].SetAdjacent(Direction.East, Adjacent[(int)Direction.East]);
                        if (Adjacent[(int)Direction.South]) _tileManager.TileScripts[x, y].SetAdjacent(Direction.South, Adjacent[(int)Direction.South]);
                        if (Adjacent[(int)Direction.West]) _tileManager.TileScripts[x, y].SetAdjacent(Direction.West, Adjacent[(int)Direction.West]);
                        if (Adjacent[(int)Direction.NorthWest]) _tileManager.TileScripts[x, y].SetAdjacent(Direction.NorthWest, Adjacent[(int)Direction.NorthWest]);
                        if (Adjacent[(int)Direction.NorthEast]) _tileManager.TileScripts[x, y].SetAdjacent(Direction.NorthEast, Adjacent[(int)Direction.NorthEast]);
                        if (Adjacent[(int)Direction.SouthEast]) _tileManager.TileScripts[x, y].SetAdjacent(Direction.SouthEast, Adjacent[(int)Direction.SouthEast]);
                        if (Adjacent[(int)Direction.SouthWest]) _tileManager.TileScripts[x, y].SetAdjacent(Direction.SouthWest, Adjacent[(int)Direction.SouthWest]);
                    }
                }
            }
        }
    }
}
