using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WarQuest.Characters
{
    [CreateAssetMenu(menuName =("RPG/Special Ability/AOE"))]
    public class AOEConfig : AbilityConfig
    {
        [Header("AOE Specific")]
        [SerializeField] float radius = 2f;
        [SerializeField] float damageToEachTarget = 15f;
        [SerializeField] float damageDelay = 0.5f;

        public override AbilityBehaviour GetBehaviourComponent(GameObject objectToAttachTo)
        {
            return objectToAttachTo.AddComponent<AOEBehaviour>();
        }

        public float GetDamageToEachTarget()
        {
            return damageToEachTarget;
        }

        public float GetRadius()
        {
            return radius;
        }

        public float GetDamageDelay()
        {
            return damageDelay;
        }
    }
}
