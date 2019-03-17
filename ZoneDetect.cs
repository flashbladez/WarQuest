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
        [SerializeField] AudioClip[] audioClips;
        [SerializeField] bool playMusic = true;

        string wakeUpMusic = "WakeUpMusic";
        string fadeMusic = "FadeMusic";
        AudioSource audioSource;
        int randomNumber = 0;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.volume = 0f;
            
        }

        void OnTriggerEnter(Collider other)
        {
           
            if (other.gameObject.GetComponent<PlayerControl>())
            {
                textDisplay.GetComponent<ZoneDisplayText>().DisplayZoneText(zoneName);
                if (playMusic)
                {
                    randomNumber = Random.Range(0, audioClips.Length);
                    audioSource.clip = audioClips[randomNumber];
                    audioSource.Play(0);
                    CancelInvoke(fadeMusic);
                    InvokeRepeating(wakeUpMusic, 1f, 1f);
                }
            }

        }

        void OnTriggerExit(Collider other)
        {
            //todo after clip finishes wait a random time before starting again
            //music fades out the further away from zone player gets and blends from one clip to another from zone to zone
            if (other.gameObject.GetComponent<PlayerControl>())
            {
                CancelInvoke(wakeUpMusic);
                if (playMusic) {
                    InvokeRepeating(fadeMusic, 1f, 1f);
                }
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