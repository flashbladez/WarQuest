using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WarQuest.Characters
{
  [CreateAssetMenu(menuName = "RPG/ArmourSlot/HandSlot")]

public class HandSlotConfig : ScriptableObject
{
    public Transform goToAttachToTransform;
    [SerializeField] HandSlotConfig handSlot;
    [SerializeField] GameObject armorPrefab;
    [SerializeField] string objectName;
    [SerializeField] float staminaValue;
    [SerializeField] float mentalAgilityValue;
    [SerializeField] float energyValue;
    [SerializeField] float strengthValue;
    [SerializeField] float hitValue;
    [SerializeField] float armourValue;

    public HandSlotConfig HandSlot()
    {
        return handSlot;
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

}
}
