using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {

    private GameObject[,] Tiles;
    private TileBase[,] TileScripts;
    private uint MapSize;
    private string MapText;
    public TextAsset MapFile;
    public GameObject PrefabDirt;
    public GameObject PrefabRock;
    public GameObject PrefabGem;
    public GameObject PrefabWater;
    public GameObject PrefabLava;
    public GameObject PrefabPath;
    public GameObject PrefabRoom;

    // Use this for initialization
    void Start ()
    {
		
	}

    public void Initialize(uint tilesize)
    {
        MapText = MapFile.text;
        int index;
        MapSize = uint.Parse(MapText.Substring(0, index = MapText.IndexOf('.')));
        MapText = MapText.Substring(index + 1);

        //MapSize = mapsize;
        Tiles = new GameObject[MapSize, MapSize];
        TileScripts = new TileBase[MapSize, MapSize];

        //CREATE TILES
        for (int y = (int)MapSize - 1; y >= 0; --y)
        {
            for (int x = 0; x < MapSize; ++x)
            {
                TileTypes type;
                type = (TileTypes)int.Parse(MapText.Substring(0, index = MapText.IndexOf('.')));
                MapText = MapText.Substring(index + 1);
                switch (type)
                {
                    case TileTypes.Dirt:
                        Tiles[x, y] = Instantiate(PrefabDirt);
                        TileScripts[x, y] = Tiles[x, y].GetComponent<TileDirt>();
                        break;
                    case TileTypes.Rock:
                        Tiles[x, y] = Instantiate(PrefabRock);
                        TileScripts[x, y] = Tiles[x, y].GetComponent<TileRock>();
                        break;
                    case TileTypes.Gem:
                        Tiles[x, y] = Instantiate(PrefabGem);
                        TileScripts[x, y] = Tiles[x, y].GetComponent<TileGem>();
                        break;
                    case TileTypes.Path:
                        Tiles[x, y] = Instantiate(PrefabPath);
                        TileScripts[x, y] = Tiles[x, y].GetComponent<TileFloor>();
                        break;
                    case TileTypes.Lava:
                        Tiles[x, y] = Instantiate(PrefabLava);
                        TileScripts[x, y] = Tiles[x, y].GetComponent<TileLava>();
                        break;
                    case TileTypes.Water:
                        Tiles[x, y] = Instantiate(PrefabWater);
                        TileScripts[x, y] = Tiles[x, y].GetComponent<TileWater>();
                        break;
                    case TileTypes.Building:
                        Tiles[x, y] = Instantiate(PrefabRoom);
                        TileScripts[x, y] = Tiles[x, y].GetComponent<TileBuilding>();
                        break;
                    default:
                        Tiles[x, y] = Instantiate(PrefabDirt);
                        TileScripts[x, y] = Tiles[x, y].GetComponent<TileDirt>();
                        break;
                }
                Tiles[x, y].name = "TileX" + x + "Y" + y;
                Tiles[x, y].transform.Translate((x - (MapSize / 2.0f)) * tilesize, 0.0f, (y - (MapSize / 2.0f)) * tilesize);
                Tiles[x, y].transform.localScale = new Vector3(tilesize, tilesize, tilesize);
                Tiles[x, y].transform.parent = transform;
            }
        }
    }

    // Update is called once per frame
    void Update ()
    {
    }
}
