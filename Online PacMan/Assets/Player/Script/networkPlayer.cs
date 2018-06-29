using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class networkPlayer : NetworkBehaviour{
    public GameObject prefab;
    public bool spawned = false; 
    public myNetworkManager manager;
   [SyncVar]
    public int playerCount;
    [SyncVar]
    public GameObject myPiece;
    [SyncVar]
    public GameObject enemy;
    //[SyncVar]
    //public string myPiece;
    [SyncVar]
    public int points;
    [SyncVar]
    public string playerName;
    [SyncVar]
    public Color thisCol;
    public Canvas canvasA;

    //private string[] names = ToArray;

    private NetworkStartPosition[] spawnPoints;
    private networkPlayer[] playerList;


    // Use this for initialization
    void Start()
    {
        canvasA = GameObject.Find("Canvas").GetComponent<Canvas>();
        canvasA.enabled = false;
        playerList = FindObjectsOfType<networkPlayer>();
        if (isServer)
       {
            //GameObject Temp = Instantiate(prefab);
            //NetworkServer.Spawn(Temp);
            myPiece = this.gameObject;
            myPiece.GetComponent<Renderer>().enabled = false;
            //manager = GameObject.Find("Network Manager").GetComponent<myNetworkManager>();
            //enemy = GameObject.Find("enemy1");
            manager = GameObject.Find("Network Manager").GetComponent<myNetworkManager>();
            playerName = manager.gName;
            thisCol = manager.col;
            myPiece.GetComponent<Renderer>().material.color = thisCol;
            //RpcupdateColor();
            spawnPoints = FindObjectsOfType<NetworkStartPosition>();
            Debug.Log(isServer + " Color " + manager.getColor().name);
            //points = 0;
        }
        StartCoroutine(slowUpdate());
    }

    public IEnumerator slowUpdate()
    {
        while (true)
        {
            while (myPiece == null)
            {               
                // Debug.Log("myPiece null");
                yield return new WaitForSeconds(.1f);
            }
            while (myPiece != null)
            {
                Debug.Log(isServer + ":" + isLocalPlayer + " " + thisCol);
                if (!spawned && isLocalPlayer)
                {
                    CmdrequestCol();

                    //myPiece.GetComponent<Renderer>().material.color = thisCol;
                    //NetworkServer.Spawn(myPiece);
                    spawned = true;
                }
                if (isLocalPlayer)
                {
                    //I should try/catch here.
                    float xt = Input.GetAxisRaw("Horizontal");
                    float yt = Input.GetAxisRaw("Vertical");
                    bool tab = Input.GetKey("tab");
                    if (tab)
                    {
                        
                        Text me = canvasA.transform.FindChild("Panel").FindChild("Me").GetComponent<Text>();
                        Text o1 = canvasA.transform.FindChild("Panel").FindChild("op1").GetComponent<Text>();
                        Text o2 = canvasA.transform.FindChild("Panel").FindChild("op2").GetComponent<Text>();
                        Text o3 = canvasA.transform.FindChild("Panel").FindChild("op3").GetComponent<Text>();
                        //List<Text> ops = new List<Text>();
                        Text[] ops = new Text[4];
                        ops[0] = me;
                        ops[1] = o1;
                        ops[2] = o2;
                        ops[3] = o3;
                        me.text = "0";
                        o1.text = "0";
                        o2.text = "0";
                        o3.text = "0";
                        Debug.Log("TAABBBBB");
                        Debug.Log(points);
                        playerList = FindObjectsOfType<networkPlayer>();
                        int opCount = 0;
                      /*  for(int i = 0; i < playerList.Length; i++)
                        {
                            //playerList[i].playerName;
                            if (playerList[i].playerName == playerName)
                            {
                                me.color = playerList[i].thisCol;
                                me.text = "Me: " + playerList[i].points;
                            }
                            else
                            {
                                ops[opCount].color = playerList[i].thisCol;
                                ops[opCount].text = playerList[i].playerName + ": " + playerList[i].points;
                                opCount++;

                            }
                        }*/
                            foreach (networkPlayer pl in playerList)
                             {
                            ops[opCount].color = pl.thisCol;
                            ops[opCount].text = pl.playerName + ": " + pl.points;
                            opCount++;
                            /*
                            opCount++;
                                 if(pl.playerName == playerName)
                                 {
                                     me.color = pl.thisCol;
                                     me.text = "Me: " + pl.points;
                                 }
                                 else
                                 {
                                     .color = pl.thisCol;
                                     me.text = "Me: " + pl.points;
                                 }*/
                             }
                        // Debug.Log(pl.playerName + " : "+ pl.points);

                        canvasA.enabled = !canvasA.enabled;
                        yield return new WaitForSeconds(.05f);
                    }
                    Vector3 tempV = new Vector3(xt, 0, yt) * 5.0f;
                    if (
                    (!isServer && myPiece.GetComponent<NetworkTransform>().targetSyncVelocity != tempV) ||
                    (isServer && myPiece.GetComponent<Rigidbody>().velocity != tempV)
                    )
                    {
                        //Send the command only when the velocity is different 
                        //Then the user input.
                        Cmd_Move(xt, yt);
                    }
                }

                yield return new WaitForSeconds(.05f);
            }
            ///while(enemy != null)
            //{
            //}


        }
    }

    void showMenu()
    {

    }

    [ClientRpc]
    void RpcupdateColor(Color c)
    {
        //runs on all clients

        //updates color of players just added
        //players before being added is yellow
        thisCol = c;
        if (isClient)
        {
            playerList = FindObjectsOfType<networkPlayer>();
            foreach (networkPlayer pl in playerList)
            {
               pl.gameObject.GetComponent<Renderer>().material.color = pl.thisCol;
            //Debug.Log(pl.playerName + " : " + pl.points);
            }
            myPiece.GetComponent<Renderer>().material.color = thisCol;
            myPiece.GetComponent<networkPlayer>().thisCol = thisCol;
        }
        if (isServer) { 
}
        myPiece.GetComponent<Renderer>().enabled = true;
    }

    [Command]
    public void CmdrequestCol()
    {
        //just runs on host
        //playerList = FindObjectsOfType<networkPlayer>();
        //foreach (networkPlayer pl in playerList)
        //{
         //   pl.gameObject.GetComponent<Renderer>().material.color = pl.thisCol;
            //Debug.Log(pl.playerName + " : " + pl.points);
        //}
        RpcupdateColor(thisCol);

    }

    void showBoard()
    {
        //foreach(Player in NetworkManager.Instantiate)
    }
    [Command]
    public void Cmd_points()
    {
        Rpc_points();
    }

    [ClientRpc]
    public void Rpc_points()
    {
        if(name == "green")
        {

        }
        points = points;
    }

    [Command]
    public void Cmd_Move(float x, float y)
    {
        myPiece.GetComponent<Rigidbody>().velocity = new Vector3(x, 0, y) * 5.0f;
    }

    [Command]
    public void Cmd_enemy1()
    {
        enemy.GetComponent<Rigidbody>().velocity = new Vector3(.1f, .1f, 0) * 5.0f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {

            if (isServer){

              RpcRespawn();
            }
            
        }
        if (collision.gameObject.tag == "Cherry")
        {
            if (isServer)
            {
                points++;
            }

        }
    }

    //[ClientRpc]
    //void RpcAddPoints()
    //{

    //}

    [ClientRpc]
    void RpcRespawn()
    {
        //if (isLocalPlayer)
        //{
            // Set the spawn point to origin as a default value
            Vector3 spawnPoint = Vector3.zero;

            // If there is a spawn point array and the array is not empty, pick one at random
            if (spawnPoints != null && spawnPoints.Length > 0)
            {
                spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
            }

            // Set the player’s position to the chosen spawn point
            myPiece.transform.position = spawnPoint;
       // }
    }

}

//if isServer
//owner == null
//destroyGamePiece