using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBase : MonoBehaviour {
    protected float HP = 1.0f;
    protected TileTypes TileType = TileTypes.Empty;
    protected bool IsDestroyed = false;
    protected TileBase[] AdjacentTiles = new TileBase[8];
    protected bool IsFlat = false;
    protected Dictionary<Direction, bool> OpenSides = new Dictionary<Direction, bool>();
    protected TileTypes BreaksIntoType = TileTypes.Empty;

    public Mesh Mesh0Walls;
    public Mesh Mesh1Wall;
    public Mesh Mesh2WallsCorner;
    public Mesh Mesh2WallsOpposing;
    public Mesh Mesh3Walls;
    public Mesh Mesh4Walls;
    public GameObject BreaksInto;


    // Use this for initialization
    void Start ()
    {
    }
    public virtual void Initialize()
    {
        TileType = TileTypes.Empty;
	}

	// Update is called once per frame
	void Update ()
    {
		
	}

    public void UpdateMesh()
    {
        
        bool n, e, s, w;
        int NrOfOpenSides = 0;
        int NrOfDirectlyOpenSides = 0;

        OpenSides.TryGetValue(Direction.North, out n);
        OpenSides.TryGetValue(Direction.East, out e);
        OpenSides.TryGetValue(Direction.South, out s);
        OpenSides.TryGetValue(Direction.West, out w);

        if (n) NrOfDirectlyOpenSides++;
        if (e) NrOfDirectlyOpenSides++;
        if (s) NrOfDirectlyOpenSides++;
        if (w) NrOfDirectlyOpenSides++;

        foreach (KeyValuePair<Direction, bool> Side in OpenSides)
        {
            if (Side.Value) NrOfOpenSides++;
        }

        if (NrOfOpenSides == 0) //NOTHING
        {
            gameObject.GetComponent<MeshFilter>().mesh = Mesh0Walls;
            gameObject.transform.rotation = Quaternion.Euler(0, Random.Range(0, 4) * 90, 0);
        }
        else if (NrOfDirectlyOpenSides == 1) //WALL
        {
            if (NrOfDirectlyOpenSides == 1 && NrOfOpenSides - NrOfDirectlyOpenSides < 3)
            {
                gameObject.GetComponent<MeshFilter>().mesh = Mesh1Wall;
                if (n) gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
                else if (e) gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
                else if (s) gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                else if (w) gameObject.transform.rotation = Quaternion.Euler(0, 270, 0);
            }
            else
            {
                bool ne, se, sw, nw;

                OpenSides.TryGetValue(Direction.NorthEast, out ne);
                OpenSides.TryGetValue(Direction.SouthEast, out se);
                OpenSides.TryGetValue(Direction.SouthWest, out sw);
                OpenSides.TryGetValue(Direction.NorthWest, out nw);

                if (n && !se && !sw)
                {
                    gameObject.GetComponent<MeshFilter>().mesh = Mesh1Wall;
                    gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                else if (e && !sw && !nw)
                {
                    gameObject.GetComponent<MeshFilter>().mesh = Mesh1Wall;
                    gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
                }
                else if (s && !ne && !nw)
                {
                    gameObject.GetComponent<MeshFilter>().mesh = Mesh1Wall;
                    gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else if (w && !se && !ne)
                {
                    gameObject.GetComponent<MeshFilter>().mesh = Mesh1Wall;
                    gameObject.transform.rotation = Quaternion.Euler(0, 270, 0);
                }
                else //OTHER STUFF (With one open side)
                {
                    gameObject.GetComponent<MeshFilter>().mesh = Mesh4Walls;
                    gameObject.transform.rotation = Quaternion.Euler(0, Random.Range(0, 4) * 90, 0);
                }
            }
        }
        else if (n == s && e == w && n != e)  //DOUBLE WALL
        {
            if (n == true)
            {
                gameObject.GetComponent<MeshFilter>().mesh = Mesh2WallsOpposing;
                gameObject.transform.rotation = Quaternion.Euler(0, Random.Range(0, 2) * 180, 0);
            }
            else
            {
                gameObject.GetComponent<MeshFilter>().mesh = Mesh2WallsOpposing;
                gameObject.transform.rotation = Quaternion.Euler(0, Random.Range(0, 2) * 180 + 90, 0);
            }
        }
        else //OTHER STUFF
        {
            gameObject.GetComponent<MeshFilter>().mesh = Mesh4Walls;
            gameObject.transform.rotation = Quaternion.Euler(0, Random.Range(0, 4) * 90, 0);
        }

    }

    public void UpdateAdjacent()
    {
        foreach (TileBase Tile in AdjacentTiles)
        {
            Tile.UpdateMesh();
        }
    }

    public TileBase[] GetAdjacent()
    {
        return AdjacentTiles;
    }
    
    public void TakeDamage(float damage)
    {
        HP -= damage;
        IsDestroyed = HP <= 0.0f;
    }

    public bool GetIsDestroyed()
    {
        return IsDestroyed;
    }

    public float GetHP()
    {
        return HP;
    }

    public TileTypes GetTileType()
    {
        return TileType;
    }

    public bool GetIsFlat()
    {
        return IsFlat;
    }

    public TileTypes GetBreaksIntoTileType()
    {
        return BreaksIntoType;
    }

    public void SetAdjacent(Direction dir, TileBase tile)
    {
        AdjacentTiles[(int)dir] = tile;

        bool val;
        if (OpenSides.TryGetValue(dir, out val)) OpenSides[dir] = AdjacentTiles[(int)dir].IsFlat;
        else OpenSides.Add(dir, AdjacentTiles[(int)dir].IsFlat);
    }
}
