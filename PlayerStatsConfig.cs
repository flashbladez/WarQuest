using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//This script saves all players values so they can be retrieved everytime the player instantiates.

namespace WarQuest.Characters
{
   
    [CreateAssetMenu(menuName = ("RPG/PlayerStatsConfig"))]
    [System.Serializable]
    public class PlayerStatsConfig : ScriptableObject
    {
       
        [SerializeField] float stamina;
        [SerializeField] float energy;
        [SerializeField] float strength;
        [SerializeField] float mentalAgility;
        [SerializeField] float hit;
        [SerializeField] float totalArmour;
        [SerializeField] float currentXp;
        [SerializeField] float xpToNextLevel;
        [SerializeField] float gold;
        [SerializeField] float silver;
        [SerializeField] float totalDamage;
        [SerializeField] int level;
      
       // EditorUtility.SetDirty(PlayerStatsConfig);
        
        public float Stamina
        {
            get{return stamina;}
            set{stamina = value;}
        }

        public float Energy
        {
            get{return energy;}
            set{energy = value;}
        }

        public float Strength
        {
            get{return strength;}
            set{strength = value;}
        }

        public float MentalAgility
        {
            get{return mentalAgility;}
            set{mentalAgility = value;}
        }

        public float Hit
        {
            get{return hit;}
            set{hit = value;}
        }

        public float TotalArmour
        {
            get { return totalArmour; }
            set { totalArmour = value; }
        }

        public float TotalDamage
        {
            get { return totalDamage; }
            set { totalDamage = value; }
        }

        public int SavedLevel
        {
            get{return level;}
            set{level = value;}
        }

        public float SavedXp
        {
            get{return currentXp;}
            set{currentXp = value;}
        }

        public float Gold
        {
            get { return gold; }
            set { gold = value; }
        }

        public float Silver
        {
            get { return silver; }
            set { silver = value; }
        }

        public float MaxXpForNextLevel
        {
            get{return xpToNextLevel;}
            set{xpToNextLevel = value;}
        }
    }
}
