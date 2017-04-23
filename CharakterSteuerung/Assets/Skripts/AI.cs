using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {
    public Transform target;
    public Transform eyes;
    public LayerMask playerLayer;
    public LayerMask obstacleLayer;

    public float sight = 10f;
    public float chasingSpeed = 0.2f;
    public float reachedTarget = 2f;
    public float radius;
    Vector3 destination;

    bool gameStart = false;
    bool gameOver = false;
    bool wait = true;

    Animator anim;
    AudioSource audio;
    public AudioClip gameOverSound;
    private UnityEngine.AI.NavMeshAgent agent;


    void Start () {
        destination = eyes.position;
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
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
        if (gameOver == false && wait==false)
        {

            if (!agent.pathPending)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        getNewDestination();
                    }
                }
            }

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
                    StartCoroutine("CoRoutineGameOver");
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

    IEnumerator CoRoutineGameOver()
    {
        Debug.logger.Log("Begin GameOver");
        yield return new WaitForSeconds(0.5f);
        Debug.logger.Log("End GameOver");
        Time.timeScale = 0;
    }

    IEnumerator CoRoutineWaitForAnimationEnd()
    {
        Debug.logger.Log("Begin Animation");
        yield return new WaitForSeconds(4.3f);
        Debug.logger.Log("End Animation");
        wait = false;
        anim.SetFloat("searching", 0.00001f);
    }

    void getNewDestination()
    {
        radius = GetRandomNumber();
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        UnityEngine.AI.NavMeshHit hit;
        UnityEngine.AI.NavMesh.SamplePosition(randomDirection, out hit, radius, 1);
        destination = hit.position;

        agent.SetDestination(destination);

    }

    public float GetRandomNumber()
    {
        return Random.Range(1.0f, 40.0f);
    }


    void wakeUPGirl()
    {
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            getNewDestination();
            anim.SetFloat("startGame", 1f);
            gameStart = true;
            wait = false;
        }
        
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.layer == 10)
        {
            Debug.logger.Log("COLLISION DETECTED");
            agent.ResetPath();
            wait = true;
            anim.SetFloat("searching", 0.2f);
            StartCoroutine("CoRoutineWaitForAnimationEnd");
            
        }
    }



}

