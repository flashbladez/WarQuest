using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WarQuest.Characters;

namespace WarQuest.Environment
{
    public class ZoneDetect : MonoBehaviour
    {
        [SerializeField] string zoneName = "";
        [SerializeField] GameObject textDisplay;
        [SerializeField] AudioClip[] audioClip;
        AudioSource audioSource;
         int randomNumber = 0;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.volume = 1f;
            
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<PlayerControl>())
            {
                randomNumber = Random.Range(0, audioClip.Length);
                textDisplay.GetComponent<ZoneDisplayText>().DisplayZoneText(zoneName);
                audioSource.clip = audioClip[randomNumber];
                audioSource.Play(0);
                InvokeRepeating("WakeUpMusic", 1f, 1f);
            }

        }

        void OnTriggerExit(Collider other)
        {//after clip finishes wait a random time before starting again
         //music fades out the further away from zone player gets and maybe blends from one clip to another from zone to zone
            if (other.gameObject.GetComponent<PlayerControl>())
            {
              InvokeRepeating("FadeMusic",1f,1f);
            }
        }


        void FadeMusic()
        {
            if (audioSource.volume > 0f) {
                audioSource.volume -= .05f;
            }
            else
            {
                audioSource.Stop();
                CancelInvoke();
            }
        }

        void WakeUpMusic()
        {
            if (audioSource.volume < 1f)
            {
                audioSource.volume += .05f;
            }
            else
            {
                CancelInvoke();
            }
        }
    }
}