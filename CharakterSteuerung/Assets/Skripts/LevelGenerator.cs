using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {
    public GameObject[] rooms = new GameObject[6];
    public GameObject[] spawnpoints = new GameObject[6];
	// Use this for initialization
	void Start () {
        Shuffle(rooms);
        Shuffle(spawnpoints);
        Place();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void Place()
    {
        for(int s = 0; s< spawnpoints.Length; s++)
        {
            Instantiate(rooms[s], spawnpoints[s].transform.position, spawnpoints[s].transform.rotation);
        }
    }

    public static void Shuffle(GameObject[] list)
    {
        System.Random rnd = new System.Random();
        for (int i = 0; i < list.Length; i++)
        {
            int rand = rnd.Next(i, list.Length);
            Swap(list, i, rand);
        }         
    }

    public static void Swap(GameObject[] list, int i, int j)
    {
        var temp = list[i];
        list[i] = list[j];
        list[j] = temp;
    }
}
