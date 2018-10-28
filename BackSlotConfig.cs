using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//queried by playerStats for setting 

namespace WarQuest.Characters
{
  
    [CreateAssetMenu(menuName = "RPG/ArmourSlot/BackSlot")]

    public class BackSlotConfig : ScriptableObject
    {
        public Transform goToAttachToTransform;
       // [SerializeField] BackSlotConfig backSlot;
        [SerializeField] GameObject armorPrefab;
        [SerializeField] string objectName;
        [SerializeField] float staminaValue;
        [SerializeField] float mentalAgilityValue;
        [SerializeField] float energyValue;
        [SerializeField] float strengthValue;
        [SerializeField] float hitValue;
        [SerializeField] float armourValue;
        [SerializeField] Image icon = null;
        [SerializeField] int levelEquippableValue;

       // public BackSlotConfig Slot()
       // {
        //     return backSlot;
       // }

        public GameObject GetArmourPrefab()
        {
            return armorPrefab;
        }

        public string GetObjectName()
        {
            return objectName;
        }

        public float GetStamina()
        {
            return staminaValue;
        }

        public float GetMentalAgility()
        {
            return mentalAgilityValue;
        }

        public float GetEnergy()
        {
            return energyValue;
        }

        public float GetStrength()
        {
            return strengthValue;
        }

        public float GetHit()
        {
            return hitValue;
        }

        public float GetArmourValue()
        {
            return armourValue;
        }

        public int GetLevelEquippable()
        {
            return levelEquippableValue;
        }

        public Image GetIcon()
        {
            return icon;
        }

    }
}
