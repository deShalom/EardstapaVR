using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animation))]

public class chickenAI : MonoBehaviour {

    [SerializeField]
    Transform Target;

    private enum States { Idle, Move, Run, Travelling }
    [SerializeField]
    private States currentState;

    float distance = 0;
    float roamDistance;
    float speed = 5.0f;
    float minAnimationTime = 2.0f;
    float maxAnimationTime = 5.0f;
    float animationTime = 0;
    float time = 0;
    float rotSpeed = 50;

    NavMeshAgent agent;
    Animator anim;
    Vector3 moveDirection;
    bool isIdle = true;

    void Start () {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        currentState = States.Idle;
        SetAnimationTime();
    }
	
	void Update () {

        //Checks to see if Chicken has seen a Target or not
        if (Target != null)
        {
            distance = Vector3.Distance(transform.position, Target.position);
        }
        else
        {
            distance = 0;
        }

        switch (currentState)
        {
            case States.Idle:
                {
                    //The below function is currently causing NullReference errors, for reasons unknown
                    //SearchTarget();
                    agent.isStopped = true;
                    anim.SetBool("Idle", true);
                    break;
                }
            case States.Move:
               {
                   randomRoam();
                    break;
                }
            case States.Run:
               {
                   RunFromTarget();
                    break;
               }
            case States.Travelling:
                {
                    destinationReached();
                    agent.isStopped = false;
                    anim.SetBool("Walk", true);
                    break;
                }
        }

    }

    void FixedUpdate()
    {
        time += Time.deltaTime;

        //Checks the current State of the Chicken and changes it accordingly
        UpdateState();
    }

    void destinationReached()
    {
        if (Vector3.Distance(agent.destination, agent.transform.position) <= agent.stoppingDistance)
        {
            //if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            if (!agent.hasPath || agent.velocity.magnitude == 0f)
            {
                currentState = States.Idle;
            }
        }
    }

    void UpdateState()
    {
        if (time >= animationTime)
        {
            time = 0;
            SetAnimationTime();

            if (currentState == States.Idle)
            {
                currentState = States.Move;
            }
            else
            {
                currentState = States.Idle;
            }
        }
    }

    void SetAnimationTime()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        animationTime = Random.Range(minAnimationTime, maxAnimationTime + 2);
    }

    private void SearchTarget()
    {
        Vector3 centre = transform.position + (agent.height / 2) * transform.up;
        RaycastHit hit;
        Ray ray = new Ray(centre, transform.forward);

        if (Physics.SphereCast(ray, 2, out hit, 100))
        { 
            if (hit.rigidbody.tag == "Player")
            {
                Target = hit.transform;
                currentState = States.Run;
            }
        }
    }

    public static Vector3 roamLocation(Vector3 origin, float dist, int layermask)
    {
        Vector3 roamDirection = Random.insideUnitSphere * dist;

        roamDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(roamDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    private void randomRoam()
    {
        //Set the location, and direction of the chicken
        roamDistance = 100;
        directionAndLocation();
    }

    private void RunFromTarget()
    {
        //Set the location, and direction of the chicken
        roamDistance = 100;
        directionAndLocation();
    }

        public void directionAndLocation()
    {
        //Sets the direction the chicken is facing
        moveDirection = new Vector3(0, Random.value * 360, Time.deltaTime * rotSpeed);
        //The trigger that rotates the chicken
        transform.Rotate(moveDirection);
        //Sets the location for the chicken to move to
        Vector3 newPos = roamLocation(transform.position, roamDistance, -1);
        //Moves the chicken to it's new location
        agent.SetDestination(newPos);
        //Sets the chickens state to travelling
        currentState = States.Travelling;
    }

}
