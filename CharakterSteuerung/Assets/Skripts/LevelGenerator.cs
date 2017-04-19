using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {
    public GameObject[] rooms = new GameObject[6];
    public GameObject[] roomspawnpoints = new GameObject[6];
    public GameObject[] objects = new GameObject[4];
    // Use this for initialization
    void Start () {
        System.Random rnd = new System.Random();
        Shuffle(rooms, rnd);
        Shuffle(roomspawnpoints, rnd);
        Place(rooms, roomspawnpoints);

        GameObject[] objectspawnpoints = new GameObject[6];
        objectspawnpoints[0] = GameObject.Find("ObjectSpawn_Arbeitszimmer");
        objectspawnpoints[1] = GameObject.Find("ObjectSpawn_Kueche");
        objectspawnpoints[2] = GameObject.Find("ObjectSpawn_Wohnzimmer");
        objectspawnpoints[3] = GameObject.Find("ObjectSpawn_Schlafzimmer");
        objectspawnpoints[4] = GameObject.Find("ObjectSpawn_Kinderzimmer");
        objectspawnpoints[5] = GameObject.Find("ObjectSpawn_Esszimmer");

        Shuffle(objects, rnd);
        Shuffle(objectspawnpoints, rnd);
        Place(objects, objectspawnpoints);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void Place(GameObject[] objects, GameObject[] spawns)
    {
        for(int s = 0; s < spawns.Length && s < objects.Length; s++)
        {
            Instantiate(objects[s], spawns[s].transform.position, spawns[s].transform.rotation);
        }
    }

    public static void Shuffle(GameObject[] list, System.Random rnd)
    {
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
