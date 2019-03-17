using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;


namespace WarQuest.Characters
{
    public class WeaponSystem : MonoBehaviour
    {

        [SerializeField] WeaponConfig currentWeaponConfig;
        float baseDamage = 0f;
        PlayerStats playerStats;
        GameObject target;
        GameObject weaponObject;
        Animator animator;
        Character character;
        float lastHitTime = 0f;
               
        bool attackerStillAlive;
        bool targetStillAlive;

        const string ATTACK_TRIGGER = "Attack";
        const string DEFAULT_ATTACK = "DEFAULT_ATTACK";

        void Start()
        {
            playerStats = GetComponent<PlayerStats>();
            character = GetComponent<Character>();
            animator = GetComponent<Animator>();
            //UpdateCurrentWeapon = playerStats.ArmourEquippedConfig.WeaponSlotEquipped;
           
            SetAttackAnimation();
            PutWeaponInHand(currentWeaponConfig);
        }

        public float BaseDamage {
            get{ return baseDamage; }
            set{ baseDamage = value; }
        }

        public WeaponConfig UpdateCurrentWeapon
        {
            get{ return currentWeaponConfig; }
            set{ currentWeaponConfig = value;}
        }

        void Update()
        {
            bool targetIsDead;
            bool targetIsOutOfRange;

            if(target == null)
            {
                targetIsDead = false;
                targetIsOutOfRange = false;
            }
            else
            {
                var targetHealth = target.GetComponent<HealthSystem>().HealthAsPercentage;
                targetIsDead = targetHealth <= Mathf.Epsilon;

                var distanceTotarget = Vector3.Distance(transform.position, target.transform.position);
                targetIsOutOfRange = distanceTotarget > UpdateCurrentWeapon.GetMaxAttackRange();

            }

            float characterHealth = GetComponent<HealthSystem>().HealthAsPercentage;
            bool characterIsDead = (characterHealth <= Mathf.Epsilon);

            if(characterIsDead || targetIsOutOfRange || targetIsDead)
            {
                StopAllCoroutines();
            }
          
        }

        public void PutWeaponInHand(WeaponConfig weaponToUse)
        {
           
            if (GetComponent<PlayerStats>()) {

                playerStats.WeaponUpdate(weaponToUse);//let playerStats.WeaponUpdate know about new weapon
            }
            else
            {
                UpdateCurrentWeapon = weaponToUse;
            }
                                                                  
            var weaponPrefab = weaponToUse.GetWeaponPrefab();
            GameObject dominantHand = RequestDominantHand();
            if (weaponObject)
            {
                Destroy(weaponObject);
            }
            weaponObject = Instantiate(weaponPrefab, dominantHand.transform);
            weaponObject.transform.localPosition = UpdateCurrentWeapon.gripTransform.localPosition;
            weaponObject.transform.localRotation = UpdateCurrentWeapon.gripTransform.localRotation;
        
        }


        void SetAttackAnimation()
        {
            if (!character.GetOverrideController())
            {
                Debug.Break();
                Debug.Log("Need an animator override controller");
            }
            else
            {
                animator = GetComponent<Animator>();
                animator.runtimeAnimatorController = character.GetOverrideController();
                character.GetOverrideController()[DEFAULT_ATTACK] = UpdateCurrentWeapon.GetAttackAnimClip();
            }
        }

        public void AttackTarget(GameObject targetToAttack)
        {
            target = targetToAttack;

            StartCoroutine(AttackTargetRepeatedly());
        }

        IEnumerator AttackTargetRepeatedly()
        {
            if (target)
            {
               attackerStillAlive = GetComponent<HealthSystem>().HealthAsPercentage >= Mathf.Epsilon;
               targetStillAlive = target.GetComponent<HealthSystem>().HealthAsPercentage >= Mathf.Epsilon;
            }

            while(attackerStillAlive && targetStillAlive)
            {
                var animationClip = UpdateCurrentWeapon.GetAttackAnimClip();
              
                float animationClipTime = animationClip.length / character.GetAnimationSpeedMultiplier();
                float timeToWait = animationClipTime + UpdateCurrentWeapon.GetTimeBetweenAnimationCycles();
              
                bool isTimeToHitAgain = Time.time - lastHitTime > timeToWait;

                if (isTimeToHitAgain)
                {
                    AttackTargetOnce();
                    lastHitTime = Time.time;
                }

                yield return new WaitForSeconds(timeToWait);
            }         
        }

        void AttackTargetOnce()
        {
            if (target != null) {
                transform.LookAt(target.transform);
                animator.SetTrigger(ATTACK_TRIGGER);
                float damageDelay = UpdateCurrentWeapon.GetDamageDelay();
                SetAttackAnimation();
                StartCoroutine(DamageAfterDelay(damageDelay));
            }
        }

        IEnumerator DamageAfterDelay(float damageDelay)
        {
            target.GetComponent<HealthSystem>().TakeDamage(BaseDamage);
            yield return new WaitForSecondsRealtime(damageDelay);
        }

        GameObject RequestDominantHand()
        {
            var dominantHands = GetComponentsInChildren<DominantHand>();
            int numberOfDominantHands = dominantHands.Length;
            Assert.IsFalse(numberOfDominantHands <= 0, "No DominantHand found on player please add one.");
            Assert.IsFalse(numberOfDominantHands > 1, "Multiple DominantHands found on player");
            return dominantHands[0].gameObject;
        }


        public void StopAttacking()
        {
            if (animator)
            {
                animator.StopPlayback();
                StopAllCoroutines();
            }
        }

       
     //   float CalculateDamage()
       // {
              //todo maybe consider the multplier to be based on level sounds better idea
         //     return baseDamage;
       //  }
    }
}
