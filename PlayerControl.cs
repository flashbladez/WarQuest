using UnityEngine;
using System.Collections;
using WarQuest.CameraUI;

namespace WarQuest.Characters
{
    public class PlayerControl : MonoBehaviour
    {

        SpecialAbilities abilities;
        Character character;
        WeaponSystem weaponSystem;
        Animator animator;
        HealthSystem healthSystem;

        void Start()
        {
            weaponSystem = GetComponent<WeaponSystem>();
            character = GetComponent<Character>();
            abilities = GetComponent<SpecialAbilities>();
            animator = GetComponent<Animator>();
            healthSystem = GetComponent<HealthSystem>();
            RegisterForMouseEvents();
        }

        void Update()
        {
            if (AnimatorIsPlaying("Grounded") && healthSystem.CurrentHealthPoints < healthSystem.MaxHealthpoints)
            {
                healthSystem.AutoRegenerateHealth();
            }

            if (Input.GetKeyDown(KeyCode.U))
            {
                GetComponent<PlayerStats>().DisplayStats();
            }
            ScanForAbilityKeyDown();
            // KeyBoardControl();
        }

        bool AnimatorIsPlaying(string stateName)
        {
            return animator.GetCurrentAnimatorStateInfo(0).IsName(stateName);
        }

        void ScanForAbilityKeyDown()
        {
            for (int keyIndex = 0; keyIndex < abilities.GetNumberOfAbilities(); keyIndex++)
            {
                if (Input.GetKeyDown(keyIndex.ToString()))
                {
                    abilities.AttemptSpecialAbility(keyIndex);
                }
            }
        }

        void OnMouseOverPotentiallyWalkable(Vector3 destination)
        {
            if (Input.GetMouseButtonDown(0))
            {
                weaponSystem.StopAttacking();
                character.SetDestination(destination);
            }
        }

        bool IsTargetInRange(GameObject target)
        {

            if (target && target.GetComponent<HealthSystem>().healthAsPercentage > Mathf.Epsilon)
            {
                float distanceToTarget = (target.transform.position - transform.position).magnitude;
                return distanceToTarget <= weaponSystem.UpdateCurrentWeapon.GetMaxAttackRange();
            }
            return false;
        }

        void OnMouseOverEnemy(EnemyAI enemy)
        {

            if (Input.GetMouseButton(0) && IsTargetInRange(enemy.gameObject))
            {
                //    print("In Range");
                weaponSystem.AttackTarget(enemy.gameObject);
            }
            else if (Input.GetMouseButton(0) && !IsTargetInRange(enemy.gameObject))
            {
                //   print("move and attack");
                StartCoroutine(MoveAndAttack(enemy));
            }
        }

        void RegisterForMouseEvents()
        {
            CameraRaycaster cameraRaycaster = FindObjectOfType<CameraRaycaster>();
            cameraRaycaster.onMouseOverEnemy += OnMouseOverEnemy;
            cameraRaycaster.onMouseOverPotentiallyWalkable += OnMouseOverPotentiallyWalkable;
        }



        public void TakeDamage(float damage)
        {

        }

        IEnumerator MoveToTarget(GameObject target)
        {
            character.SetDestination(target.transform.position);
            while (!IsTargetInRange(target))
            {
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForEndOfFrame();
        }

        IEnumerator MoveAndAttack(EnemyAI enemy)
        {
            yield return StartCoroutine(MoveToTarget(enemy.gameObject));
            weaponSystem.AttackTarget(enemy.gameObject);
        }
    }
}
