using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script deals with keeping the player stats updated and saved by other scripts

namespace WarQuest.Characters
{

    public class PlayerStats : MonoBehaviour
    {
        [Header("Points to add to base stats each level up")]
        [SerializeField] float stamina = 0f;
        [SerializeField] float energy = 0f;
        [SerializeField] float mentalAgility = 0f;
        [SerializeField] float strength = 0f;
        [SerializeField] float hit = 0f;
        [SerializeField] float armour = 0f;
        [SerializeField] PlayerStatsConfig playerStatsConfig;
        [SerializeField] ArmourEquippedConfig armourEquippedConfig;

        [Header("Base Stats at level 1")]
        [SerializeField] float baseArmour = 0f;
        [SerializeField] float baseDamage = 0f;

        WeaponSystem weaponSystem;
        HealthSystem healthSystem;
        SpecialAbilities specialAbility;
        PlayerXP playerXp;
        int currentLevel;
        float currentXp;
       
        void Start()
        {
            playerXp = GetComponent<PlayerXP>();
            weaponSystem = GetComponent<WeaponSystem>();
            healthSystem = GetComponent<HealthSystem>();
            specialAbility = GetComponent<SpecialAbilities>();
            // var head = armourEquippedConfig.HeadSlotEquipped;
            //  print(head.GetObjectName());//it works
            weaponSystem.UpdateCurrentWeapon = armourEquippedConfig.WeaponSlotEquipped;
            SetUpPlayerStats();
        }

        public ArmourEquippedConfig ArmourEquippedConfig
        {
            get{return armourEquippedConfig;}
        }

        public PlayerStatsConfig PlayerStatsConfig
        {
            get { return playerStatsConfig; }
        }

        //todo add stats from when on first play level1
        //****************************************************************
        //Calls the neccesary method to add extra points to each stat awarded for leveling up and writes new values to playerstatsconfig scriptable object
        public void LevelUpHealthPoints()
        {
            LevelUpStamina(stamina);
        }

        public void LevelUpEnergyPoints()
        {
            LevelUpEnergy(energy);
        }

        public void LevelUpStrengthPoints()
        {
            LevelUpStrength(energy);
        }

        public void LevelUpMentalAgility()
        {
            LevelUpMentalAgi(mentalAgility);
        }

        public void LevelUpHitPoints()
        {
            LevelUpHit(hit);
        }

        public void LevelUpArmourPoints()
        {
            LevelUpArmour(armour);
        }
        //**************************************************************
        //adds incoming changes to stats from various calls and updates playerStatsConfig

        public void LevelUpStamina(float stamina)
        {
            healthSystem.MaxHealthpoints += stamina;
            playerStatsConfig.Stamina = healthSystem.MaxHealthpoints;
        }

        public void LevelUpEnergy(float energy)
        {
            specialAbility.MaxEnergyPoints += energy;
            playerStatsConfig.Energy = specialAbility.MaxEnergyPoints;
        }

        public void LevelUpStrength(float strength)
        {
            weaponSystem.SetBaseDamage += strength;
            playerStatsConfig.Strength = weaponSystem.SetBaseDamage;
        }

        public void LevelUpMentalAgi(float mentalAgility)
        {
            healthSystem.HealthPointsToRegenPerSecond += mentalAgility;
            playerStatsConfig.MentalAgility = healthSystem.HealthPointsToRegenPerSecond;
        }

        public void LevelUpHit(float hit)
        {
            // add to hit rating when collider combat detection or raycast combat is done
        }

        public void LevelUpArmour(float armour)
        {
            healthSystem.Armour += armour;
            playerStatsConfig.TotalArmour = healthSystem.Armour;
        }

        public void CurrentXp()
        {
            playerStatsConfig.SavedXp = playerXp.CurrentXP;
        }

        public void CurrentLevel()
        {
            playerStatsConfig.SavedLevel = playerXp.Level;       
        }

        public void MaxXp()
        {
            playerStatsConfig.MaxXpForNextLevel = playerXp.XpToLevel;
        }

        //set newweapon in armourEquippedConfig and updateCurrentWeapon
        public void WeaponUpdate(WeaponConfig newWeaponEquipped)
        {
            weaponSystem.UpdateCurrentWeapon = newWeaponEquipped;
            armourEquippedConfig.WeaponSlotEquipped =  newWeaponEquipped;
        }

