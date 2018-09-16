using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public enum PlayerMode
{
    RTS,
    FIRSTPERSON
}
public enum PlayerState
{
    BASESTATE,
    BUILDING,
    PICKUP,
    CASTING,
}



public class PlayerBase : NetworkBehaviour {

    public PlayerState _PlayerState;
    public PlayerMode _PlayerMode;

	// Use this for initialization
	void Awake ()
    {
        _PlayerMode = PlayerMode.RTS;
        _PlayerState = PlayerState.BASESTATE;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!isLocalPlayer )
        {
            return;
        }
	}

    public void SetPlayerMode(PlayerMode mode)
    {
        _PlayerMode = mode;
    }

    public void SetPlayerState(PlayerState state)
    {
        _PlayerState = state;
    }

    public PlayerMode GetPlayerMode()
    {
        return _PlayerMode;
    }

    public PlayerState GetPlayerState()
    {
        return _PlayerState;
    }

    public NetworkInstanceId GetPlayerID()
    {
        return netId;
    }


    //Server commands


}
