using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using UnityEngine;

public class TileBase : NetworkBehaviour {
    protected float HP = 1.0f;
    protected TileTypes TileType = TileTypes.Empty;
    protected bool IsDestroyed = false;
    protected TileBase[] AdjacentTiles = new TileBase[8];
    protected bool IsFlat = false;
    protected Dictionary<Direction, bool> OpenSides = new Dictionary<Direction, bool>();


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
            if (transform != ChildTransform) Destroy(ChildTransform.gameObject);
        }

        //ADD TOP
        GameObject top = Instantiate(PrefabTop);
        SetPart(ref top);

        foreach (KeyValuePair<Direction, bool> Side in OpenSides)
        {
            switch (Side.Key)
            {
                //ADD WALLS
                case Direction.North:
                    if (Side.Value)
                    {
                        GameObject wallN = Instantiate(PrefabWall);
                        SetPart(ref wallN);
                        wallN.transform.RotateAround(transform.position, Vector3.up, 180);
                    }
                    break;
                case Direction.East:
                    if (Side.Value)
                    {
                        GameObject wallE = Instantiate(PrefabWall);
                        SetPart(ref wallE);
                        wallE.transform.RotateAround(transform.position, Vector3.up, 90);
                    }
                    break;
                case Direction.South:
                    if (Side.Value)
                    {
                        GameObject wallS = Instantiate(PrefabWall);
                        SetPart(ref wallS);
                        wallS.transform.RotateAround(transform.position, Vector3.up, 0);
                    }
                    break;
                case Direction.West:
                    if (Side.Value)
                    {
                        GameObject wallW = Instantiate(PrefabWall);
                        SetPart(ref wallW);
                        wallW.transform.RotateAround(transform.position, Vector3.up, 270);
                    }
                    break;
                //ADD CORNERS
                case Direction.NorthWest:
                    if (Side.Value)
                    {
                        //Normal
                        if (OpenSides[Direction.North] && OpenSides[Direction.West])
                        {
                            GameObject cornNW = Instantiate(PrefabCorner);
                            SetPart(ref cornNW);
                            cornNW.transform.RotateAround(transform.position, Vector3.up, 180);
                        }
                        else
                        //Inner
                        if (!OpenSides[Direction.North] && !OpenSides[Direction.West])
                        {
                            GameObject incorNW = Instantiate(PrefabInnerCorner);
                            SetPart(ref incorNW);
                            incorNW.transform.RotateAround(transform.position, Vector3.up, 180);
                        }
                    }
                    break;
                case Direction.NorthEast:
                    if (Side.Value)
                    {
                        //Normal
                        if (OpenSides[Direction.North] && OpenSides[Direction.East])
                        {
                            GameObject cornNE = Instantiate(PrefabCorner);
                            SetPart(ref cornNE);
                            cornNE.transform.RotateAround(transform.position, Vector3.up, 90);
                        }
                        else
                        //Inner
                        if (!OpenSides[Direction.North] && !OpenSides[Direction.East])
                        {
                            GameObject incorNE = Instantiate(PrefabInnerCorner);
                            SetPart(ref incorNE);
                            incorNE.transform.RotateAround(transform.position, Vector3.up, 90);
                        }
                    }
                    break;
                case Direction.SouthWest:
                    if (Side.Value)
                    {
                        //Normal
                        if (OpenSides[Direction.South] && OpenSides[Direction.West])
                        {
                            GameObject cornSW = Instantiate(PrefabCorner);
                            SetPart(ref cornSW);
                            cornSW.transform.RotateAround(transform.position, Vector3.up, 270);
                        }
                        else
                        //Inner
                        if (!OpenSides[Direction.South] && !OpenSides[Direction.West])
                        {
                            GameObject incorSW = Instantiate(PrefabInnerCorner);
                            SetPart(ref incorSW);
                            incorSW.transform.RotateAround(transform.position, Vector3.up, 270);
                        }
                    }
                    break;
                case Direction.SouthEast:
                    if (Side.Value)
                    {
                        //Normal
                        if (OpenSides[Direction.South] && OpenSides[Direction.East])
                        {
                            GameObject cornSE = Instantiate(PrefabCorner);
                            SetPart(ref cornSE);
                            cornSE.transform.RotateAround(transform.position, Vector3.up, 0);
                        }
                        else
                        //Inner
                        if (!OpenSides[Direction.South] && !OpenSides[Direction.East])
                        {
                            GameObject incorSE = Instantiate(PrefabInnerCorner);
                            SetPart(ref incorSE);
                            incorSE.transform.RotateAround(transform.position, Vector3.up, 0);
                        }
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
        return BreaksInto.GetComponent<TileBase>().TileType;
    }

    public void SetAdjacent(Direction dir, TileBase tile)
    {
        AdjacentTiles[(int)dir] = tile;

        bool val;
        if (OpenSides.TryGetValue(dir, out val)) OpenSides[dir] = AdjacentTiles[(int)dir].IsFlat;
        else OpenSides.Add(dir, AdjacentTiles[(int)dir].IsFlat);
    }

    private void SetPart(ref GameObject obj)
    {
        //Temp can be deleted later
        Vector3 temp = obj.transform.position;
        temp.Scale(transform.localScale);
        obj.transform.position = temp;
        ///////////////////////////

        temp = obj.transform.localScale;
        temp.Scale(transform.localScale);
        obj.transform.localScale = temp;

        obj.transform.position += transform.position;
        obj.transform.parent = transform;
    }
}
