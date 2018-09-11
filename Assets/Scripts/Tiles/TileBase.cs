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


    public GameObject PrefabWall;
    public GameObject PrefabCorner;
    public GameObject PrefabInnerCorner;
    public GameObject PrefabTop;
    
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

    public virtual void UpdateMesh()
    {
        //DELETE OLD MESH
        foreach (Transform ChildTransform in GetComponentsInChildren<Transform>())
        {
            Destroy(ChildTransform.gameObject);
        }
        
        //ADD TOP
        Instantiate(PrefabTop).transform.parent = transform;
        
        foreach (KeyValuePair<Direction, bool> Side in OpenSides)
        {
            switch (Side.Key)
            {
                //ADD WALLS
                case Direction.North:
                    GameObject wallN = Instantiate(PrefabWall);
                    wallN.transform.parent = transform;
                    wallN.transform.Rotate(0, 180, 0);
                    break;
                case Direction.East:
                    GameObject wallE = Instantiate(PrefabWall);
                    wallE.transform.parent = transform;
                    wallE.transform.Rotate(0, 90, 0);
                    break;
                case Direction.South:
                    GameObject wallS = Instantiate(PrefabWall);
                    wallS.transform.parent = transform;
                    wallS.transform.Rotate(0, 0, 0);
                    break;
                case Direction.West:
                    GameObject wallW = Instantiate(PrefabWall);
                    wallW.transform.parent = transform;
                    wallW.transform.Rotate(0, 270, 0);
                    break;
                //ADD CORNERS
                case Direction.NorthWest:
                    //Normal
                    if (OpenSides[Direction.North] && OpenSides[Direction.West])
                    {
                        GameObject cornNW = Instantiate(PrefabCorner);
                        cornNW.transform.parent = transform;
                        cornNW.transform.Rotate(0, 180, 0);
                    }
                    else 
                    //Inner
                    if (!OpenSides[Direction.North] && !OpenSides[Direction.West])
                    {
                        GameObject incorNW = Instantiate(PrefabInnerCorner);
                        incorNW.transform.parent = transform;
                        incorNW.transform.Rotate(0, 180, 0);
                    }
                    break;
                case Direction.NorthEast:
                    //Normal
                    if (OpenSides[Direction.North] && OpenSides[Direction.East])
                    {
                        GameObject cornNE = Instantiate(PrefabCorner);
                        cornNE.transform.parent = transform;
                        cornNE.transform.Rotate(0, 90, 0);
                    }
                    else
                    //Inner
                    if (!OpenSides[Direction.North] && !OpenSides[Direction.East])
                    {
                        GameObject incorNE = Instantiate(PrefabInnerCorner);
                        incorNE.transform.parent = transform;
                        incorNE.transform.Rotate(0, 90, 0);
                    }
                    break;
                case Direction.SouthWest:
                    //Normal
                    if (OpenSides[Direction.South] && OpenSides[Direction.West])
                    {
                        GameObject cornSW = Instantiate(PrefabCorner);
                        cornSW.transform.parent = transform;
                        cornSW.transform.Rotate(0, 270, 0);
                    }
                    else
                    //Inner
                    if (!OpenSides[Direction.South] && !OpenSides[Direction.West])
                    {
                        GameObject incorSW = Instantiate(PrefabInnerCorner);
                        incorSW.transform.parent = transform;
                        incorSW.transform.Rotate(0, 270, 0);
                    }
                    break;
                case Direction.SouthEast:
                    //Normal
                    if (OpenSides[Direction.South] && OpenSides[Direction.East])
                    {
                        GameObject cornSE = Instantiate(PrefabCorner);
                        cornSE.transform.parent = transform;
                        cornSE.transform.Rotate(0, 0, 0);
                    }
                    else
                    //Inner
                    if (!OpenSides[Direction.South] && !OpenSides[Direction.East])
                    {
                        GameObject incorSE = Instantiate(PrefabInnerCorner);
                        incorSE.transform.parent = transform;
                        incorSE.transform.Rotate(0, 0, 0);
                    }
                    break;
            }
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
