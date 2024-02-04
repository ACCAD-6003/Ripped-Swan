using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private float prepTime;
    [SerializeField] private float xDirection;
    [SerializeField] LayerMask wallMask; // mask so vision ignores walls
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float visionRange = 20f;  //how far they can see
    [SerializeField] private float AttackVisionRange = 2f;
    [SerializeField][Range(0f, 359f)] private float fieldOfView = 90f; // Cone field of view angle
    [SerializeField] private float chaseTime = 10f;   //how long you want them to flee for

    private enum State
    {
        Idle, Walk,Pursuit,Cooldown,
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
                Scout();
                break;
            case State.Walk:
                walk();
                Scout();
                break;
            case State.Pursuit:
                PursuitState();
                AttackRange();
                break;
            case State.Attack:
                
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


    void Scout()
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

                        currentState = State.Pursuit;
                        target = collider.transform;
                        StartCoroutine(Chasing(target));
                        break;

                    }
                }
            }
        }
    }



    void AttackRange()
    {
        Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, transform.localScale / 2, Quaternion.identity);
        int i = 0;
        //Check when there is a new collider coming into contact with the box
        while (i < hitColliders.Length)
        {
            //Output all of the collider names
            Debug.Log("Hit : " + hitColliders[i].name + i);
            if (hitColliders[i].gameObject.CompareTag("Player")){
                currentState = State.Idle;
                StartCoroutine(Attack());

            }
            //Increase the number of Colliders in the array
            i++;
        }
    }






    // pursues the prey while in the Pursuit State
    void PursuitState()
    {
        Vector3 chaseDirection = -(transform.position - target.position);
        Vector3 movement = chaseDirection.normalized * moveSpeed;
        movement.y = 0f;
        rb.velocity = movement;
    }




   

    // will stay in pursuit state for a certain amount of time
    IEnumerator Chasing(Transform target)
    {
        currentState = State.Pursuit;
        yield return new WaitForSeconds(chaseTime);
        currentState = State.Idle;
    }



    IEnumerator Attack()
    {
      
        yield return new WaitForSeconds(prepTime);
        
        currentState = State.Attack;
        BoxCollider myBC = gameObject.GetComponent<BoxCollider>();
        myBC.enabled = true;
        gameObject.transform.GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSeconds(.5f);
        gameObject.transform.GetChild(1).gameObject.SetActive(false);
        myBC.enabled = false;
        currentState = State.Cooldown;
        yield return new WaitForSeconds(.5f);
        currentState = State.Idle;
        
    }

    





    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && currentState== State.Attack)
        {
            Swan swan = other.gameObject.GetComponent<Swan>();
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
        Gizmos.DrawWireSphere(transform.position, visionRange);
        Gizmos.DrawWireCube(transform.position +transform.forward*.7f, transform.localScale);
       
    }
}
