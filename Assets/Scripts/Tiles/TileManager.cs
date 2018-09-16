using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.Networking;
using UnityEngine;
using System.Text;

public class TileManager : NetworkBehaviour
{
    public GameObject[,] Tiles;

    public TileBase[,] TileScripts;

    [SerializeField]
    private int MapSize;
    [SerializeField]
    private float TileSize;

    private BinaryReader FileReader;
    private FileStream File;

    [SerializeField]
    protected NetworkCommands _networkCommand;


    public GameObject PrefabDirt;
    public GameObject PrefabRock;
    public GameObject PrefabGem;
    public GameObject PrefabWater;
    public GameObject PrefabLava;
    public GameObject PrefabPath;
    public GameObject PrefabRoom;
    public TextAsset FileAsset;


    // Use this for initialization
    void Start ()
    {
        
	}


    public void Initialize()
    {
        if (!isServer)
        {
            return;
        }
        MemoryStream mStrm = new MemoryStream(Encoding.UTF8.GetBytes(FileAsset.text));
        FileReader = new BinaryReader(mStrm);

        //MapSize = FileReader.ReadInt16();

        Tiles = new GameObject[MapSize, MapSize];
        TileScripts = new TileBase[MapSize, MapSize];

        //CREATE TILES
        for (int y = MapSize - 1; y >= 0; --y)
        {
            for (int x = 0; x < MapSize; ++x)
            {
                TileTypes type = (TileTypes)FileReader.ReadInt16();
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
                NetworkServer.Spawn(Tiles[x, y]);
                //if (!isServer)
                //{
                //    _networkCommand.CmdNetworkSpawn(Tiles[x, y]);
                //}
                //else
                //{
                //    NetworkServer.Spawn(Tiles[x, y]);
                //}
                TileScripts[x, y].Initialize();
                Tiles[x, y].name = "TileX" + x + "Y" + y;
                Tiles[x, y].transform.Translate((x - (MapSize / 2.0f)) * TileSize, 0.0f, (y - (MapSize / 2.0f)) * TileSize);
                Tiles[x, y].transform.localScale = new Vector3(TileSize, TileSize, TileSize);
                //Tiles[x, y].transform.parent = GameObject.FindGameObjectWithTag("TileManager").transform;
            }
        }

        FileReader.Close();

        //Give Tiles The Adjacent
        for (int y = MapSize - 1; y >= 0; --y)
        {
            for (int x = 0; x < MapSize; ++x)
            {
                if (y < MapSize - 1) TileScripts[x, y].SetAdjacent(Direction.North, TileScripts[x, y + 1]);
                if (y > 0) TileScripts[x, y].SetAdjacent(Direction.South, TileScripts[x, y - 1]);
                if (x < MapSize - 1) TileScripts[x, y].SetAdjacent(Direction.East, TileScripts[x + 1, y]);
                if (x > 0) TileScripts[x, y].SetAdjacent(Direction.West, TileScripts[x - 1, y]);

                if (x > 0 && y < MapSize - 1) TileScripts[x, y].SetAdjacent(Direction.NorthWest, TileScripts[x - 1, y + 1]);
                if (x > 0 && y > 0) TileScripts[x, y].SetAdjacent(Direction.SouthWest, TileScripts[x - 1, y - 1]);
                if (x < MapSize - 1 && y < MapSize - 1) TileScripts[x, y].SetAdjacent(Direction.NorthEast, TileScripts[x + 1, y + 1]);
                if (x < MapSize - 1 && y > 0) TileScripts[x, y].SetAdjacent(Direction.SouthEast, TileScripts[x + 1, y - 1]);
            }
        }

        //Update Meshes
        foreach (TileBase Tile in TileScripts)
        {
            Tile.UpdateMesh();
        }
    }

