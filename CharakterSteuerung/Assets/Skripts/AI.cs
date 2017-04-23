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
    AudioSource audio;
    public AudioClip gameOverSound;



    void Start () {
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }

    void Update () {
        Vector3 direction = target.position - eyes.position;
        direction.y = 0;
        var rotation = Quaternion.LookRotation(direction);
        if (gameStart == false)
        {
            wakeUPGirl();
        }
        
        RaycastHit hit;
        if (gameOver == false)
        {
            
            if (Physics.Raycast(eyes.position, direction, out hit, sight, obstacleLayer.value))
            {
                Debug.DrawRay(eyes.position, direction * sight, Color.red);
            }
            else
            {
                RaycastHit hit2;
                if (Physics.Raycast(eyes.position, direction, out hit2, sight, playerLayer.value))
                {
                    audio.PlayOneShot(gameOverSound, 5f);
                    Debug.DrawRay(eyes.position, direction * sight, Color.green);
                    anim.SetFloat("gameOver", 0.2f);
                    Debug.logger.Log("GAME OVER");
                    StartCoroutine("CoRoutineWaitForAnimationEnd");
                    gameOver = true;
                    
                }
            }
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 4f);
            /*if (Vector3.Distance(transform.root.position, target.position) > reachedTarget)
            {
                transform.root.position += transform.root.forward * chasingSpeed;

            }*/

        }
        
	}

    IEnumerator CoRoutineWaitForAnimationEnd()
    {
        Debug.logger.Log("Begin");
        yield return new WaitForSeconds(0.5f);
        Debug.logger.Log("End");
        Time.timeScale = 0;
    }


    void wakeUPGirl()
    {
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            anim.SetFloat("startGame", 1f);
            gameStart = true;
        }
        
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.layer == 10)
        {
            Debug.logger.Log("COLLISION DETECTED");
            anim.SetFloat("gameOver", 0.2f);
        }
    }



}

