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

    [ClientRpc]
    public void RpcSyncObjectOnce(Vector3 localPos, Quaternion localRot, GameObject obj, GameObject parent)
    {
        if (obj == null || parent == null)
        {
            return;
        }
        obj.transform.parent = parent.transform;
        obj.transform.localPosition = localPos;
        obj.transform.localRotation = localRot;
    }

    [ClientRpc]
    public void RpcSyncObjectNameOnce(Vector3 localPos, Quaternion localRot, GameObject obj, string name)
    {
        if (obj == null)
        {
            return;
        }
        obj.name = name;
        obj.transform.localPosition = localPos;
        obj.transform.localRotation = localRot;
    }
}
