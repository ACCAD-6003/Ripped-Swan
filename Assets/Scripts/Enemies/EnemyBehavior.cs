using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private float xDirection;
    [SerializeField] LayerMask wallMask; // mask so vision ignores walls
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float visionRange = 20f;  //how far they can see
    [SerializeField] private float AttackVisionRange = 2f;
    [SerializeField][Range(0f, 359f)] private float fieldOfView = 90f; // Cone field of view angle
    [SerializeField] private float chaseTime = 10f;   //how long you want them to flee for

    private enum State
    {
        Idle, Walk,Pursuit,
        Attack, Damaged
    }

    private State currentState;
    private Transform target; // will be the prey when in sight
    private Rigidbody rb;
   



    void Start()
    {

        currentState = State.Idle; // start in idle
        rb = GetComponent<Rigidbody>(); //rigidbody for movement

        rb.velocity = new Vector3(0, 0, 0); //initial movement
       // StartCoroutine(RandomDirection()); // starts a couroutine for what to do when in idle

    }

    void Update()
    {

      
        Debug.Log("Current State of" + gameObject.name + " is : " + currentState);
        switch (currentState)
        {
            case State.Idle:
                IdleState();
                break;
            case State.Walk:
                walk();
                WalkState();
                break;
            case State.Pursuit:
                PursuitState();
                break;
            case State.Attack:
                AttackState();
                break;
            case State.Damaged:
                //call Damage
                break;

        }

        if (rb.velocity.x > 0)
        {

            transform.rotation = Quaternion.LookRotation(Vector3.right);
        }
        else if (rb.velocity.x < 0)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.left);
        }

    }


    
   
    void IdleState()
    {
        rb.velocity = new Vector3(0, 0, 0);
    }


    void walk()
    {
        rb.velocity = new Vector3(xDirection*moveSpeed, 0, 0);
    }


    void WalkState()
    {
        // Check for prey in the cone of vision
        Collider[] colliders = Physics.OverlapSphere(transform.position, visionRange);
        foreach (var collider in colliders)
        {
            Vector3 targetPosition = collider.transform.position;
            Vector3 directionToTarget = targetPosition - transform.position;
            float angle = Vector3.Angle(transform.forward, directionToTarget);

            if (angle < fieldOfView * 0.5f)
            {
                if (collider.CompareTag("Player") && Vector3.Distance(transform.position, collider.transform.position) < visionRange)
                {

                    float distancetoTarget = Vector3.Distance(transform.position, targetPosition);
                    if (!Physics.Raycast(transform.position, directionToTarget, distancetoTarget, wallMask))
                    {

                        currentState = State.Attack;
                        target = collider.transform;
                       // StartCoroutine(Chasing(target));
                        break;

                    }
                }
            }
        }
    }


    // pursues the prey while in the Pursuit State
    void PursuitState()
    {
        Vector3 chaseDirection = -(transform.position - target.position);
        Vector3 movement = chaseDirection.normalized * moveSpeed;
        rb.velocity = movement;
    }




    // for random movement while in idle
    float randomValue()
    {
        float r = 0;
        while (r == 0)
            r = Random.Range(-10f, 10f);

        return r;
    }

    // will stay in pursuit state for a certain amount of time
    IEnumerator Chasing(Transform target)
    {
        currentState = State.Attack;
        yield return new WaitForSeconds(chaseTime);
        currentState = State.Idle;
    }

    


    

    private void AttackState()
    {
        //Attack with Collider
        //or shoot projectile
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && currentState == State.Attack)
        {
            Swan swan = collision.gameObject.GetComponent<Swan>();
            swan.hit();
        }
        
    }


    public void BeginWalk()
    {
        currentState = State.Walk;
        Vector3 move = new Vector3(xDirection, 0, 0);
        move = Vector3.Normalize(move);
        rb.velocity = moveSpeed * move;

    }
}
