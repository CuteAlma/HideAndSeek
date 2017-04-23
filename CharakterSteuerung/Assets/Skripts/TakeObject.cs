using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeObject : MonoBehaviour {

    public int distance;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonUp(1))//("Action")
        {
            Debug.Log("Button pressed");
            int x = Screen.width / 2;
            int y = Screen.height / 2;

            Ray ray = Camera.main.ScreenPointToRay(new Vector3(x, y));
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, distance))
            {
                Debug.Log(hit.collider.name);
                Pickupable p = hit.collider.GetComponent<Pickupable>();
                Debug.Log("Item hit");
                if (p != null)
                {
                    Debug.Log("wuerfel hit");
                    Destroy(hit.collider.gameObject);
                    Debug.Log("Parent: " + p.transform.parent.gameObject.tag);
                    Debug.Log("SearchedObj: " + LevelGenerator.searchedobj.tag);
                    if (p.transform.parent.gameObject.tag == LevelGenerator.searchedobj.tag)
                    {
                        Debug.Log("Gewonnen");
                    } else
                    {
                        Debug.Log("Verloren");
                    }
                    //p.gameObject.SetActive(false);
                    //Event einbauen, ->textausgabe
                }
            }
        }

    }
}
