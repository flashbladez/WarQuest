using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WarQuest.Characters
{
    public class PlayerXP : MonoBehaviour
    {

        [SerializeField] Image xpBar = null;
        [SerializeField] Text xpText;
        [SerializeField] Text levelText;
        [SerializeField] float multiplierForNextLevel = 3f;
        [SerializeField] GameObject levelUpEffect;

        string xpeBar = "Game Canvas/XPBar";
        string xpeText = "Game Canvas/XPText";
        string levelTxt = "Game Canvas/LevelText";
        int currentLevel;
        float xpToLevel;
        float currentXp;
        float destroyEffectTimer = 3f;
        GameObject levelUpParticleEffect = null;
        PlayerStats playerStats;

        void Start()
        {
            if (GetComponent<PlayerControl>())
            {
                var XPB = GameObject.Find(xpeBar);
                var XPT = GameObject.Find(xpeText);
                var LevelT = GameObject.Find(levelTxt);
                xpBar = XPB.GetComponent<Image>();
                xpText = XPT.GetComponent<Text>();
                levelText = LevelT.GetComponent<Text>();
            }
            playerStats = GetComponent<PlayerStats>();
            //  levelUpEffect = GameObject.FindGameObjectWithTag("LevelUp");

            UpdateXpBar();
        }

        public float CurrentXP
        {
            get { return currentXp; }
            set { currentXp = value; }
        }

        public float XpToLevel
        {
            get { return xpToLevel; }
            set { xpToLevel = value; }
        }

        public int Level
        {
            get { return currentLevel; }
            set { currentLevel = value; }
        }

        public float xpAsPercentage
        {
            get { return CurrentXP / XpToLevel; }
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
                print(CurrentXP);
                levelUpParticleEffect = Instantiate(levelUpEffect, transform);
                levelUpParticleEffect.transform.position = gameObject.transform.TransformPoint(Vector3.up * 1);
               
                Invoke("CancelParticleEffect", destroyEffectTimer);

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
                playerStats.SaveStatsToPlayerPrefs();
                UpdateXpBar();
            }
            xpText.text = (CurrentXP.ToString() + "/" + XpToLevel.ToString());
            levelText.text = (Level.ToString());
        }

        void CancelParticleEffect()
        {
            CancelInvoke();
            Destroy(levelUpParticleEffect);
        }
    }
}
