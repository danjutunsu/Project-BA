using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
    public NPCStats npcStats;
    public Transform playerTransform;
    public float aggroDistance = 20.0f;
    public float stopDistance = 2.5f;
    public float slowDistance = 5.0f;
    public float wanderRadius = 10.0f;
    public float wanderTimer = 5.0f;

    public NPCAttack NPCAttack;
    public Target Target;

    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private float timer;
    private bool isWandering;


    public float aggroRange = 20.0f; // Range within which the NPC initializes aggro
    public string playerTag = "Player"; // Tag to identify the player

    private bool isAggroed = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
        isWandering = false;
    }
    
    private void Update()
    {
        // Calculate the distance between the NPC and the player
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (npcStats.currentHealth <= 0)
        {
            animator.StopPlayback();
            navMeshAgent.speed = 0;
            navMeshAgent.isStopped = true;
        }
        // If the player is within stopping distance, move towards the player
        else if (distanceToPlayer <= aggroDistance)
        {

            // Move the NPC towards the player
            navMeshAgent.SetDestination(playerTransform.position);

            // Check if the NPC is within slow distance
            if (distanceToPlayer > slowDistance)
            {
                //Debug.Log("Chasing");
                navMeshAgent.speed = 4.0f;

                StopLongAnimationOnDeath(animator, "AGIA_Idle_generic_01", "IsRunning", 0f);

                // Stop the NPC's movement and stop the walking animation
                navMeshAgent.isStopped = false;
                animator.SetFloat("speed", navMeshAgent.speed);
            }
            // Check if the NPC is within stop distance
            else if (distanceToPlayer <= slowDistance && distanceToPlayer >= stopDistance)
            {
                //Debug.Log("Walking");

                navMeshAgent.speed = 2.0f;

                // Stop the NPC's movement and stop the walking animation
                navMeshAgent.isStopped = false;
                animator.SetFloat("speed", 1.0f);
            }// Check if the NPC is within stop distance
            else if (distanceToPlayer <= stopDistance)
            {
                navMeshAgent.speed = 0f;

                //Debug.Log("Stopping");
                // Stop the NPC's movement and stop the walking animation
                navMeshAgent.isStopped = true;
                animator.SetFloat("speed", 0.0f);
                animator.SetBool("attacking", true);
            }
            else
            {
                // Resume the NPC's movement and play the walking animation
                navMeshAgent.isStopped = false;
                animator.SetFloat("speed", 1.0f);
                animator.SetFloat("speed", navMeshAgent.speed);
                isWandering = true;
            }
        }
        else
        {
            Debug.Log("Here");

            navMeshAgent.speed = 1.0f;
            animator.SetFloat("speed", 1.0f);

            if (!isWandering)
            {
                // Start wandering
                timer = wanderTimer;
                isWandering = true;
            }

            // If the NPC is not moving, start the walking animation
            if (navMeshAgent.velocity.magnitude == 0)
            {
                animator.SetFloat("speed", navMeshAgent.speed);
            }

            // Reduce the timer
            timer -= Time.deltaTime;

            // If the timer has expired, generate a new destination within the wanderRadius and move towards it
            if (timer <= 0)
            {
                Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                navMeshAgent.SetDestination(newPos);
                timer = wanderTimer;
            }

            // Resume the NPC's movement
            navMeshAgent.isStopped = false;

            isWandering = true;
        }

        // Check if the NPC is already aggroed
        if (isAggroed) return;

        // Check for player within aggro range
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, aggroRange);
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag(playerTag))
            {
                Target.setTarget(hitCollider.gameObject);
                //Debug.Log("Player Found");
                // Player found, initialize aggro
                InitializeAggro();
                break;
            }
        }
    }

    private void InitializeAggro()
    {
        // TODO: Initialize aggro behavior for the NPC
        isAggroed = true;
    }

    // Helper function to generate a random position within a sphere
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        return navHit.position;
    }

    public void StopLongAnimationOnDeath(Animator animator, string currentAnimation, string nextAnimation, float speed)
    {
        // Check if the long animation is currently playing
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(currentAnimation))
        {
            // Crossfade to a death animation state with zero transition time
            animator.CrossFade(nextAnimation, speed);
        }
    }
}
