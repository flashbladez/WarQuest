using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using WarQuest.CameraUI;

namespace WarQuest.Characters
{
    [SelectionBase]

    public class Character : MonoBehaviour
    {
        [Header("Nav Mesh Settings")]
        [SerializeField] float navBaseOffset = -0.07f;
        [SerializeField] float navSpeed = 6f;
        [SerializeField] float navRadius = 0.1f;
        [SerializeField] float navHeight = 2f;
        [SerializeField] float navStoppingDistance = 3f;
        [SerializeField] bool navAutoBraking = false;

        [Header("Animator Settings")]
        [SerializeField] RuntimeAnimatorController animatorController;
        [SerializeField] AnimatorOverrideController animatorOverideController;
        [SerializeField] Avatar characterAvatar;
        [SerializeField] [Range(0.1f, 1f)] float animatorForwardCap = 1f;

        [Header("Movement Properties")]
        [SerializeField] float stoppingDistance = 1f;
        [SerializeField] float moveSpeedMultiplier = 1f;
        [SerializeField] float animationSpeedMultiplier = 1f;
        [SerializeField] float movingTurnSpeed = 360;
        [SerializeField] float stationaryTurnSpeed = 180;
        [SerializeField] float moveThreshold = 1f;

        [Header("Audio settings")]
        [SerializeField] float audioSourceSpatialBlend = 0.5f;

        CameraRaycaster cameraRaycaster;
        NavMeshAgent navMeshAgent;
        Animator animator;
        Rigidbody rigidBody;

        float turnAmount;
        bool isAlive = true;

        float forwardAmount;
        bool keyBoardControl = false;

        void Awake()
        {
            AddRequiredComponents();
        }

        void AddRequiredComponents()
        {
            navMeshAgent = gameObject.AddComponent<NavMeshAgent>();
            navMeshAgent.radius = navRadius;
            navMeshAgent.height = navHeight;
            navMeshAgent.stoppingDistance = navStoppingDistance;
            navMeshAgent.baseOffset = navBaseOffset;
            navMeshAgent.speed = navSpeed;
            navMeshAgent.updatePosition = true;
            navMeshAgent.updateRotation = false;
            navMeshAgent.autoBraking = navAutoBraking;

            rigidBody = gameObject.AddComponent<Rigidbody>();
            rigidBody.constraints = RigidbodyConstraints.FreezeRotation;
            rigidBody.drag = 10f;

            animator = gameObject.AddComponent<Animator>();
            animator.runtimeAnimatorController = animatorController;
            animator.avatar = characterAvatar;

            var audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.spatialBlend = audioSourceSpatialBlend;
            audioSource.playOnAwake = false;
            audioSource.maxDistance = 200;
        }

        void Update()
        {
            if (navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance && isAlive)
            {
                Move(navMeshAgent.desiredVelocity);
            }
            else
            {
                // navMeshAgent.velocity = Vector3.zero;
                Move(Vector3.zero);
            }
            if (Input.GetKeyDown(KeyCode.F9))
            {
                keyBoardControl = !keyBoardControl;
            }
        }

        public AnimatorOverrideController GetOverrideController()
        {
            return animatorOverideController;
        }

        public void Kill()
        {
            isAlive = false;
        }

        public float GetAnimationSpeedMultiplier()
        {
            return animationSpeedMultiplier;
        }

        public void OnAnimatorMove()
        {
            //Note makes camera shake
            //   if(Time.deltaTime > 0)
            //   {
            //  Vector3 velocity = (animator.deltaPosition * moveSpeedMultiplier) / Time.deltaTime;
            // velocity.y = rigidBody.velocity.y;
            // rigidBody.velocity = velocity;
            //}
        }
        public float SetForwardAmount
        {
            set
            {
                forwardAmount = value;
            }
        }

        public void SetDestination(Vector3 worldPos)
        {
            if (isAlive)
            {
                navMeshAgent.destination = worldPos;
            }
        }

        public void Move(Vector3 movement)
        {
            SetForwardAndTurn(movement);
            ApplyExtraTurnRotation();
            UpdateAnimator();
        }

        void SetForwardAndTurn(Vector3 movement)
        {
            // convert the world relative moveInput vector into a local-relative
            // turn amount and forward amount required to head in the desired direction.
           
            if (movement.magnitude > moveThreshold)
            {
                movement.Normalize();
            }
            var localMove = transform.InverseTransformDirection(movement);
            turnAmount = Mathf.Atan2(localMove.x, localMove.z);
            forwardAmount = localMove.z;
        }

          
        void UpdateAnimator()
        {
            // update the animator parameters
                animator.SetFloat("Forward", forwardAmount * animatorForwardCap, 0.1f, Time.deltaTime);
                animator.SetFloat("Turn", turnAmount, 0.1f, Time.deltaTime);
                animator.speed = animationSpeedMultiplier;
        }

        void ApplyExtraTurnRotation()
        {
            // help the character turn faster (this is in addition to root rotation in the animation)
            float turnSpeed = Mathf.Lerp(stationaryTurnSpeed, movingTurnSpeed, forwardAmount);
            transform.Rotate(0, turnAmount * turnSpeed * Time.deltaTime, 0);
        }

        public void SetAnimatorForwardCap(float setForwardCap)
        {
            animatorForwardCap = setForwardCap;
        }
    }
}






