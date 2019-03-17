using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WarQuest.Characters;
namespace WarQuest.Environment
{
    //ToDo use audio mixer to connect sound effects up in different places also add volume of sound effects closer to source 

    public class PlayAudioEffects : MonoBehaviour
    {
        [SerializeField] AudioClip[] audioClips;
        [SerializeField] float timeBetweenClipPlaysMin = 0f;
        [SerializeField] float timeBetweenClipPlaysMax = 10f;

        AudioSource audioSource;
        string playSoundEffects = "PlayVillageSoundEffects";

        void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerControl>())
            {
               // PlayVillageSoundEffects();
                InvokeRepeating(playSoundEffects, 0f, UnityEngine.Random.Range(timeBetweenClipPlaysMin, timeBetweenClipPlaysMax));
            }
        }

        private void OnTriggerExit(Collider other)
        {
            CancelInvoke(); 
        }

        void PlayVillageSoundEffects()
        {
            var clip = audioClips[UnityEngine.Random.Range(0, audioClips.Length)];
            audioSource.PlayOneShot(clip);
        }
    }
}