        //Sets up player stats by getting all values from armour weapons and base values
        public void SetUpPlayerStats()
        {
            CalculateTotalArmour();
            CalculateTotalStamina();
            CalculateTotalEnergy();
            CalculateTotalMentalAgility();
            CalculateTotalStrength();
            CalculateTotalHit(); // needs to be done
            playerXp.CurrentXP = playerStatsConfig.SavedXp;
            playerXp.Level = playerStatsConfig.SavedLevel;
            playerXp.XpToLevel = playerStatsConfig.MaxXpForNextLevel;
            weaponSystem.UpdateCurrentWeapon = armourEquippedConfig.WeaponSlotEquipped;
        }

        public void CalculateTotalArmour()
        {
            float armourEquippedTotals = 0;
            armourEquippedTotals += armourEquippedConfig.BackSlotEquipped.GetArmourValue();
            armourEquippedTotals += armourEquippedConfig.HeadSlotEquipped.GetArmourValue();
            armourEquippedTotals += armourEquippedConfig.NeckSlotEquipped.GetArmourValue();
            armourEquippedTotals += armourEquippedConfig.ShoulderSlotEquipped.GetArmourValue();
            armourEquippedTotals += armourEquippedConfig.ChestSlotEquipped.GetArmourValue();
            armourEquippedTotals += armourEquippedConfig.WristSlotEquipped.GetArmourValue();
            armourEquippedTotals += armourEquippedConfig.LegSlotEquipped.GetArmourValue();
            armourEquippedTotals += armourEquippedConfig.HandSlotEquipped.GetArmourValue();
            armourEquippedTotals += armourEquippedConfig.FootSlotEquipped.GetArmourValue();
            armourEquippedTotals += armourEquippedConfig.FingerSlotEquipped.GetArmourValue();
            armourEquippedTotals += weaponSystem.UpdateCurrentWeapon.GetArmourValue();
            playerStatsConfig.TotalArmour = (playerStatsConfig.SavedLevel * baseArmour) + armourEquippedTotals;
            healthSystem.Armour = playerStatsConfig.TotalArmour;
            
        }

        public void CalculateTotalStamina()
        {
            float staminaEquippedTotals = 0;
            staminaEquippedTotals += armourEquippedConfig.BackSlotEquipped.GetStamina();
            staminaEquippedTotals += armourEquippedConfig.HeadSlotEquipped.GetStamina();
            staminaEquippedTotals += armourEquippedConfig.NeckSlotEquipped.GetStamina();
            staminaEquippedTotals += armourEquippedConfig.ShoulderSlotEquipped.GetStamina();
            staminaEquippedTotals += armourEquippedConfig.WristSlotEquipped.GetStamina();
            staminaEquippedTotals += armourEquippedConfig.LegSlotEquipped.GetStamina();
            staminaEquippedTotals += armourEquippedConfig.HandSlotEquipped.GetStamina();
            staminaEquippedTotals += armourEquippedConfig.FootSlotEquipped.GetStamina();
            staminaEquippedTotals += armourEquippedConfig.FingerSlotEquipped.GetStamina();
            staminaEquippedTotals += weaponSystem.UpdateCurrentWeapon.GetStamina();//check stamina from weapon is being added
          
            playerStatsConfig.Stamina = healthSystem.MaxHealthpoints + (playerStatsConfig.SavedLevel * stamina) + staminaEquippedTotals;
            healthSystem.MaxHealthpoints = playerStatsConfig.Stamina;
            //put other modifiers here
          
        }

        public void CalculateTotalEnergy()
        {
            float energyEquippedTotals = 0;
            energyEquippedTotals += armourEquippedConfig.BackSlotEquipped.GetEnergy();
            energyEquippedTotals += armourEquippedConfig.HeadSlotEquipped.GetEnergy();
            energyEquippedTotals += armourEquippedConfig.NeckSlotEquipped.GetEnergy();
            energyEquippedTotals += armourEquippedConfig.ShoulderSlotEquipped.GetEnergy();
            energyEquippedTotals += armourEquippedConfig.WristSlotEquipped.GetEnergy();
            energyEquippedTotals += armourEquippedConfig.LegSlotEquipped.GetEnergy();
            energyEquippedTotals += armourEquippedConfig.HandSlotEquipped.GetEnergy();
            energyEquippedTotals += armourEquippedConfig.FootSlotEquipped.GetEnergy();
            energyEquippedTotals += armourEquippedConfig.FingerSlotEquipped.GetEnergy();
            energyEquippedTotals += weaponSystem.UpdateCurrentWeapon.GetEnergy();
            playerStatsConfig.Energy = specialAbility.MaxEnergyPoints + (playerStatsConfig.SavedLevel * energy) + energyEquippedTotals;
            specialAbility.MaxEnergyPoints = playerStatsConfig.Energy;
            //put other modifiers here
          
        }

