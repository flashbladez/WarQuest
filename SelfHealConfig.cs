using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WarQuest.Characters
{
    [CreateAssetMenu(menuName =("RPG/Special Ability/Self Heal"))]
    public class SelfHealConfig : AbilityConfig
    {
        [Header("Self Heal Specific")]
        [SerializeField] float extraHealth = 70f;
        [SerializeField] float damageDelay = 0.5f;

        public override AbilityBehaviour GetBehaviourComponent(GameObject objectToAttachTo)
        {
            return objectToAttachTo.AddComponent<SelfHealBehaviour>();
        }
        
        public float GetExtraHealth()
        {
            return extraHealth;
        }

        public float GetDamageDelay()
        {
            return damageDelay;
        }
    }
}
