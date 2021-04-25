using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Deeper
{
    public class KnightExplosion : MonoBehaviour
    {
        #region Inspector



        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ProjectileHitEffect projectileHit))
            {
                // i have the feeling this is getting called twice so let's see
                if (TryGetComponent(out Rigidbody _))
                    return;

                float explosionForce = 3000;
                var projectilePosition = projectileHit.transform.position;
                var projectileRadius = projectileHit.CurrentRadius;

                var knight = GetComponent<Knight>();

                var weapon = knight.Weapon;

                if (weapon != null)
                {
                    weapon.SetParent(transform.parent);

                    foreach (var collider in weapon.GetComponentsInChildren<Collider>())
                        collider.enabled = true;

                    var weaponBody = weapon.gameObject.AddComponent<Rigidbody>();
                    weaponBody.AddExplosionForce(explosionForce, projectilePosition, projectileRadius);
                }

                foreach (var collider in GetComponentsInChildren<Collider>())
                    collider.enabled = true;

                var body = gameObject.AddComponent<Rigidbody>();
                body.AddExplosionForce(explosionForce, projectilePosition, projectileRadius);

                Destroy(GetComponent<KnightAttackCloseRange>());
                Destroy(GetComponent<KnightMove>());
                Destroy(knight);
                Destroy(GetComponent<Animator>());
                Destroy(GetComponent<CharacterController>());
                Destroy(this);
            }
        }
    }
    
    [Serializable]
    public class UnityEventKnightExplosion : UnityEvent<KnightExplosion> { }
}