using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//stores equipped armor configs

namespace WarQuest.Characters
{
    [CreateAssetMenu(menuName = "RPG/PlayerArmourEquipped")]

    public class ArmourEquippedConfig : ScriptableObject
    {
        [SerializeField] HeadSlotConfig headSlot;
        [SerializeField] ShoulderSlotConfig shoulderSlot;
        [SerializeField] NeckSlotConfig neckSlot;
        [SerializeField] WristSlotConfig wristSlot;
        [SerializeField] HandSlotConfig handSlot;
        [SerializeField] ChestSlotConfig chestSlot;
        [SerializeField] BackSlotConfig backSlot;
        [SerializeField] LegSlotConfig legSlot;
        [SerializeField] FootSlotConfig footSlot;
        [SerializeField] FingerSlotConfig fingerSlot;
        [SerializeField] WeaponConfig equippedWeapon;

        public HeadSlotConfig HeadSlotEquipped
        {
            get{ return headSlot; }
            set{ headSlot = value; }
        }

        public ShoulderSlotConfig ShoulderSlotEquipped
        {
            get { return shoulderSlot; }
            set { shoulderSlot = value; }
        }

        public NeckSlotConfig NeckSlotEquipped
        {
            get { return neckSlot; }
            set { neckSlot = value; }
        }

        public WristSlotConfig WristSlotEquipped
        {
            get { return wristSlot; }
            set { wristSlot = value; }
        }

        public HandSlotConfig HandSlotEquipped
        {
            get { return handSlot; }
            set { handSlot = value; }
        }

        public ChestSlotConfig ChestSlotEquipped
        {
            get { return chestSlot; }
            set { chestSlot = value; }
        }

        public BackSlotConfig BackSlotEquipped
        {
            get { return backSlot; }
            set { backSlot = value; }
        }

        public LegSlotConfig LegSlotEquipped
        {
            get { return legSlot; }
            set { legSlot = value; }
        }

        public FootSlotConfig FootSlotEquipped
        {
            get { return footSlot; }
            set { footSlot = value; }
        }

        public FingerSlotConfig FingerSlotEquipped
        {
            get { return fingerSlot; }
            set { fingerSlot = value; }
        }

        public WeaponConfig WeaponSlotEquipped
        {
            get { return equippedWeapon; }
            set { equippedWeapon = value; }
        }
    }
}
