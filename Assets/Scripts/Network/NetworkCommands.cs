using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class NetworkCommands :  NetworkBehaviour
{
    [Command]
    public void CmdNetworkDestroy(GameObject gameObj)
    {
        NetworkServer.Destroy(gameObj);
    }

    [Command]
    public void CmdNetworkSpawn(GameObject gameObj)
    {
        NetworkServer.Spawn(gameObj);
    }
}