        public void CalculateTotalMentalAgility()
        {
            float mentalAgilityEquippedTotals = 0;
            mentalAgilityEquippedTotals += armourEquippedConfig.BackSlotEquipped.GetMentalAgility();
            mentalAgilityEquippedTotals += armourEquippedConfig.HeadSlotEquipped.GetMentalAgility();
            mentalAgilityEquippedTotals += armourEquippedConfig.NeckSlotEquipped.GetMentalAgility();
            mentalAgilityEquippedTotals += armourEquippedConfig.ShoulderSlotEquipped.GetMentalAgility();
            mentalAgilityEquippedTotals += armourEquippedConfig.WristSlotEquipped.GetMentalAgility();
            mentalAgilityEquippedTotals += armourEquippedConfig.LegSlotEquipped.GetMentalAgility();
            mentalAgilityEquippedTotals += armourEquippedConfig.HandSlotEquipped.GetMentalAgility();
            mentalAgilityEquippedTotals += armourEquippedConfig.FootSlotEquipped.GetMentalAgility();
            mentalAgilityEquippedTotals += armourEquippedConfig.FingerSlotEquipped.GetMentalAgility();
            mentalAgilityEquippedTotals += weaponSystem.UpdateCurrentWeapon.GetMentalAgility();

            playerStatsConfig.MentalAgility = healthSystem.HealthPointsToRegenPerSecond + (playerStatsConfig.SavedLevel * mentalAgility) + mentalAgilityEquippedTotals;
            healthSystem.HealthPointsToRegenPerSecond = playerStatsConfig.MentalAgility;
            //put other modifiers here
        
        }

        public void CalculateTotalStrength()
        {
            float strengthEquippedTotals = 0;
            strengthEquippedTotals += armourEquippedConfig.BackSlotEquipped.GetStrength();
            strengthEquippedTotals += armourEquippedConfig.HeadSlotEquipped.GetStrength();
            strengthEquippedTotals += armourEquippedConfig.NeckSlotEquipped.GetStrength();
            strengthEquippedTotals += armourEquippedConfig.ShoulderSlotEquipped.GetStrength();
            strengthEquippedTotals += armourEquippedConfig.WristSlotEquipped.GetStrength();
            strengthEquippedTotals += armourEquippedConfig.LegSlotEquipped.GetStrength();
            strengthEquippedTotals += armourEquippedConfig.HandSlotEquipped.GetStrength();
            strengthEquippedTotals += armourEquippedConfig.FootSlotEquipped.GetStrength();
            strengthEquippedTotals += armourEquippedConfig.FingerSlotEquipped.GetStrength();
            strengthEquippedTotals += weaponSystem.UpdateCurrentWeapon.GetStrength();
         
            playerStatsConfig.Strength = baseDamage + (playerStatsConfig.SavedLevel * strength) + strengthEquippedTotals;
            weaponSystem.SetBaseDamage = playerStatsConfig.Strength;
            //put other modifiers here
         
        }

        public void CalculateTotalHit()
        {
            float hitEquippedTotals = 0;
            hitEquippedTotals += armourEquippedConfig.BackSlotEquipped.GetHit();
            hitEquippedTotals += armourEquippedConfig.HeadSlotEquipped.GetHit();
            hitEquippedTotals += armourEquippedConfig.NeckSlotEquipped.GetHit();
            hitEquippedTotals += armourEquippedConfig.ShoulderSlotEquipped.GetHit();
            hitEquippedTotals += armourEquippedConfig.WristSlotEquipped.GetHit();
            hitEquippedTotals += armourEquippedConfig.LegSlotEquipped.GetHit();
            hitEquippedTotals += armourEquippedConfig.HandSlotEquipped.GetHit();
            hitEquippedTotals += armourEquippedConfig.FootSlotEquipped.GetHit();
            hitEquippedTotals += armourEquippedConfig.FingerSlotEquipped.GetHit();
            hitEquippedTotals += weaponSystem.UpdateCurrentWeapon.GetHit();

        }
    }
}
