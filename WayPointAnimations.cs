using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WarQuest.Characters
{
    public class WayPointAnimations : MonoBehaviour
    {

        [SerializeField] AnimationClip[] animClip;
        [SerializeField] int index = 0;

        Animator animator;
        Character character;
        const string ATTACK_TRIGGER = "Attack";
        const string DEFAULT_ATTACK = "DEFAULT_ATTACK";
        
        //float lastHitTime = 0.5f;
        AnimatorOverrideController animatorOverrideController;

        void Start()
        {
            animator = GetComponent<Animator>();
            character = GetComponent<Character>();
        }

        void UpdateAnimator()
        {
          
            animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
            animator.runtimeAnimatorController = character.GetOverrideController();
            character.GetOverrideController()[DEFAULT_ATTACK] = animClip[index];
            RemoveAnimationEvents();
            PlayAnimationOnce();
        }

        public void SetIndex(int indexNumber)
        {
            if (indexNumber >= animClip.Length)
            {
                indexNumber = 0;
            }
            index = indexNumber;

            UpdateAnimator();
        }

        void RemoveAnimationEvents()
        {
            animClip[index].events = new AnimationEvent[0];
        }

        void PlayAnimationOnce()
        {
              //  transform.LookAt(target.transform);
                animator.SetTrigger(ATTACK_TRIGGER);
        }
    }
}
