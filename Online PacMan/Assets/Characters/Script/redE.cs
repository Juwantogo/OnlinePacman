using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class redE : NetworkBehaviour
{

    Vector3 goal1;
    Vector3 goal2;
    Vector3 pDetect;
    NavMeshAgent myNav = null;
    public GameObject Good;
    private int goal = 0;
    //public GameObject enemy;

    void Start()
    {
            myNav = this.gameObject.GetComponent<NavMeshAgent>();
            goal1 = GameObject.Find("EnemySpawn").transform.position;
            goal2 = GameObject.Find("EnemyPatrol").transform.position;
            myNav.destination = goal2;
            myNav.Resume();
        //StartCoroutine(setGoal());
       
    }

    void Update()
    {
        if (isServer)
        {
            RpcsetGoal();
        }
    }

    [ClientRpc]
    void RpcsetGoal()
    {
        if (isServer)
        {
            //Debug.Log("isServer Hit");
            if (myNav.pathPending)
            {
                return;
            }
            else if (myNav.remainingDistance == 0 && goal == 0)
            {
                //Debug.Log("goal is 0");
                //Debug.Log(myNav.remainingDistance);
                goal = 1;
                myNav.SetDestination(goal1);
               
                myNav.Resume();
            }
            else if (myNav.remainingDistance == 0 && goal == 1)
            {
                //Debug.Log("goal is 1");

                goal = 0;
                myNav.SetDestination(goal2);
                myNav.Resume();
            }
        }

    }
}
