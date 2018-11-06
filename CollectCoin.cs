using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WarQuest.Characters;

namespace WarQuest.Loot
{
    public class CollectCoin : MonoBehaviour
    {
        [Range(1f, 1000f)]
        [SerializeField] float coinToAward;//maybe make 2 vars one for gold one for silver
        [SerializeField] bool gold;
        [SerializeField] AudioClip pickUpAudioClip;
        AudioSource audioSource;

        float coin = 0;
        void Start()
        {
            coinToAward = Mathf.Round(coinToAward);
            coinToAward = Mathf.Floor(Random.Range(coinToAward / 2, coinToAward));
            audioSource = GetComponent<AudioSource>();
        }

        void OnTriggerEnter(Collider other)
        {
            //will refactor this into a central script so quests can use it etc
            if (other.gameObject.GetComponent<PlayerControl>())
            {
                if (gold)
                {
                   other.gameObject.GetComponent<PlayerStats>().PlayerStatsConfig.Gold = coinToAward;
                }
                else
                {
                    coin = other.gameObject.GetComponent<PlayerStats>().PlayerStatsConfig.Silver;
                    if (coin + coinToAward >= 100)
                    {
                        other.gameObject.GetComponent<PlayerStats>().PlayerStatsConfig.Gold += Mathf.Floor((coin + coinToAward) / 100);
                        var gold = Mathf.Floor((coin + coinToAward) / 100);
                        var silver = (coin + coinToAward) - (gold * 100);
                        other.gameObject.GetComponent<PlayerStats>().PlayerStatsConfig.Silver = silver;
                    }
                    else
                    {
                        other.gameObject.GetComponent<PlayerStats>().PlayerStatsConfig.Silver += coinToAward;
                    }
                }
                other.GetComponent<PlayerStats>().SaveStatsToPlayerPrefs();
                audioSource.PlayOneShot(pickUpAudioClip);

            }
            
        }
    }
}
