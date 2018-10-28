using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

namespace WarQuest.Characters
{
    public class HealthSystem : MonoBehaviour
    {
      
        [SerializeField] float maxHealthPoints = 100f;
        [SerializeField] AudioClip[] damageSounds;
        [SerializeField] AudioClip[] deathSounds;
        [SerializeField] float deathVanishSeconds = 1.5f;
        [SerializeField] float healthPointsToRegenPerSecond = 0f;
        [SerializeField] float armour;
        [SerializeField] Text healthDisplayText = null;
        [SerializeField] Image healthBar;
        

       
        float currentHealthPoints = 0;
        Animator animator = null;
        AudioSource audioSource = null;
        Character characterMovement;
        PlayerStats playerStats;

        const string DEATH_TRIGGER = "Death";

       
        public float healthAsPercentage
        {
            get{return CurrentHealthPoints / MaxHealthpoints;}
        }
      
        public float CurrentHealthPoints
        {
            get{return currentHealthPoints;}
            set{currentHealthPoints = value;

            }
        }

        public float MaxHealthpoints
        {
            get{ return maxHealthPoints; }
            set{maxHealthPoints = value;
                UpdateHealthBar();
            }
        }

        public float HealthPointsToRegenPerSecond
        {
            get{return healthPointsToRegenPerSecond;}
            set{healthPointsToRegenPerSecond = value;}
        }

        public float Armour
        {
            get {return armour;} 
            set { armour = value;}
        }

        void Start()
        {
            if (GetComponent<PlayerControl>())
            {
                var HB = GameObject.Find("Environment/Game Canvas/Health Bar");
                var HT = GameObject.Find("Environment/Game Canvas/Hp Text");
                healthBar = HB.GetComponent<Image>();
                healthDisplayText = HT.GetComponent<Text>();
            }
            playerStats = GetComponent<PlayerStats>();
            animator = GetComponent<Animator>();
            audioSource = GetComponent<AudioSource>();
            characterMovement = GetComponent<Character>();
            CurrentHealthPoints = MaxHealthpoints;
            UpdateHealthBar();
        }

        void Update()
        {
            if (!gameObject.GetComponent<EnemyAI>() && CurrentHealthPoints < MaxHealthpoints)
            {
                AutoRegenerateHealth();
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
                healthBar.fillAmount = healthAsPercentage;
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
            if (!IsInvoking("UpdateHealthBar") && CurrentHealthPoints < MaxHealthpoints)
            {
                // var pointsToAddPerSecond = healthPointsToRegenPerSecond;
                CurrentHealthPoints = Mathf.Clamp(CurrentHealthPoints + HealthPointsToRegenPerSecond, 0, MaxHealthpoints);
                Invoke("UpdateHealthBar", 1f);
            }
            else if(CurrentHealthPoints >= MaxHealthpoints)
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
            //will need to get this from enemyAI script
            audioSource.clip = deathSounds[UnityEngine.Random.Range(0, deathSounds.Length)];
            audioSource.Play();
           
            if (playerComponent && playerComponent.isActiveAndEnabled)
            { 
                 yield return new WaitForSecondsRealtime(deathVanishSeconds);
                 Destroy(gameObject, deathVanishSeconds);
            }
            else
            {
                if (GetComponent<EnemyAI>())
                {
                    var xpToAward = GetComponent<EnemyAI>().XpValue();
                    player.XpToBeAwarded(xpToAward);
                }
                // todo instantiate loot at enemy position before destroying

                yield return new WaitForSecondsRealtime(deathVanishSeconds);
                Destroy(gameObject, deathVanishSeconds);
            }
           // StopAllCoroutines();
        }   
    }
}
