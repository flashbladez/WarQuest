using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WarQuest.Characters
{
    [RequireComponent(typeof(HealthSystem))]
    [RequireComponent(typeof(Character))]
    [RequireComponent(typeof(WeaponSystem))]
    public class NPCAI : MonoBehaviour
    {
      
        [SerializeField] float chaseRadius = 6f;
        [SerializeField] WayPointContainer patrolPath;
        [SerializeField] float timeToWaitAtWayPoint = 2.0f;
        [SerializeField] float wayPointTolerance = 1.0f;
        [SerializeField] float damageToInflict;

        int startingWaypoint = 0;
        GameObject target;
        WeaponSystem weaponSystem;
        Character character;
        float currentWeaponRange;
        float distanceToCharacter;
        float distanceToEnemy;
        float timeLeft;
        int nextWaypointIndex;
        bool isInWeaponCircle = false;
        bool isInChaseCircle = false;
        bool isOutsideChaseCircle = true;

    
        void Start()
        {
            character = GetComponent<Character>();
            GetComponent<WeaponSystem>().SetBaseDamage = damageToInflict;
        }

        public void SetPatrolPath(WayPointContainer setPatrolPath,int startingWayPoint)
        {
            this.startingWaypoint = startingWayPoint;
            patrolPath = setPatrolPath;
            nextWaypointIndex = startingWaypoint;
        }

        void Update()
        {         
            weaponSystem = GetComponent<WeaponSystem>();
            currentWeaponRange = weaponSystem.UpdateCurrentWeapon.GetMaxAttackRange();
       
            if (target != null) {
                
                distanceToEnemy = Vector3.Distance(target.transform.position, transform.position);
                isInWeaponCircle = distanceToEnemy <= currentWeaponRange;
                isInChaseCircle = (distanceToEnemy > currentWeaponRange && distanceToEnemy <= chaseRadius);
                isOutsideChaseCircle = distanceToEnemy > chaseRadius;
            }
            else if (target == null)
            {
                isOutsideChaseCircle = true;
                isInWeaponCircle = false;
                isInChaseCircle = false;
            }

            if (isOutsideChaseCircle)
            {
                weaponSystem.StopAttacking();
                StartCoroutine(Patrol());
            }

            if(isInChaseCircle)
            {
                StopAllCoroutines();
                float animatorForwardCap = 1f;
                character.SetAnimatorForwardCap(animatorForwardCap);
                weaponSystem.StopAttacking();
                StartCoroutine(ChaseTarget());
            }
            else if(!isInWeaponCircle && !isInChaseCircle)
            {
                float animatorForwardCap = 0.5f;
                character.SetAnimatorForwardCap(animatorForwardCap);
            }

            if(isInWeaponCircle)
            {
                //todo check target is still alive
                if (weaponSystem )
                {
                    weaponSystem.AttackTarget(target.gameObject);
                    StartCoroutine(PatrolWaitWhenAttacking());
                }
            }

        }

        void OnTriggerEnter(Collider target)
        {
             if (target.GetComponent<EnemyAI>())
             {
                 this.target = target.gameObject;
             }
        }

        void OnTriggerExit(Collider target)
        {
            
        }

        IEnumerator ChaseTarget()
        {
            while (distanceToEnemy > currentWeaponRange && target != null)
            {
                character.SetDestination(target.transform.position);
                yield return new WaitForEndOfFrame();
            }
        }

        IEnumerator Patrol()
        { 
            while (patrolPath != null)
            {
              
                    Vector3 nextWaypointPos = patrolPath.transform.GetChild(nextWaypointIndex).position;
              
                    character.SetDestination(nextWaypointPos);// trying to still get to this destination even though it is in attacking state
                
                if (GetComponent<WayPointAnimations>() && (Vector3.Distance(transform.position, nextWaypointPos) <= wayPointTolerance))
                {
                    GetComponent<WayPointAnimations>().SetIndex(nextWaypointIndex);            
                }
                    yield return new WaitForSeconds(timeToWaitAtWayPoint);
               
                    CycleWayPointWhenClose(nextWaypointPos);
            }
        }

       IEnumerator PatrolWaitWhenAttacking()
       {
            //Todo put a 20s delay in to wait before he goes back to patrolling while hes watching you run away
          
            while (isInWeaponCircle) {
                character.SetDestination(transform.position);
                yield return new WaitForSeconds(timeToWaitAtWayPoint);
            }
       }

        void CycleWayPointWhenClose(Vector3 nextWaypointPos)
        {
            StopAllCoroutines();
            if (Vector3.Distance(transform.position , nextWaypointPos) <= wayPointTolerance) {
       
                nextWaypointIndex = (nextWaypointIndex + 1) % patrolPath.transform.childCount;
            }
        }
    }
}
