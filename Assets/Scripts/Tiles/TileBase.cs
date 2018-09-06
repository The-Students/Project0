using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBase : MonoBehaviour {
    protected float HP = 1.0f;
    protected TileTypes TileType = TileTypes.Empty;
    protected bool IsDestroyed = false;
    protected TileBase[] AdjacentTiles = new TileBase[4];
    protected bool IsFlat = false;
    protected Dictionary<Direction, bool> OpenSides = new Dictionary<Direction, bool>();

    public Mesh Mesh0Walls;
    public Mesh Mesh1Wall;
    public Mesh Mesh2WallsCorner;
    public Mesh Mesh2WallsOpposing;
    public Mesh Mesh3Walls;
    public Mesh Mesh4Walls;


    // Use this for initialization
    protected virtual void Start ()
    {
        TileType = TileTypes.Empty;
	}

	// Update is called once per frame
	void Update ()
    {
		
	}

    public void UpdateMesh()
    {
        int NrOfOpenSides = 0;
        foreach (KeyValuePair<Direction, bool> Side in OpenSides)
        {
            if (Side.Value) NrOfOpenSides++;
        }
        switch (NrOfOpenSides)
        {
            case 0:
                gameObject.GetComponent<MeshFilter>().mesh = Mesh4Walls;
                gameObject.transform.rotation = Quaternion.Euler(0, Random.Range(0, 4) * 90, 0);
                break;
            case 1:
                gameObject.GetComponent<MeshFilter>().mesh = Mesh3Walls;
                foreach (KeyValuePair<Direction, bool> Side in OpenSides)
                {
                    if (Side.Value) //Find empty side
                    {
                        switch (Side.Key)
                        {
                            case Direction.North:
                                gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                                break;
                            case Direction.East:
                                gameObject.transform.rotation = Quaternion.Euler(0, 270, 0);
                                break;
                            case Direction.South:
                                gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
                                break;
                            case Direction.West:
                                gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
                                break;
                        }
                        break;
                    }
                }
                break;
            case 2:
                bool n = false, s = false;
                OpenSides.TryGetValue(Direction.North, out n);
                OpenSides.TryGetValue(Direction.South, out s);
                if (n == s)
                {
                    gameObject.GetComponent<MeshFilter>().mesh = Mesh2WallsOpposing;
                    foreach (KeyValuePair<Direction, bool> Side in OpenSides)
                    {
                        if (!Side.Value) //Find side with wall
                        {
                            switch (Side.Key)
                            {
                                case Direction.North:
                                case Direction.South:
                                    gameObject.transform.rotation = Quaternion.Euler(0, Random.Range(0,2) * 180, 0);
                                    break;
                                case Direction.East:
                                case Direction.West:
                                    gameObject.transform.rotation = Quaternion.Euler(0, Random.Range(0, 2) * 180 + 90, 0);
                                    break;
                            }
                            break;
                        }
                    }
                }
                else
                {
                    gameObject.GetComponent<MeshFilter>().mesh = Mesh2WallsCorner;
                    foreach (KeyValuePair<Direction, bool> Side in OpenSides)
                    {
                        if (!Side.Value) //Find side with wall
                        {
                            switch (Side.Key)
                            {
                                case Direction.East:
                                    if (n) gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
                                    else gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                                    break;
                                case Direction.West:
                                    if (n) gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
                                    else gameObject.transform.rotation = Quaternion.Euler(0, 270, 0);
                                    break;
                            }
                            break;
                        }
                    }
                }
                break;
            case 3:
                gameObject.GetComponent<MeshFilter>().mesh = Mesh1Wall;
                foreach (KeyValuePair<Direction, bool> Side in OpenSides)
                {
                    if (!Side.Value) //Find side with wall
                    {
                        switch (Side.Key)
                        {
                            case Direction.North:
                                gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
                                break;
                            case Direction.East:
                                gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
                                break;
                            case Direction.South:
                                gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                                break;
                            case Direction.West:
                                gameObject.transform.rotation = Quaternion.Euler(0, 270, 0);
                                break;
                        }
                        break;
                    }
                }
                break;
            case 4:
                gameObject.GetComponent<MeshFilter>().mesh = Mesh0Walls;
                gameObject.transform.rotation = Quaternion.Euler(0, Random.Range(0, 4) * 90, 0);
                break;
            default:
                //EXPLOSION *SHRRRRRIIIIIIIIIIIIII BOOOOOOOOM*
                break;
        }

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


    public void SetAdjacent(Direction dir, TileBase tile)
    {
        AdjacentTiles[(int)dir] = tile;

        bool val;
        if (OpenSides.TryGetValue(dir, out val)) OpenSides[dir] = AdjacentTiles[(int)dir].IsFlat;
        else OpenSides.Add(dir, AdjacentTiles[(int)dir].IsFlat);
    }
}
