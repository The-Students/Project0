using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {

    private GameObject[,] Tiles;
    private uint MapSize;
    private string MapText;
    public Mesh DefaultMesh;
    //public Mesh DirtMesh;
    //public Mesh RockMesh;
    //public Mesh GemMesh;
    //public Mesh WaterMesh;
    //public Mesh LavaMesh;
    //public Mesh BuildingMesh;
    //public Mesh PathMesh;
    public Material DefaultMat;
    public Material DirtMat;
    public Material RockMat;
    public Material GemMat;
    public Material WaterMat;
    public Material LavaMat;
    public Material BuildingMat;
    public Material PathMat;
    public TextAsset MapFile;

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
        for (int y = (int)MapSize - 1; y >= 0; --y)
        {
            for (int x = 0; x < MapSize; ++x)
            {
                TileTypes type;
                Tiles[x, y] = new GameObject();
                Tiles[x, y].name = "TileX" + x + "Y" + y;
                Tiles[x, y].AddComponent<TileBase>();
                Tiles[x, y].GetComponent<TileBase>().Initialize(type = (TileTypes)int.Parse(MapText.Substring(0, index = MapText.IndexOf('.'))));
                MapText = MapText.Substring(index + 1);

                //TO-DO Change With Meshes
                switch (type)
                {
                    case TileTypes.Dirt:
                    case TileTypes.Rock:
                    case TileTypes.Gem:
                        Tiles[x, y].transform.Translate((x - (MapSize / 2.0f)) * tilesize, tilesize / 2.0f, (y - (MapSize / 2.0f)) * tilesize);
                        Tiles[x, y].transform.localScale = new Vector3(tilesize, tilesize, tilesize);
                        break;
                    case TileTypes.Path:
                    case TileTypes.Lava:
                    case TileTypes.Water:
                        Tiles[x, y].transform.Translate((x - (MapSize / 2.0f)) * tilesize, tilesize / 2.0f - tilesize / 2.0f, (y - (MapSize / 2.0f)) * tilesize);
                        Tiles[x, y].transform.localScale = new Vector3(tilesize, tilesize / 10, tilesize);
                        break;
                    case TileTypes.Building:
                        Tiles[x, y].transform.Translate((x - (MapSize / 2.0f)) * tilesize, tilesize / 2.0f, (y - (MapSize / 2.0f)) * tilesize);
                        Tiles[x, y].transform.localScale = new Vector3(tilesize, tilesize, tilesize);
                        break;
                    default:
                        Tiles[x, y].transform.Translate((x - (MapSize / 2.0f)) * tilesize, tilesize / 2.0f, (y - (MapSize / 2.0f)) * tilesize);
                        Tiles[x, y].transform.localScale = new Vector3(tilesize, tilesize, tilesize);
                        break;
                }

                Tiles[x, y].transform.parent = transform;
                Tiles[x, y].AddComponent<MeshFilter>();
                Tiles[x, y].GetComponent<MeshFilter>().mesh = DefaultMesh;
                Tiles[x, y].AddComponent<MeshRenderer>();

                switch (type)
                {
                    case TileTypes.Dirt:
                        Tiles[x, y].GetComponent<MeshRenderer>().material = DirtMat;
                        break;
                    case TileTypes.Rock:
                        Tiles[x, y].GetComponent<MeshRenderer>().material = RockMat;
                        break;
                    case TileTypes.Gem:
                        Tiles[x, y].GetComponent<MeshRenderer>().material = GemMat;
                        break;
                    case TileTypes.Path:
                        Tiles[x, y].GetComponent<MeshRenderer>().material = PathMat;
                        break;
                    case TileTypes.Lava:
                        Tiles[x, y].GetComponent<MeshRenderer>().material = LavaMat;
                        break;
                    case TileTypes.Water:
                        Tiles[x, y].GetComponent<MeshRenderer>().material = WaterMat;
                        break;
                    case TileTypes.Building:
                        Tiles[x, y].GetComponent<MeshRenderer>().material = BuildingMat;
                        break;
                    default:
                        Tiles[x, y].GetComponent<MeshRenderer>().material = DefaultMat;
                        break;
                }

                Tiles[x, y].AddComponent<BoxCollider>();
                //Tiles[x, y].GetComponent<BoxCollider>().size = new Vector3(tilesize, tilesize, tilesize);
            }
        }
    }

    // Update is called once per frame
    void Update ()
    {
		
	}
}
