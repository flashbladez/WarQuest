using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script deals with setting the enemy stats

namespace WarQuest.Characters
{

    public class EnemyStats : MonoBehaviour
    {
        [SerializeField] float baseStamina;
        [SerializeField] float baseEnergy;
        [SerializeField] float baseMentalAgility;
        [SerializeField] float baseStrength;
        [SerializeField] float baseHit;
        [SerializeField] float level;
        [SerializeField] float baseArmour = 0f;
        [SerializeField] float baseDamage;

        WeaponSystem weaponSystem;
        HealthSystem healthSystem;
        SpecialAbilities specialAbility;
       
        void Start()
        {
            weaponSystem = GetComponent<WeaponSystem>();
            healthSystem = GetComponent<HealthSystem>();
            specialAbility = GetComponent<SpecialAbilities>();
        
            SetUpEnemyStats();
        }

        //Sets up enemy stats by setting level * base values base values
        public void SetUpEnemyStats()
        {
            CalculateTotalArmour();
            CalculateTotalStamina();
            CalculateTotalEnergy();
            CalculateTotalMentalAgility();
            CalculateTotalStrength();
            CalculateTotalHit(); // needs to be done
        }

        public void CalculateTotalArmour()
        {
            healthSystem.Armour = level * baseArmour;
        }

        public void CalculateTotalStamina()
        {
            healthSystem.MaxHealthpoints = level * baseStamina;
         }

        public void CalculateTotalEnergy()
        {
          //  specialAbility.MaxEnergyPoints = level * baseEnergy;
        }

        public void CalculateTotalMentalAgility()
        {
            healthSystem.HealthPointsToRegenPerSecond = level * baseMentalAgility;
        }

        public void CalculateTotalStrength()
        {
           
            weaponSystem.SetBaseDamage = level * baseStrength;
        }

        public void CalculateTotalHit()
        {
           // enemyStatsConfig.Hit = baseDamage + (enemyStatsConfig.SavedLevel * baseHit);

        }
    }
}
