using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
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

        Text staminaDisplay = null;
        Text energyDisplay = null;
        Text strengthDisplay = null;
        Text hitDisplay = null;
        Text mentalAgilityDisplay = null;
        Text levelDisplay = null;
        Text armourDisplay = null;
        Text damageDisplay = null;
        Text goldDisplay = null;
        Text silverDisplay = null;

        [Header("Base Stats at level 1")]
        [SerializeField] float baseArmour = 0f;
        [SerializeField] float baseStrength = 0f;

        GameObject uidDisplay;
        WeaponSystem weaponSystem;
        HealthSystem healthSystem;
        SpecialAbilities specialAbility;
        PlayerXP playerXp;
        int currentLevel;
        float currentXp;
        bool playerIsAlive = false;
        bool uiOpen = false;
      //  string staminaKey = "StaminaKey";
       // string energyKey = "EnergyKey";
        //string mentalAgilityKey = "MentalAgilityKey";
       // string strengthKey = "StrengthKey";
      //  string hitKey = "HitKey";
     //   string armourKey = "ArmourKey";
      //  string totalDamageKey = "TotalDamageKey";
        string currentXpKey = "CurrentXpKey";
        string maxXpKey = "MaxXpKey";
        string levelKey = "LevelKey";
        string goldKey = "GoldKey";
        string silverKey = "SilverKey";

        private void Awake()
        {
            playerXp = GetComponent<PlayerXP>();
            weaponSystem = GetComponent<WeaponSystem>();
            healthSystem = GetComponent<HealthSystem>();
            specialAbility = GetComponent<SpecialAbilities>();
            // var head = armourEquippedConfig.HeadSlotEquipped;
            //  print(head.GetObjectName());//it works
            uidDisplay = GameObject.Find("Environment/Game Canvas/PlayerStatsDisplay");
            
            PlayerIsAlive = true;
            if (PlayerPrefs.HasKey(levelKey))
            {
                LoadStatsFromPlayerPrefs();
              //  print(PlayerPrefs.GetFloat(levelKey));
            }
            SortPlayerStats();
            healthSystem.SethealthBarText();
            specialAbility.SetEergyBarText();
        }

        void OnApplicationQuit()
        {
            SaveStatsToPlayerPrefs();
        }

        public ArmourEquippedConfig ArmourEquippedConfig
        {
            get{return armourEquippedConfig;}
        }

        public PlayerStatsConfig PlayerStatsConfig
        {
            get { return playerStatsConfig; }
        }

        public bool PlayerIsAlive
        {
            get { return playerIsAlive; }
            set { playerIsAlive = value; }
        }

        public void DisplayStats()
        {
            uiOpen = !uiOpen;
            uidDisplay.SetActive(uiOpen);
            var staminaText = GameObject.Find("Environment/Game Canvas/PlayerStatsDisplay/StaminaDisplay");
            var energyText = GameObject.Find("Environment/Game Canvas/PlayerStatsDisplay/EnergyDisplay");
            var strengthText = GameObject.Find("Environment/Game Canvas/PlayerStatsDisplay/StrengthDisplay");
            var mentalAgilityText = GameObject.Find("Environment/Game Canvas/PlayerStatsDisplay/MentalAgilityDisplay");
            var hitText = GameObject.Find("Environment/Game Canvas/PlayerStatsDisplay/HitDisplay");
            var levelText = GameObject.Find("Environment/Game Canvas/PlayerStatsDisplay/LevelDisplay");
            var armourText = GameObject.Find("Environment/Game Canvas/PlayerStatsDisplay/ArmourDisplay");
            var damageText = GameObject.Find("Environment/Game Canvas/PlayerStatsDisplay/DamageDisplay");
            var goldText = GameObject.Find("Environment/Game Canvas/PlayerStatsDisplay/GoldDisplay");
            var silverText = GameObject.Find("Environment/Game Canvas/PlayerStatsDisplay/SilverDisplay");

            staminaDisplay = staminaText.GetComponent<Text>();
            energyDisplay = energyText.GetComponent<Text>();
            strengthDisplay = strengthText.GetComponent<Text>();
            mentalAgilityDisplay = mentalAgilityText.GetComponent<Text>();
            hitDisplay = hitText.GetComponent<Text>();
            levelDisplay = levelText.GetComponent<Text>();
            armourDisplay = armourText.GetComponent<Text>();
            damageDisplay = damageText.GetComponent<Text>();
            goldDisplay = goldText.GetComponent<Text>();
            silverDisplay = silverText.GetComponent<Text>();

            staminaDisplay.text = playerStatsConfig.Stamina.ToString();
            energyDisplay.text = playerStatsConfig.Energy.ToString();
            strengthDisplay.text = playerStatsConfig.Strength.ToString();
            mentalAgilityDisplay.text = playerStatsConfig.MentalAgility.ToString();
            hitDisplay.text = playerStatsConfig.Hit.ToString();
            levelDisplay.text = playerStatsConfig.SavedLevel.ToString();
            armourDisplay.text = playerStatsConfig.TotalArmour.ToString();
            damageDisplay.text = playerStatsConfig.TotalDamage.ToString();
            goldDisplay.text = playerStatsConfig.Gold.ToString();
            silverDisplay.text = playerStatsConfig.Silver.ToString();
        }

        //Calls the neccesary method to add extra points to each stat awarded for leveling up 
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
           // healthSystem.MaxHealthpoints += stamina;
            playerStatsConfig.Stamina = healthSystem.MaxHealthpoints + stamina;
        }

        public void LevelUpEnergy(float energy)
        {
          //  specialAbility.MaxEnergyPoints += energy;
            playerStatsConfig.Energy = specialAbility.MaxEnergyPoints + energy;
        }

        public void LevelUpStrength(float strength)
        {
          //  weaponSystem.SetBaseDamage += strength;
            playerStatsConfig.Strength = weaponSystem.SetBaseDamage + strength;
            playerStatsConfig.TotalDamage = playerStatsConfig.Strength;
        }

        public void LevelUpMentalAgi(float mentalAgility)
        {
           // healthSystem.HealthPointsToRegenPerSecond += mentalAgility;
            playerStatsConfig.MentalAgility = healthSystem.HealthPointsToRegenPerSecond + mentalAgility;
        }

        public void LevelUpHit(float hit)
        {
            // add to hit rating when collider combat detection or raycast combat is done
        }

        public void LevelUpArmour(float armour)
        {
           // healthSystem.Armour += armour;
            playerStatsConfig.TotalArmour = healthSystem.Armour + armour;
        }

        public void CurrentXp()
        {
            playerStatsConfig.SavedXp = playerXp.CurrentXP;
        }

        public void CurrentLevel()
        {
            playerStatsConfig.SavedLevel = playerXp.Level;
           // playerXp.Level = playerStatsConfig.SavedLevel;
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
        public void SortPlayerStats()
        {
        
            CalculateTotalArmour();
            CalculateTotalStamina();
            CalculateTotalEnergy();
            CalculateTotalMentalAgility();
            CalculateTotalStrength();
            CalculateTotalHit(); // needs to be done collider hits
            playerXp.CurrentXP = playerStatsConfig.SavedXp;
            playerXp.Level = playerStatsConfig.SavedLevel;
            playerXp.XpToLevel = playerStatsConfig.MaxXpForNextLevel;
            weaponSystem.UpdateCurrentWeapon = armourEquippedConfig.WeaponSlotEquipped;
            SaveStatsToPlayerPrefs();
        }

        public void SaveStatsToPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
          //  PlayerPrefs.SetFloat(staminaKey, playerStatsConfig.Stamina);
          //  PlayerPrefs.SetFloat(energyKey, playerStatsConfig.Energy);
          //  PlayerPrefs.SetFloat(strengthKey, playerStatsConfig.Strength);
          //  PlayerPrefs.SetFloat(mentalAgilityKey, playerStatsConfig.MentalAgility);
          //  PlayerPrefs.SetFloat(hitKey, playerStatsConfig.Hit);
          //  PlayerPrefs.SetFloat(armourKey, playerStatsConfig.TotalArmour);
          //  PlayerPrefs.SetFloat(totalDamageKey, playerStatsConfig.TotalDamage);
            PlayerPrefs.SetFloat(currentXpKey, playerStatsConfig.SavedXp);
            PlayerPrefs.SetFloat(maxXpKey, playerStatsConfig.MaxXpForNextLevel);
            PlayerPrefs.SetFloat(goldKey, playerStatsConfig.Gold);
            PlayerPrefs.SetFloat(silverKey, playerStatsConfig.Silver);
            PlayerPrefs.SetInt(levelKey, playerStatsConfig.SavedLevel);
            PlayerPrefs.Save();
        }

        public void LoadStatsFromPlayerPrefs()
        {
          //  playerStatsConfig.Stamina = PlayerPrefs.GetFloat(staminaKey);
          //  playerStatsConfig.Energy = PlayerPrefs.GetFloat(energyKey);
          //  playerStatsConfig.Strength = PlayerPrefs.GetFloat(strengthKey);
          //  playerStatsConfig.MentalAgility = PlayerPrefs.GetFloat(mentalAgilityKey);
          //  playerStatsConfig.Hit = PlayerPrefs.GetFloat(hitKey);
          //  playerStatsConfig.TotalArmour = PlayerPrefs.GetFloat(armourKey);
          //  playerStatsConfig.TotalDamage = PlayerPrefs.GetFloat(totalDamageKey);
            playerStatsConfig.SavedXp = PlayerPrefs.GetFloat(currentXpKey);
            playerStatsConfig.MaxXpForNextLevel = PlayerPrefs.GetFloat(maxXpKey);
            playerStatsConfig.Gold = PlayerPrefs.GetFloat(goldKey);
            playerStatsConfig.Silver = PlayerPrefs.GetFloat(silverKey);
            playerStatsConfig.SavedLevel = PlayerPrefs.GetInt(levelKey);
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

          //  print(playerStatsConfig.Stamina + " FF   " + healthSystem.MaxHealthpoints);
            playerStatsConfig.Stamina = healthSystem.MaxHealthpoints + (playerStatsConfig.SavedLevel * stamina) + staminaEquippedTotals;
            healthSystem.MaxHealthpoints = playerStatsConfig.Stamina;

          //  print(playerStatsConfig.Stamina + "    " + healthSystem.MaxHealthpoints);
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
         //   print(strengthEquippedTotals);

            playerStatsConfig.Strength = baseStrength  + (playerStatsConfig.SavedLevel * strength) + strengthEquippedTotals;
            playerStatsConfig.TotalDamage = playerStatsConfig.Strength;
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
