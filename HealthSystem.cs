using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using WarQuest.CameraUI;

namespace WarQuest.Characters
{
    public class HealthSystem : MonoBehaviour
    {

        [SerializeField] float maxHealthPoints = 100f;//todo only set this here for enemy and npcs etc not player, player stats will set it
        [SerializeField] AudioClip[] damageSounds;
        [SerializeField] AudioClip[] deathSounds;
        [SerializeField] float deathVanishSeconds = 1.5f;
        [SerializeField] float healthPointsToRegenPerSecond = 0f;
        [SerializeField] float armour;    //todo not set for player here player stats will set it
        [SerializeField] Text healthDisplayText = null;
        [SerializeField] Image healthBar = null;
        [SerializeField] GameObject lootTable = null;

        string updateHealthBar = "UpdateHealthBar";
        string hpBar = "Game Canvas/Health Bar";
        string hpText = "Game Canvas/Hp Text";
        string zoneDetect = "Zone Detect";
        float currentHealthPoints = 0;
        Animator animator = null;
        AudioSource audioSource = null;
        Character characterMovement;
       // PlayerStats playerStats;
       // NPCAndEnemyStats npcAndEnemyStats;

        const string DEATH_TRIGGER = "Death";

        public float HealthAsPercentage
        {
            get { return CurrentHealthPoints / MaxHealthpoints; }
        }

        public float CurrentHealthPoints
        {
            get { return currentHealthPoints; }
            set { currentHealthPoints = value; }
        }

        public float MaxHealthpoints
        {
            get { return maxHealthPoints; }
            set { maxHealthPoints = value;
                CurrentHealthPoints = MaxHealthpoints;
                UpdateHealthBar(); }
        }

        public float HealthPointsToRegenPerSecond
        {
            get { return healthPointsToRegenPerSecond; }
            set { healthPointsToRegenPerSecond = value; }
        }

        public float Armour
        {
            get { return armour; }
            set { armour = value; }
        }

        void Start()
        {
          //  npcAndEnemyStats = GetComponent<NPCAndEnemyStats>();
          //  playerStats = GetComponent<PlayerStats>();
            animator = GetComponent<Animator>();
            audioSource = GetComponent<AudioSource>();
            characterMovement = GetComponent<Character>();

            CurrentHealthPoints = MaxHealthpoints;
            UpdateHealthBar();
        }

        void Update()
        {
            if (CurrentHealthPoints < MaxHealthpoints)
            {
                AutoRegenerateHealth();
            }
        }

        public void SethealthBarText()
        {
            if (GetComponent<PlayerControl>())
            {
                var HB = GameObject.Find(hpBar);
                var HT = GameObject.Find(hpText);
                healthBar = HB.GetComponent<Image>();
                healthDisplayText = HT.GetComponent<Text>();
            }
        }

        void UpdateHealthBar()
        {
            if (healthBar)
            {
                if (CurrentHealthPoints > MaxHealthpoints)
                {
                    CurrentHealthPoints = MaxHealthpoints;
                }
                if (gameObject.GetComponent<PlayerControl>())
                {
                    healthDisplayText.text = (CurrentHealthPoints.ToString() + "/" + MaxHealthpoints.ToString());
                }
                healthBar.fillAmount = HealthAsPercentage;
            }
        }

        public void TakeDamage(float damage)
        {
            var thisDamage = damage;

            if (GetComponent<PlayerControl>())
            {
                thisDamage = Mathf.Round(damage - (armour / 4));//This will maybe need to be a percentage including enemy level in formula
            }
            CurrentHealthPoints = Mathf.Clamp(CurrentHealthPoints - thisDamage, 0f, MaxHealthpoints);
            UpdateHealthBar();
            //Todo get this sound from weapon config so each weapon can have a different sound

            var clip = damageSounds[UnityEngine.Random.Range(0, damageSounds.Length)];
            audioSource.PlayOneShot(clip);
            bool characterDies = (CurrentHealthPoints <= 0);
            if (characterDies)
            {
                StartCoroutine(KillCharacter());
            }
        }

        public void Heal(float points)
        {
            CurrentHealthPoints = Mathf.Clamp(CurrentHealthPoints + points, 0f, MaxHealthpoints);
            UpdateHealthBar();
        }

        //regenerate amount of health over time
        public void AutoRegenerateHealth()
        {
            //todo need to check if character is in combat
            if (!IsInvoking(updateHealthBar) && CurrentHealthPoints < MaxHealthpoints)
            {
                // var pointsToAddPerSecond = healthPointsToRegenPerSecond;
                CurrentHealthPoints = Mathf.Clamp(CurrentHealthPoints + HealthPointsToRegenPerSecond, 0, MaxHealthpoints);
                Invoke(updateHealthBar, 1f);
            }
            else if (CurrentHealthPoints >= MaxHealthpoints)
            {
                CancelInvoke();
            }
        }

        IEnumerator KillCharacter()
        {
            characterMovement.Kill();
            animator.SetTrigger(DEATH_TRIGGER);
            var playerComponent = GetComponent<PlayerControl>();
            var player = FindObjectOfType<PlayerXP>();

            audioSource.clip = deathSounds[UnityEngine.Random.Range(0, deathSounds.Length)];
            audioSource.Play();

            if (playerComponent && playerComponent.isActiveAndEnabled)
            {
                // GetComponent<PlayerControl>().DeRegister();
                yield return new WaitForSecondsRealtime(deathVanishSeconds);

                var audioSource = GameObject.FindGameObjectsWithTag(zoneDetect);
                foreach (GameObject go in audioSource)
                {
                    var audio = go.GetComponent<AudioSource>();
                    audio.Stop();
                }

                Destroy(gameObject, deathVanishSeconds);
            }
            else
            {
                if (GetComponent<EnemyAI>())
                {
                    var xpToAward = GetComponent<EnemyAI>().XpValue();
                    player.XpToBeAwarded(xpToAward);

                    var loot = Instantiate(lootTable, transform);
                    loot.transform.position = GetComponent<EnemyAI>().transform.TransformPoint(Vector3.right * 2);
                    loot.transform.parent = null;
                }

                yield return new WaitForSecondsRealtime(deathVanishSeconds);
                Destroy(gameObject, deathVanishSeconds);
            }
            // StopAllCoroutines();
        }
    }
}
