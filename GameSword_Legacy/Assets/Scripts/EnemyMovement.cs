using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Basic class for following a target to within a certain distance
// Specifies both hearing and sight range (hearing is currently just based on distance).
namespace GS
{
    public class EnemyMovement : MonoBehaviour
    {
        NavMeshAgent agent;
        // Finding/Following
        public float sightRange; // Range at which target will be sighted within sight cone
        public float fov; // Field of view
        public float hearRange; // Range at which target will be detected regardless of sight cone
        public float followRange; // Range at which to stop following target
        public float targetDistance; // Distance from the target to move to
        protected float currentDistance; // Current distance from target
        protected bool sighted = false;
        protected bool heard = false;
        protected Vector3 targetLocation; // Location to move to
        public LayerMask targetMask;
        public LayerMask wallMask;
        // Patrolling
        public Transform[] waypoints;
        private int destination;

        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.autoBraking = false;
            destination = 0;
        }

        void Update()
        {
            findTarget();
            moveToTarget();
        }
        
        protected virtual void findTarget()
        {
            float range = sightRange > hearRange ? sightRange : hearRange; // Uses the greater of the two values
            Collider [] colliders = Physics.OverlapSphere(transform.position, range, targetMask); // Only selects objects with target tags

            if (colliders.Length > 0)
            {
                currentDistance = Vector3.Distance(colliders[0].transform.position, transform.position);
                // Hearing check
                heard = (currentDistance < hearRange);
                // Sight check
                if (Physics.Linecast(transform.position, colliders[0].transform.position, wallMask))
                {
                    sighted = false;
                    return;
                }
                else
                {
                    float angle = Vector3.Angle(colliders[0].transform.position - transform.position, transform.forward);
                    sighted = (angle < fov / 2);
                }
            }
            if (sighted || heard)
            {
                targetLocation = colliders[0].transform.position;
                Debug.Log("spotted");
            }
        }

        protected virtual void moveToTarget()
        {
            if ((sighted || heard) && currentDistance > targetDistance && currentDistance < followRange)
            {
                agent.SetDestination(targetLocation);
            }
            else if (waypoints.Length > 0 && !(sighted || heard))
                nextWaypoint();
            else
                agent.SetDestination(transform.position);
        }
        protected virtual void nextWaypoint()
        {
            Debug.Log("patrolling");
            agent.SetDestination(waypoints[destination].position);
            destination = (destination + 1) % waypoints.Length;
        }
    }
}
