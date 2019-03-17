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
        [SerializeField] bool reSpawnAllowed = true;
        [SerializeField] bool reSpawnAllowedAlternative = false;
        [SerializeField] float initialSpawnTimeSecs = 20f;

        bool previousSpawn = false;
        string instantiateCharacter = "InstantiateCharacter";

        void Start()
        {
            Invoke(instantiateCharacter,initialSpawnTimeSecs);
        }


        void Update()
        {
            if(gameObject.transform.childCount == 0 && previousSpawn)
            {
                if (reSpawnAllowed)
                {
                    Invoke(instantiateCharacter, respawnDelay);
                }else
                {
                    Destroy(gameObject);
                }
            }
        }

        void InstantiateCharacter()
        {
            previousSpawn = true;
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
            }
            else if (this.gameObject.transform.GetChild(0).GetComponent<NeutralAI>())
            {
                var thisNeutralAIComponent = this.gameObject.transform.GetChild(0).GetComponent<NeutralAI>();
                thisNeutralAIComponent.SetPatrolPath(patrolPathToAssign, startingWaypoint);
                
            }
        }
    }
}
