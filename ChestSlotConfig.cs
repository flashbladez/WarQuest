using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace WarQuest.Characters
{
    [CreateAssetMenu(menuName = "RPG/ArmourSlot/ChestSlot")]

    public class ChestSlotConfig : ScriptableObject
    {
        public Transform goToAttachToTransform;
        [SerializeField] ChestSlotConfig chestSlot;
        [SerializeField] GameObject armorPrefab;
        [SerializeField] string objectName;
        [SerializeField] float staminaValue;
        [SerializeField] float mentalAgilityValue;
        [SerializeField] float energyValue;
        [SerializeField] float strengthValue;
        [SerializeField] float hitValue;
        [SerializeField] float armourValue;

        public ChestSlotConfig ChestSlot()
        {
            return chestSlot;
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

        public float GeEneregy()
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

    }
}