    void Update()
    {
        for (int y = MapSize - 1; y >= 0; --y)
        {
            for (int x = 0; x < MapSize; ++x)
            {
                if (Tiles == null)
                {
                    Debug.Log(x + ", " + y);
                    return;
                }
                if (Tiles[x, y] != null)
                {
                    if (TileScripts[x, y].GetIsDestroyed())
                    {
                        TileBase[] Adjacent = TileScripts[x, y].GetAdjacent();

                        //TO-DO: MIGHT NOT WORK CHECK LATER
                        var name = Tiles[x, y].name;
                        var pos = Tiles[x, y].transform.position;
                        var rot = Tiles[x, y].transform.rotation;
                        var scale = Tiles[x, y].transform.localScale;
                        var parent = Tiles[x, y].transform.parent;

                        if (!isServer)
                        {
                            _networkCommand.CmdNetworkDestroy(Tiles[x, y]);
                        }
                        else
                        {
                            NetworkServer.Destroy(Tiles[x, y]);
                        }

                        Tiles[x, y] = Instantiate(TileScripts[x, y].BreaksInto);
                        Tiles[x, y].transform.parent = parent;
                        Tiles[x, y].name = name;
                        Tiles[x, y].transform.position = pos;
                        Tiles[x, y].transform.rotation = rot;
                        Tiles[x, y].transform.localScale = scale;

                        TileScripts[x, y] = Tiles[x, y].GetComponent<TileBase>();
                        TileScripts[x, y].Initialize();

                        if (Adjacent[(int)Direction.North])
                        {
                            TileScripts[x, y].SetAdjacent(Direction.North, Adjacent[(int)Direction.North]);
                            Adjacent[(int)Direction.North].SetAdjacent(Direction.South, TileScripts[x, y]);
                        }
                        if (Adjacent[(int)Direction.East])
                        {
                            TileScripts[x, y].SetAdjacent(Direction.East, Adjacent[(int)Direction.East]);
                            Adjacent[(int)Direction.East].SetAdjacent(Direction.West, TileScripts[x, y]);
                        }
                        if (Adjacent[(int)Direction.South])
                        {
                            TileScripts[x, y].SetAdjacent(Direction.South, Adjacent[(int)Direction.South]);
                            Adjacent[(int)Direction.South].SetAdjacent(Direction.North, TileScripts[x, y]);
                        }
                        if (Adjacent[(int)Direction.West])
                        {
                            TileScripts[x, y].SetAdjacent(Direction.West, Adjacent[(int)Direction.West]);
                            Adjacent[(int)Direction.West].SetAdjacent(Direction.East, TileScripts[x, y]);
                        }
                        if (Adjacent[(int)Direction.NorthWest])
                        {
                            TileScripts[x, y].SetAdjacent(Direction.NorthWest, Adjacent[(int)Direction.NorthWest]);
                            Adjacent[(int)Direction.NorthWest].SetAdjacent(Direction.SouthEast, TileScripts[x, y]);
                        }
                        if (Adjacent[(int)Direction.NorthEast])
                        {
                            TileScripts[x, y].SetAdjacent(Direction.NorthEast, Adjacent[(int)Direction.NorthEast]);
                            Adjacent[(int)Direction.NorthEast].SetAdjacent(Direction.SouthWest, TileScripts[x, y]);
                        }
                        if (Adjacent[(int)Direction.SouthEast])
                        {
                            TileScripts[x, y].SetAdjacent(Direction.SouthEast, Adjacent[(int)Direction.SouthEast]);
                            Adjacent[(int)Direction.SouthEast].SetAdjacent(Direction.NorthWest, TileScripts[x, y]);
                        }
                        if (Adjacent[(int)Direction.SouthWest])
                        {
                            TileScripts[x, y].SetAdjacent(Direction.SouthWest, Adjacent[(int)Direction.SouthWest]);
                            Adjacent[(int)Direction.SouthWest].SetAdjacent(Direction.NorthEast, TileScripts[x, y]);
                        }

                        TileScripts[x, y].UpdateMesh();
                        TileScripts[x, y].UpdateAdjacent();
                    }
                }
            }
        }
    }

    public float GetMapSize()
    {
        return MapSize * TileSize;
    }

    public int GetMapVar()
    {
        return MapSize;
    }
}
