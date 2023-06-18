using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class MinionAI : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent agent;
    private Animator anim;
    public GameObject[] waypoints;
    private int currWaypoint;
    public enum MinionState {Patrol,Chase};
    private MinionState currentState;
    public VelocityReporter movingWaypointVelocity;
    float lookaheadTime = 6.0f; 
    public Vector3 predictedPosition;
    public bool isCatached;
    public bool isChasing;



    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponent<Animator>();
        currentState = MinionState.Patrol;
        currWaypoint = -1;
        setNextWaypoint();
        isCatached = false;
        isChasing = false;
    }

    void Update()
    {
        anim.SetFloat("vely", agent.velocity.magnitude / agent.speed);
        switch (currentState)
        {   
            case MinionState.Patrol:
                isCatached = false;
                isChasing = false;
                if (!agent.pathPending && agent.remainingDistance <= 0.1)
                {
                    if (currWaypoint<=3) {
                         setNextWaypoint();
                    } else {
                        currentState = MinionState.Chase;
                    }
                }
                break;

            case MinionState.Chase:
                isChasing = true;
                predictedPosition = movingWaypointVelocity.transform.position + movingWaypointVelocity.velocity * lookaheadTime;
                agent.SetDestination(predictedPosition);

                if(Vector3.Distance(transform.position, movingWaypointVelocity.transform.position)<0.3) 
                {
                    isCatached = true;
                    currentState = MinionState.Patrol;
                    Debug.LogError("Catched");
                    currWaypoint = -1;
                }
                break;
        }
    }

    private void setNextWaypoint()
    {
        if (waypoints.Length == 0) 
        {
            Debug.LogError("waypoints array is empty");
        } else 
        {
            currWaypoint = (currWaypoint + 1) % 5;
            agent.SetDestination(waypoints[currWaypoint].transform.position);
        }
        
    }
}
