using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MyNetworkManager : NetworkManager
{
    /*
    [SerializeField] GameObject finishPrefab;
    [SerializeField] Vector3 finishPosition;

    [SerializeField] Quaternion finishRotation;
    */
    SharedData shared;
    PlayerController player;
    public int pCount = 0;
    public override void OnStartServer()
    {
        Debug.Log("Server Started!");
        base.OnStartServer();

    }

    public override void OnStopServer()
    {
        Debug.Log("Server Stopped!");
        base.OnStopServer();
    }

    public override void OnServerReady(NetworkConnectionToClient conn)
    {
        shared = GameObject.FindGameObjectWithTag("shared").GetComponent<SharedData>();
        base.OnServerReady(conn);
    }


    public override void OnClientConnect()
    {
        Debug.Log("Connected to Server!");
        base.OnClientConnect();
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        //shared.SetSharedValue(pCount);
        //Debug.Log("Player Joined " + shared.GetSharedValue());
        pCount++;
        shared.SetPlayerCount(pCount);
        base.OnServerAddPlayer(conn);
    }

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        //Debug.Log("Disconnected from Server!");
        //pCount--;
        //shared.SetSharedValue(pCount);
        if (pCount > 0) pCount--;
        shared.SetPlayerCount(pCount);
        base.OnServerDisconnect(conn);
    }

    public void testNetwork()
    {
        shared.SetCountDown(1000);
        Debug.Log("testNetwork!");
    }
}