using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace WarQuest.Characters
{
    [CreateAssetMenu(menuName = ("RPG/Weapon"))]
    public class WeaponConfig: ScriptableObject
    {

        public Transform gripTransform;
        [SerializeField] GameObject weaponPrefab;
        [SerializeField] AnimationClip attackAnimation;
        [SerializeField] WeaponConfig weaponEquipped;
        [SerializeField] float timeBetweenAnimationCycles = 0.5f;
        [SerializeField] float maxAttackrange = 2f;
        //[SerializeField] float additionalDamage = 10f;
        [SerializeField] float damageDelay = .5f;
        [SerializeField] bool weaponOneHanded = false;
        [SerializeField] Image icon = null;
        [SerializeField] string objectName;
        [SerializeField] float staminaValue;
        [SerializeField] float mentalAgilityValue;
        [SerializeField] float energyValue;
        [SerializeField] float strengthValue;
        [SerializeField] float hitValue;
        [SerializeField] float armourValue;
        [SerializeField] int levelEquippableValue;

        public float GetTimeBetweenAnimationCycles()
        {
            return timeBetweenAnimationCycles;
        }

        public float GetMaxAttackRange()
        {
            return maxAttackrange;
        }

        public WeaponConfig GetWeaponConfig()
        {
            return weaponEquipped;
        }

        public GameObject GetWeaponPrefab()
        {
            return weaponPrefab;
        }

        public AnimationClip GetAttackAnimClip()
        {
            RemoveAnimationEvents();
            return attackAnimation;
        }

        public float GetDamageDelay()
        {
            return damageDelay;
        }

        public bool GetweaponHandType()
        {
            return weaponOneHanded;
        }

        public bool GetIcon()
        {
            return icon;
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

        void RemoveAnimationEvents()
        {
            attackAnimation.events = new AnimationEvent[0];
        }
    }
}

