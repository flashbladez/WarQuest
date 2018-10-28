using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



namespace WarQuest.Characters
{
    [CreateAssetMenu(menuName = "RPG/ArmourSlot/ShoulderSlot")]

    public class ShoulderSlotConfig : ScriptableObject
    {

        public Transform goToAttachToTransform;
        [SerializeField] ShoulderSlotConfig shoulderSlot;
        [SerializeField] GameObject armorPrefab;
        [SerializeField] string objectName;
        [SerializeField] float staminaValue;
        [SerializeField] float mentalAgilityValue;
        [SerializeField] float energyValue;
        [SerializeField] float strengthValue;
        [SerializeField] float hitValue;
        [SerializeField] float armourValue;
        [SerializeField] Image icon = null;
        public ShoulderSlotConfig ShoulderSlot()
        {
            return shoulderSlot;
        }

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

        public Image GetIcon()
        {
            return icon;
        }
    }
}
