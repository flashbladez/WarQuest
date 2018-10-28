using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WarQuest.Characters
{
   
    public class WeaponPickUpPoint : MonoBehaviour
    {
        [SerializeField] WeaponConfig[] weaponConfig = null;
        [SerializeField] AudioClip pickUpAudioClip;
        [SerializeField] ArmourEquippedConfig armourStatsConfig;
        AudioSource audioSource;
        WeaponSystem weaponSystem;
        GameObject weapon;
        PlayerStats playerStats = null;
        int randomItem;

        //will make it select from a list randomly of weapons
        void Start()
        {
            randomItem = UnityEngine.Random.Range(0,weaponConfig.Length);
            audioSource = GetComponent<AudioSource>();
            InstantiateWeapon();
        }

        //instantiate weapon in game area not on player
        void InstantiateWeapon()
        {
            weapon = weaponConfig[randomItem].GetWeaponPrefab();
            Instantiate(weapon, gameObject.transform);
            weapon.transform.position = Vector3.zero;
        }
      
           // Instantiate on player when walked over
        void OnTriggerEnter(Collider other)
        {
            //refactor
          //  print(other.gameObject.name);
            if (other.gameObject.GetComponent<PlayerControl>())
            {
                playerStats = other.gameObject.GetComponent<PlayerStats>();
           //maybe refactor this out so it can be called here and when player starts up.
                playerStats.LevelUpStamina(-armourStatsConfig.WeaponSlotEquipped.GetStamina());          
                playerStats.LevelUpEnergy(-armourStatsConfig.WeaponSlotEquipped.GetEnergy());
                playerStats.LevelUpMentalAgi(-armourStatsConfig.WeaponSlotEquipped.GetMentalAgility());
                playerStats.LevelUpStrength(-armourStatsConfig.WeaponSlotEquipped.GetStrength());
                playerStats.LevelUpHit(-armourStatsConfig.WeaponSlotEquipped.GetHit());
                playerStats.LevelUpArmour(-armourStatsConfig.WeaponSlotEquipped.GetArmourValue());

                weaponSystem = other.gameObject.GetComponent<WeaponSystem>();
                weaponSystem.PutWeaponInHand(weaponConfig[randomItem]);//equip weapon in weaponSystem
                      
                playerStats.LevelUpStamina(armourStatsConfig.WeaponSlotEquipped.GetStamina());
                playerStats.LevelUpEnergy(armourStatsConfig.WeaponSlotEquipped.GetEnergy());
                playerStats.LevelUpMentalAgi(armourStatsConfig.WeaponSlotEquipped.GetMentalAgility());
                playerStats.LevelUpStrength(armourStatsConfig.WeaponSlotEquipped.GetStrength());
                playerStats.LevelUpHit(armourStatsConfig.WeaponSlotEquipped.GetHit());
                playerStats.LevelUpArmour(armourStatsConfig.WeaponSlotEquipped.GetArmourValue());
               

                audioSource.PlayOneShot(pickUpAudioClip);
                Destroy(gameObject, pickUpAudioClip.length);
            }
             
        }
    }
}
