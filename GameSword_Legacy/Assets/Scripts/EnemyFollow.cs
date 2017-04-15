using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace GS
{
    public class EnemyFollow : MonoBehaviour
    {
        public float sightRange; // Range at which target will be sighted within sight cone
        public float fov; // Field of view
        public float hearRange; // Range at which target will be detected regardless of sight cone
        public float followRange; // Range at which to stop following target
        public float targetDistance; // Distance from the target to move to
        protected float currentDistance; // Current distance from target
        protected bool sighted = false;
        protected bool heard = false;
        protected Vector3 targetLocation;
        public LayerMask targetMask;
        public LayerMask wallMask;
        NavMeshAgent agent;

        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
        }
        
        void Update()
        {
            findTarget();
            moveToTarget();
        }
        
        protected virtual void findTarget()
        {
            float range = sightRange > hearRange ? sightRange : hearRange; // Uses the greater of the two values
            Collider [] colliders = Physics.OverlapSphere(transform.position, range, targetMask);

            if (colliders.Length > 0)
            {
                currentDistance = Vector3.Distance(colliders[0].transform.position, transform.position);
                heard = (currentDistance < hearRange);
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
            }
        }

        protected virtual void moveToTarget()
        {
            Debug.Log(currentDistance);
            if ((sighted || heard) && currentDistance > targetDistance && currentDistance < followRange)
            {
                agent.SetDestination(targetLocation);
            }
            else
                agent.SetDestination(transform.position);
        }
    }
}
