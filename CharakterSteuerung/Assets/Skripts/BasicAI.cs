using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAI : MonoBehaviour
{
    //public Transform destination;
    Vector3 startPosition;
    Vector3 destination;
    float radius = 40;

    private UnityEngine.AI.NavMeshAgent agent;

    void Start()
    {   

        agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();

            getNewDestination();


        /*startPosition = GameObject.Find("Alma").transform.position;
        rangeCalc = new System.Random();
        targets.Add(GameObject.Find("target1"));
        targets.Add(GameObject.Find("target2"));
        targets.Add(GameObject.Find("target3"));*/

    }

    void Update()
    {
        //GameObject obj = targets[Random.Range(0, targets.Count)];
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
        
        //agent.SetDestination(destination);

    }

    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }
        if (collision.relativeVelocity.magnitude > 2)
            getNewDestination();
    }

    void getNewDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        UnityEngine.AI.NavMeshHit hit;
        UnityEngine.AI.NavMesh.SamplePosition(randomDirection, out hit, radius, 1);
        destination = hit.position;

        agent.SetDestination(destination);

    }



    /*public Transform target;
    public float speed = 3;
    Vector3 startPosition;
    Vector3 newPosition;

    float radius = 3;

    // Use this for initialization
    void Start()
    {
        startPosition = GameObject.Find("Alma").transform.position;
    }

    void Update()
    {
        //getNewPosition();
        Vector3 direction = startPosition - newPosition;
        float distance = direction.magnitude;
        direction = direction.normalized;
        float move = speed * Time.deltaTime;
        if (move > distance) move = distance;
        transform.Translate(direction * move);
    }

    // Update is called once per frame
    public void getNewPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += startPosition;
        UnityEngine.AI.NavMeshHit hit;
        UnityEngine.AI.NavMesh.SamplePosition(randomDirection, out hit, radius, 1);
        newPosition = hit.position;
       // _nav.destination = newPosition;
    }*/
}
