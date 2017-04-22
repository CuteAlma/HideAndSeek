using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {
    public Transform target;
    public Transform eyes;
    public float sight = 10f;
    public LayerMask playerLayer;
    public LayerMask obstacleLayer;
    public float chasingSpeed = 0.2f;
    public float reachedTarget = 2f;
    Animator anim;
    bool gameStart = false;
    bool gameOver = false;
    public GameObject girl;


    void Start () {
        anim = GetComponent<Animator>();
    }

    void Update () {
        if (gameStart == false)
        {
            if (Input.GetAxis("Vertical") != 0){
                anim.SetFloat("startGame", Input.GetAxis("Vertical"));
                gameStart = true;
            }else if (Input.GetAxis("Horizontal") != 0){
                anim.SetFloat("startGame", Input.GetAxis("Horizontal"));
                gameStart = true;
            }
        }


        Vector3 direction = (target.position - eyes.position).normalized;
        direction.y = 0;
        RaycastHit hit;
        //Debug.DrawRay(eyes.position, direction * sight);
        if(Physics.Raycast(eyes.position, direction, out hit, sight, obstacleLayer.value)){
            Debug.DrawRay(eyes.position, direction * sight, Color.red);
        }
        else{
            RaycastHit hit2;
            if(Physics.Raycast(eyes.position, direction, out hit2, sight, playerLayer.value))
            {
                Debug.DrawRay(eyes.position, direction * sight, Color.green);
                transform.root.LookAt(target);
                if (gameOver == false)
                {
                    if (Vector3.Distance(transform.root.position, target.position) > reachedTarget)
                    {
                        // transform.root.position += transform.root.forward * chasingSpeed;
                        girl.GetComponent<Rigidbody>().velocity = Vector3.zero;
                        girl.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                        girl.GetComponent<Rigidbody>().isKinematic = true;
                        girl.isStatic = true;
                        gameOver = true;
                        anim.SetFloat("gameOver", 0.2f);
                    }
                    else
                    {
                        //anim.SetFloat("gameOver", 0.2f);

                    }
                }
            }
        }
	}


    
}
