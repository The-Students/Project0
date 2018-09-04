using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBase : MonoBehaviour {
    protected float HP;
    protected bool IsDestroyed;

	// Use this for initialization
	void Start ()
    {
		
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
}
