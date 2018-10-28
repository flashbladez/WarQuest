using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WarQuest.Characters
{
    public class Projectile : MonoBehaviour
    {


        [SerializeField] float projectileSpeed;
        [SerializeField] GameObject shooter;
        public float damageCaused;
        const float DESTROY_DELAY = 0.01f;

        public void SetShooter(GameObject shooter)
        {
            this.shooter = shooter;
        }

        public void SetDamage(float damage)
        {
            damageCaused = damage;
        }

        public float GetDefaultLaunchSpeed()
        {
            return projectileSpeed;
        }

        void OnCollisionEnter(Collision collision)
        {
            var layerCollidedWith = collision.gameObject.layer;

            if (shooter && layerCollidedWith != shooter.layer)
            {
               // DamageIfDamageable(collision);
            }
        }

       // void DamageIfDamageable(Collision collision)
       // {
         //   Component damageableComponent = collision.gameObject.GetComponent(typeof(HealthSystem));
           // if (damageableComponent)
           // {
            //    (damageableComponent as HealthSystem).TakeDamage(damageCaused);
           // }
           // Destroy(gameObject, DESTROY_DELAY);
        //}
    }
}
