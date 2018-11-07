using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WarQuest.Environment {
    [System.Serializable]

    public class DistanceGameObjectsConfig :MonoBehaviour  {

        [SerializeField] GameObject[] gameObjectsToDeactivate;


       public GameObject[] GameObjectsToDeactivate()
        {
            return gameObjectsToDeactivate;
        }
    }
}

