using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class myNetworkManager : NetworkManager {
    public int numOfPlayers = 0;
    public string gName;
    //public Material col;
    public Color col;


    // called when a new player is added for a client
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        //Transform startPos = GetStartPosition();
            numOfPlayers++;
            gName = getName();
            col = getColor().color;
            playerPrefab.GetComponent<Renderer>().sharedMaterial.color = col;
            GameObject player = (GameObject)GameObject.Instantiate(playerPrefab, GetStartPosition().position, Quaternion.identity);
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }

    void OnPlayerDisconnected(NetworkPlayer player)
    {
        //numOfPlayers--;
        Debug.Log("Clean up after player " + player);
        Network.RemoveRPCs(player);
        Network.DestroyPlayerObjects(player);
    }




    public override void OnServerConnect(NetworkConnection conn)
    {
        //numOfPlayers++;
        //Debug.Log("number" + numOfPlayers);
    }

    public override void OnServerDisconnect(NetworkConnection conn)
   {
        numOfPlayers--;
        NetworkServer.DestroyPlayersForConnection(conn);
    }

    public string getName()
    {
        string pName = "yellow";
        switch (numOfPlayers)
        {
            case 1:
                pName = "yellow";
                break;
            case 2:
                pName = "green";
                break;
            case 3:
                pName = "blue";
                break;
            case 4:
                pName = "white";
                break;
            default:

                break;
        }
        return pName;
    }

    public Material getColor()
    {
        Material pColor = (Material)Resources.Load("PacMan", typeof(Material));
        switch (numOfPlayers)
        {
            case 1:
                pColor = (Material)Resources.Load("PacMan", typeof(Material)); 
                break;
            case 2:
                pColor = (Material)Resources.Load("Green", typeof(Material)); 
                break;
            case 3:
                pColor = (Material)Resources.Load("Blue", typeof(Material)); 
                break;
            case 4:
                pColor = (Material)Resources.Load("White", typeof(Material));
                break;
            default:
                break;
        }
        return pColor;
    }
    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
