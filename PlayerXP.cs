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
        [SerializeField] float multiplierForNextLevel = 25f;
        

        int currentLevel = 1;
        float xpToLevel = 100f;
        float currentXp = 0f;

        PlayerStats characterStats;

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
            characterStats = GetComponent<PlayerStats>();
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
            characterStats.CurrentXp();
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
                characterStats.LevelUpHealthPoints();
                characterStats.LevelUpEnergyPoints();
                characterStats.LevelUpStrengthPoints();
                characterStats.LevelUpMentalAgility();
                characterStats.LevelUpHitPoints();
                characterStats.LevelUpArmourPoints();
                characterStats.CurrentLevel();
                characterStats.CurrentXp();
                characterStats.MaxXp();
                UpdateXpBar();
            }
            xpText.text = (CurrentXP.ToString() + "/" + XpToLevel.ToString());
            levelText.text = (Level.ToString());
        }
  
    }
}
