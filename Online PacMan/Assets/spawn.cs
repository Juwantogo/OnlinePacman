using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class spawn : NetworkBehaviour {
    [SerializeField] GameObject enemy1;
    [SerializeField] GameObject spawnSpot1;
    [SerializeField] GameObject enemy2;
    [SerializeField] GameObject spawnSpot2;
    [SerializeField] GameObject cherry;
    [SerializeField] GameObject spawnSpot3;

    // Use this for initialization
    public override void OnStartServer()
    {
        //Debug.Log("spawnAredE");
        //enemy = this.gameObject;
        GameObject e1 = GameObject.Instantiate(enemy1, spawnSpot1.transform.position, Quaternion.identity);
        GameObject e2 = GameObject.Instantiate(enemy2, spawnSpot2.transform.position, Quaternion.identity);
        GameObject collect = GameObject.Instantiate(cherry, spawnSpot3.transform.position, Quaternion.identity);
        NetworkServer.Spawn(e1);
        NetworkServer.Spawn(e2);
        NetworkServer.Spawn(collect);

    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
