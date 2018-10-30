using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WarQuest.Characters
{
    [RequireComponent(typeof(HealthSystem))]
    [RequireComponent(typeof(Character))]
    [RequireComponent(typeof(WeaponSystem))]

    public class EnemyAI : MonoBehaviour
    {

        [SerializeField] float chaseRadius = 6f;
        [SerializeField] WayPointContainer patrolPath;
        [SerializeField] float timeToWaitAtWayPoint = 5.0f;
        [SerializeField] float wayPointTolerance = 1.0f;
        [SerializeField] int xpValue = 0;
     
        int startingWaypoint;
        GameObject target;
        WeaponSystem weaponSystem;
        Character character;
        float currentWeaponRange;
        float distanceToEnemy;
        int nextWaypointIndex;
        int xPToAward = 0;
        bool isInWeaponCircle = false;
        bool isInChaseCircle = false;
        bool isOutsideChaseCircle = true;
    
        void Start()
        {
            character = GetComponent<Character>();
            xPToAward = xpValue;
        }

        public float XpValue()
        {
            return xPToAward;
        }

        public void SetPatrolPath(WayPointContainer setPatrolPath, int startingWayPoint)
        {
            this.startingWaypoint = startingWayPoint;
            patrolPath = setPatrolPath;
            nextWaypointIndex = startingWaypoint;
        }

        private void OnDrawGizmos()
        {
           // Gizmos.color = Color.white;

          //  Gizmos.DrawSphere(transform.position, distanceToEnemy);
        }

        void Update()
        {
           // print(target);
            weaponSystem = GetComponent<WeaponSystem>();
            currentWeaponRange = weaponSystem.UpdateCurrentWeapon.GetMaxAttackRange();

            if (target != null && target.gameObject.tag != ("Animal"))
            {
                distanceToEnemy = Vector3.Distance(target.transform.position, transform.position);
                isInWeaponCircle = distanceToEnemy <= currentWeaponRange;
                isInChaseCircle = (distanceToEnemy > currentWeaponRange && distanceToEnemy <= chaseRadius);
                isOutsideChaseCircle = distanceToEnemy > chaseRadius;
                // print(distanceToEnemy + "  DTE  " + currentWeaponRange + "  CWR  " + chaseRadius + "  CR  " + target.name + "  iicc  "+ isInChaseCircle);
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
            else if(!isInWeaponCircle && !isInChaseCircle )
            {
                float animatorForwardCap = 0.5f;
                character.SetAnimatorForwardCap(animatorForwardCap);
            }

            if(isInWeaponCircle)
            {
                if(weaponSystem)
                {
                    weaponSystem.AttackTarget(target.gameObject);
                }
                StartCoroutine(PatrolWaitWhenAttacking());
            }

        }
        void OnTriggerExit(Collider target)
        {
          
        }

        void OnTriggerEnter(Collider target)
        {
            if (target.GetComponent<NPCAI>())
            {
                this.target = target.gameObject;
                xPToAward = 0;
            }
            else if (target.gameObject.GetComponent<PlayerControl>())
            {

                this.target = target.gameObject;
                xPToAward = xpValue;
            }
        }

        IEnumerator ChaseTarget()
        {
            while (target && distanceToEnemy > currentWeaponRange)
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
                character.SetDestination(nextWaypointPos);
                yield return new WaitForSeconds(patrolPath.transform.GetChild(nextWaypointIndex).GetComponentInChildren<WayPointWait>().TimeToWait);
                CycleWayPointWhenClose(nextWaypointPos);
            }
        }

       IEnumerator PatrolWaitWhenAttacking()
       {
            //Todo put a 20s delay in to wait before he goes back to patrolling while hes watching you run away
            while (isInWeaponCircle) {
                character.SetDestination(target.transform.position);
                yield return new WaitForSeconds(patrolPath.transform.GetChild(nextWaypointIndex).GetComponentInChildren<WayPointWait>().TimeToWait);
                StopAllCoroutines();
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
