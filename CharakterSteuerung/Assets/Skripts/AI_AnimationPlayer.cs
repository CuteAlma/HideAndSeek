using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_AnimationPlayer : MonoBehaviour {

    Animator anim;
    bool gameStart = false;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStart == false)
        {
            if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
            {
                anim.SetFloat("startGame", Input.GetAxis("Vertical"));
                gameStart = true;
            }


        }
    }
}
