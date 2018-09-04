using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBase : MonoBehaviour {
    protected float HP;
    public TileTypes TileType;
    protected bool IsDestroyed;
    public Mesh mesh;
    
    // Use this for initialization
    void Start ()
    {
	}

    public void Initialize(TileTypes type)
    {
        TileType = type;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void TakeDamage(float damage)
    {
        HP -= damage;
        IsDestroyed = HP <= 0.0f;
    }

    bool GetIsDestroyed()
    {
        return IsDestroyed;
    }

    float GetHP()
    {
        return HP;
    }

    void SetID(TileTypes type)
    {
        TileType = type;
    }

    TileTypes GetID()
    {
        return TileType;
    }

}
