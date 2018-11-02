using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WarQuest.Characters
{
    public class PlayerXP : MonoBehaviour
    {

        [SerializeField] Image xpBar;
        [SerializeField] Text xpText;
        [SerializeField] Text levelText;
        [SerializeField] float multiplierForNextLevel = 3f;
        

        int currentLevel;
        float xpToLevel;
        float currentXp;

        PlayerStats playerStats;

        void Start()
        {
            if (GetComponent<PlayerControl>())
            {
                var XPB = GameObject.Find("Environment/Game Canvas/XPBar");
                var XPT = GameObject.Find("Environment/Game Canvas/XPText");
                var LevelT = GameObject.Find("Environment/Game Canvas/LevelText");
                xpBar = XPB.GetComponent<Image>();
                xpText = XPT.GetComponent<Text>();
                levelText = LevelT.GetComponent<Text>();
            }
            playerStats = GetComponent<PlayerStats>();
           
            UpdateXpBar();
        }

        public float CurrentXP
        {
            get{return currentXp;}
            set{currentXp = value;}
        }

        public float XpToLevel
        {
            get{return xpToLevel;}
            set{xpToLevel = value;}
        }

        public int Level
        {
            get{return currentLevel;}
            set{currentLevel = value;}
        }

        public float xpAsPercentage
        {
            get{return CurrentXP / XpToLevel; }
        }

       
        public void XpToBeAwarded(float xpToAdd)
        {
            CurrentXP += xpToAdd;
            playerStats.CurrentXp();
            UpdateXpBar();
        }

       
        void UpdateXpBar()
        {
            if (xpBar)
            {
                xpText.text = (CurrentXP.ToString() + "/" + XpToLevel.ToString());
                xpBar.fillAmount = xpAsPercentage;
                LevelUp();
            }
        }

        void LevelUp()
        {
            if (CurrentXP >= XpToLevel)
            {
                CurrentXP -= XpToLevel;
                Level += 1;
                XpToLevel += Mathf.Round(XpToLevel * multiplierForNextLevel) / Level;
                playerStats.LevelUpHealthPoints();
                playerStats.LevelUpEnergyPoints();
                playerStats.LevelUpStrengthPoints();
                playerStats.LevelUpMentalAgility();
                playerStats.LevelUpHitPoints();
                playerStats.LevelUpArmourPoints();
                playerStats.CurrentLevel();
                playerStats.CurrentXp();
                playerStats.MaxXp();
                playerStats. SaveStatsToPlayerPrefs();
                UpdateXpBar();
            }
            xpText.text = (CurrentXP.ToString() + "/" + XpToLevel.ToString());
            levelText.text = (Level.ToString());
        }
  
    }
}
