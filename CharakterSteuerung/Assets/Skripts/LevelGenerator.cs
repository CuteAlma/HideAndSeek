using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {
    public GameObject[] rooms = new GameObject[6];
    public GameObject[] spawnpoints = new GameObject[6];
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void Shuffle<T>(GameObject[] list, Random rnd)
    {
        for (int i = 0; i < list.Length; i++)
            Swap(list,i, rnd.Next(i, list.Length));
    }

    public static void Swap<T>(GameObject[] list, int i, int j)
    {
        var temp = list[i];
        list[i] = list[j];
        list[j] = temp;
    }
}
