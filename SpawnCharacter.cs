using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WarQuest.Characters
{
    public class SpawnCharacter : MonoBehaviour
    {
        [SerializeField] WayPointContainer patrolPathToAssign = null;
        [SerializeField] GameObject characterPrefab;
        [SerializeField] float respawnDelay = 5f;
        [SerializeField] int startingWaypoint = 0;

        void Start()
        {
            InstantiateCharacter();
        }


        void Update()
        {
            if(gameObject.transform.childCount == 0)
            {
                Invoke("InstantiateCharacter", respawnDelay);
            }
        }

        void InstantiateCharacter()
        {
            CancelInvoke();
            var character = Instantiate(characterPrefab, transform);

            character.transform.SetParent(gameObject.transform);
          
            if (this.gameObject.transform.GetChild(0).GetComponent<EnemyAI>()) {
                var thisEnemyAIComponent = this.gameObject.transform.GetChild(0).GetComponent<EnemyAI>();
                    thisEnemyAIComponent.SetPatrolPath(patrolPathToAssign, startingWaypoint);
            }
            else if(this.gameObject.transform.GetChild(0).GetComponent<NPCAI>())
            {
                var thisNPCAIComponent = this.gameObject.transform.GetChild(0).GetComponent<NPCAI>();
                thisNPCAIComponent.SetPatrolPath(patrolPathToAssign,startingWaypoint);
                
            }else if (this.gameObject.transform.GetChild(0).GetComponent<PlayerControl>())
            {
                
             
            }
        }
    }
}
