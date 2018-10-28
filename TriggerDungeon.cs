using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using WarQuest.Characters;

namespace WarQuest.Dungeons
{
    public class TriggerDungeon : MonoBehaviour
    {
        [SerializeField] int sceneNumberToLoad;

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<PlayerControl>())
            {
                SceneManager.LoadScene(sceneNumberToLoad);
            }
        }
    }

}