using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WarQuest.Environment
{
    public class DistanceGameObjectManager : MonoBehaviour
    {

       
        [SerializeField] float maxDistanceToDeactivate;

        DistanceGameObjectsConfig distanceGameObjectConfig;
        GameObject[] gameObjectToDeactivate;

        void Start()
        {
            distanceGameObjectConfig = GameObject.FindGameObjectWithTag("DistanceObjects").GetComponent<DistanceGameObjectsConfig>();
        }


        void Update()
        {
            gameObjectToDeactivate = distanceGameObjectConfig.GameObjectsToDeactivate();
        
            for (int i = 0; i < gameObjectToDeactivate.Length; i++)
            {
              //   var playerPositionWorldSpace = transform.TransformPoint(transform.position);
                var distanceToGameObject = Vector3.Distance(gameObjectToDeactivate[i].transform.position, transform.position);
              
                if (distanceToGameObject > maxDistanceToDeactivate)
                {
                    gameObjectToDeactivate[i].SetActive(false);
                }
                else
                {
                    gameObjectToDeactivate[i].SetActive(true);
                }
            }
        }
    }
}
