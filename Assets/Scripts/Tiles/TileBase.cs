using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBase : MonoBehaviour {
    protected float HP = 1.0f;
    protected TileTypes TileType = TileTypes.Empty;
    protected bool IsDestroyed = false;
    protected TileBase[] AdjacentTiles = new TileBase[4];
    protected bool IsFlat = false;
    protected EmptySides EmptySides;

    // Use this for initialization
    protected virtual void Start ()
    {
        TileType = TileTypes.Empty;
	}

	// Update is called once per frame
	void Update ()
    {
		
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
}
