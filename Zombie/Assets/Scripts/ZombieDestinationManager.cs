using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieDestinationManager : MonoBehaviour {

    public NavMeshAgent[] Destination1Zombies, Destination2Zombies, Destination3Zombies;
    public Transform[] Destinations;

	// Use this for initialization
	void Start () {
		for(int i = 0; i < Destination1Zombies.Length; i++)
        {
            Destination1Zombies[i].SetDestination(Destinations[0].position);
        }
        for (int i = 0; i < Destination2Zombies.Length; i++)
        {
            Destination2Zombies[i].SetDestination(Destinations[1].position);
        }
        for (int i = 0; i < Destination3Zombies.Length; i++)
        {
            Destination3Zombies[i].SetDestination(Destinations[2].position);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
