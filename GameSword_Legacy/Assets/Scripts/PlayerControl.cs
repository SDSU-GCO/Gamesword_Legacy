using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS
{
    public class PlayerControl : MonoBehaviour
    {
        public float moveSpeed = 10f;
        public float jumpForce;
        bool isGrounded;
        Vector3 groundNormal;
        Rigidbody _rigidbody;
        public float groundCheckRadius = 0.5f;
        public LayerMask groundMask;
        public LayerMask targetMask;
        public float slopeLimit;
        public Transform _camera;
        private Vector3 _camForward;             // The current forward direction of the camera
        private UnityStandardAssets.Cameras.AutoCam _camControl; // AutoCam script controlling the player's camera
        public float rotationSpeed = 20f;
        Transform lockTarget; // Target that the player is currently locked on to
        bool lockedOn = false; // Locked on to a target
        public float lockOnDistance;
        Collider[] targets;

        GameSword _sword;

        void Start()
        {
            _sword = GetComponentInChildren<GameSword>();
            _rigidbody = GetComponent<Rigidbody>();
            _camControl = _camera.gameObject.GetComponent<UnityStandardAssets.Cameras.AutoCam>();
            lockTarget = null;
        }

        void FixedUpdate()
        {
            if (Input.GetButtonDown("Lock Camera"))
            {
                if (lockedOn)
                {
                    lockTarget = null;
                    _camControl.lockedOn = lockedOn = false;
                }
                else
                    getTargets();
            }

            Vector3 move = Vector3.zero;
            checkGrounded();
            if (isGrounded)
            {
                _camForward = _camera.transform.TransformDirection(Vector3.forward);
                _camForward.y = 0;
                _camForward = _camForward.normalized; // Forward direction of the camera
                Vector3 right = new Vector3(_camForward.z, 0, -_camForward.x); // x axis of camera
                float h = Input.GetAxis("Horizontal");
                float v = Input.GetAxis("Vertical");

                move = (h * right + v * _camForward); // Moves relative to camera
                move *= moveSpeed;

                _rigidbody.velocity = Vector3.ProjectOnPlane(move, groundNormal); // Takes slope into account
                if (Input.GetButtonDown("Jump"))
                {
                    _rigidbody.AddForce(Vector3.up * jumpForce);
                }

            }
            if (lockedOn)  // Locked rotation
            {
                if (Vector3.Distance(transform.position, lockTarget.transform.position) > lockOnDistance)
                {
                    lockTarget = null;
                    lockedOn = false;
                }
                transform.LookAt(lockTarget);
            }
            else if (move != Vector3.zero) // Normal r
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(move), Time.deltaTime * rotationSpeed);
            }

        }
        void checkGrounded()
        {
            RaycastHit hitInfo;
            Physics.Raycast(transform.position, Vector3.down, out hitInfo, 2f, groundMask);
            groundNormal = hitInfo.normal;
            float slope = 180f - Vector3.Angle(groundNormal, -Vector3.up);
            if (slope <= slopeLimit && Physics.CheckCapsule(transform.position, new Vector3(transform.position.x, transform.position.y - 0.55f, transform.position.z), groundCheckRadius, groundMask))
            {
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
                groundNormal = Vector3.up;
            }
        }
        void getTargets()
        {
            targets = Physics.OverlapSphere(transform.position, lockOnDistance, targetMask);
            int i = 0;
            while (i < targets.Length && lockTarget == null)
            {
                if (targets[i].transform.GetComponent<Renderer>().isVisible)
                {
                    lockTarget = targets[i].transform;
                    lockedOn = true;
                }
                i++;
                _camControl.lockedOn = lockedOn;
            }

        }
    }
}