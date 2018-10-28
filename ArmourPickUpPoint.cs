using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WarQuest.Characters
{
   // [ExecuteInEditMode]

    public class ArmourPickUpPoint : MonoBehaviour
    {
        [SerializeField] AudioClip pickUpAudioClip;
        [SerializeField] BackSlotConfig[] backSlotChoices;

        [SerializeField] ArmourEquippedConfig armourEquippedConfig;
        AudioSource audioSource;
        BackSlotConfig armour;
        PlayerStats playerStats;
        int randomItem;

        void Start()
        {
            randomItem = UnityEngine.Random.Range(0, backSlotChoices.Length);
            audioSource = GetComponent<AudioSource>();
            InstantiateArmour();
        }

        //instantiate armour in game area not on player
        void InstantiateArmour()
        {
            armour = backSlotChoices[randomItem];
            var armourDrop = Instantiate(armour.GetArmourPrefab(),gameObject.transform);
           // armourDrop.transform.position = Vector3.zero;
        }

        //refactor
        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<PlayerControl>())
            {
                // subtract stats of previously equipped item first
                playerStats = other.gameObject.GetComponent<PlayerStats>();

                playerStats.LevelUpStamina(-armourEquippedConfig.BackSlotEquipped.GetStamina());
                playerStats.LevelUpEnergy(-armourEquippedConfig.BackSlotEquipped.GetEnergy());
                playerStats.LevelUpMentalAgi(-armourEquippedConfig.BackSlotEquipped.GetMentalAgility());
                playerStats.LevelUpStrength(-armourEquippedConfig.BackSlotEquipped.GetStrength());
                playerStats.LevelUpHit(-armourEquippedConfig.BackSlotEquipped.GetHit());
                playerStats.LevelUpArmour(-armourEquippedConfig.BackSlotEquipped.GetArmourValue());

                 armourEquippedConfig.BackSlotEquipped = backSlotChoices[randomItem];

                //pass stats of armour to playerstats to add
                playerStats.LevelUpStamina(armourEquippedConfig.BackSlotEquipped.GetStamina());
                playerStats.LevelUpEnergy(armourEquippedConfig.BackSlotEquipped.GetEnergy());
                playerStats.LevelUpMentalAgi(armourEquippedConfig.BackSlotEquipped.GetMentalAgility());
                playerStats.LevelUpStrength(armourEquippedConfig.BackSlotEquipped.GetStrength());
                playerStats.LevelUpHit(armourEquippedConfig.BackSlotEquipped.GetHit());
                playerStats.LevelUpArmour(armourEquippedConfig.BackSlotEquipped.GetArmourValue());
                Destroy(gameObject);

                // print(armourEquippedConfig.BackSlotEquipped);
                //  audioSource.PlayOneShot(pickUpAudioClip);
                //todo equip on player
             //   Destroy(gameObject);
            }
        }
    }
}
