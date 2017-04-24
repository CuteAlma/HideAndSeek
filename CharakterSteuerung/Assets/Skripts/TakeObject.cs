using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TakeObject : MonoBehaviour {

    public int distance;
    public Text text;
    public bool gameEnd = false;
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
                if (Physics.Raycast(ray, out hit, distance))
                {
                    Debug.Log(hit.collider.name);
                    Pickupable p = hit.collider.GetComponent<Pickupable>();
                    if (p != null)
                    {
                        Destroy(hit.collider.gameObject);
                        if (p.transform.parent.gameObject.tag == LevelGenerator.searchedobj.tag)
                        {
                            text.text = "Gewonnen";
                            gameEnd = true;
                            StartCoroutine("CoRoutineGameOver");
                        }
                        else
                        {
                            text.text = "Verloren";
                            gameEnd = true;
                            StartCoroutine("CoRoutineGameOver");
                        }
                    }
                }
            
        }

    }

    IEnumerator CoRoutineGameOver()
    {
        Debug.logger.Log("Begin GameOver");
        yield return new WaitForSeconds(0.5f);
        Debug.logger.Log("End GameOver");
        Time.timeScale = 0;
    }
}
