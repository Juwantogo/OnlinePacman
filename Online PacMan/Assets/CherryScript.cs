using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;



public class CherryScript : NetworkBehaviour {

    private GameObject[] bonusSpawns;
    private List<GameObject> list;
    private GameObject[] removeLastSpawn;
    [SyncVar]
    private GameObject cherry;
    int count = 0;
    //Text test;

   /* public override void OnStartServer()
    {
        bonusSpawns = GameObject.FindGameObjectsWithTag("CherrySpawns");
        cherry = this.gameObject; 
        list = new List<GameObject>(bonusSpawns);
        //look(); 
        count++;
        test = GameObject.Find("Text").GetComponent<Text>();
        test.text = count + ": " + "server Start"; 
    }*/


    // Use this for initialization
    void Start () {
        if (isServer)
        {
            bonusSpawns = GameObject.FindGameObjectsWithTag("CherrySpawns");
            cherry = this.gameObject;
            //test = GameObject.Find("Text").GetComponent<Text>();
            //test.text = count + ": " + "Start";
            list = new List<GameObject>(bonusSpawns);
        }

        //look(); 
        //count++;
        Debug.Log("Start Ran");
        StartCoroutine(lookForSpawns());
        //StartCoroutine(lookForSpawns());
    }

    public IEnumerator lookForSpawns()
    {
        while (true)
        {
            //test.text = count + ": " + bonusSpawns.Length;
            //Debug.Log("NULL1");
            /*Debug.Log("courGoing");
            bonusSpawns = GameObject.FindGameObjectsWithTag("CherrySpawns");


            while (bonusSpawns == null || bonusSpawns.Length == 0)
                {
                Debug.Log("noBonus");
                bonusSpawns = GameObject.FindGameObjectsWithTag("CherrySpawns");
                    yield return new WaitForSeconds(.1f);
                }
                while(bonusSpawns != null)
                {
                //test.text = count + ": " + "isServer "+ isServer + " - " + bonusSpawns[0].name;
                //Debug.Log(bonusSpawns[0]);
                    if (isLocalPlayer)
                    {
                        Debug.Log("isLocalPlayer inside bonus Spawns");
                        this.gameObject.SetActive(true);
                        //Rpcrandom();
                    }
                    yield return new WaitForSeconds(.1f);
            }*/
            yield return new WaitForSeconds(.1f);
            yield break;

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag == "Player")
        {

            if (isServer)
            {
                //gameObject.SetActive(false);
                //int p = collision.gameObject.GetComponent<networkPlayer>().points;
                //collision.gameObject.GetComponent<networkPlayer>().points = p++;
                //Debug.Log(p);
                checkSpawns();
                //Rpcrandom();
                //gameObject.SetActive(true);
            }
            //getRandom();

        }
    }

    void checkSpawns()
    {
        Vector3 spawnPoint = new Vector3(3.9f, -3.83f, -3.48f);

        Debug.Log(isLocalPlayer + "check SPAWNNULL?: " + (bonusSpawns == null));

        if (bonusSpawns != null && bonusSpawns.Length > 0 && removeLastSpawn == null)
        {
            int randNum = UnityEngine.Random.Range(0, bonusSpawns.Length);
            spawnPoint = bonusSpawns[randNum].transform.position;
            list.RemoveAt(randNum);
            //Array.Clear(removeLastSpawn, 0, removeLastSpawn.Length);
            removeLastSpawn = list.ToArray();
        }
        else if (bonusSpawns != null && bonusSpawns.Length > 0)
        {
            //Array3
            //0-2
            //list2
            int randNum = UnityEngine.Random.Range(0, removeLastSpawn.Length);
            spawnPoint = removeLastSpawn[randNum].transform.position;
            List<GameObject> tempList = new List<GameObject>(bonusSpawns);
            tempList.Remove(bonusSpawns[randNum]);
            Array.Clear(removeLastSpawn, 0, removeLastSpawn.Length);
            removeLastSpawn = tempList.ToArray();
        }

        cherry.transform.position = spawnPoint;
        Rpcrandom(spawnPoint);

    }

    [ClientRpc]
    void Rpcrandom(Vector3 spawn)
    {

        cherry.transform.position = spawn;
        //if (isServer)
        // {
        /*   Vector3 spawnPoint = new Vector3(3.9f, -3.83f, -3.48f);

       if(bonusSpawns == null || bonusSpawns.Length == 0)
       {
           bonusSpawns = GameObject.FindGameObjectsWithTag("CherrySpawns");
       }
           //gameObject.SetActive(true);
           //count++;
           //Text test = GameObject.FindGameObjectsWithTag("text1")[0].GetComponent<Text>();
           //test.text = count + ": " + bonusSpawns.Length;

           Debug.Log(isLocalPlayer + "check SPAWNNULL?: " + (bonusSpawns == null));

           if (bonusSpawns != null && bonusSpawns.Length > 0 && removeLastSpawn == null)
           {
               int randNum = UnityEngine.Random.Range(0, bonusSpawns.Length);
               spawnPoint = bonusSpawns[randNum].transform.position;
               list.RemoveAt(randNum);
               //Array.Clear(removeLastSpawn, 0, removeLastSpawn.Length);
               removeLastSpawn = list.ToArray();
           }
           else if (bonusSpawns != null && bonusSpawns.Length > 0)
           {
               //Array3
               //0-2
               //list2
               int randNum = UnityEngine.Random.Range(0, removeLastSpawn.Length);
               spawnPoint = removeLastSpawn[randNum].transform.position;
               List<GameObject> tempList = new List<GameObject>(bonusSpawns);
               tempList.Remove(bonusSpawns[randNum]);
               Array.Clear(removeLastSpawn, 0, removeLastSpawn.Length);
               removeLastSpawn = tempList.ToArray();
           }

           cherry.transform.position = spawnPoint;

       //}*/
        }
}
