using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Deeper
{
    [RequireComponent(typeof(Collider))]
    public class PlankGroup : MonoBehaviour
    {
        #region Inspector



        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ProjectileHitEffect projectileHit))
            {
                GetComponent<Collider>().enabled = false;

                var projectilePosition = projectileHit.transform.position;
                var projectileRadius = projectileHit.CurrentRadius;

                foreach (Transform child in transform)
                {
                    child.GetComponent<Collider>().enabled = true;
                    var body = child.gameObject.AddComponent<Rigidbody>();
                    body.AddExplosionForce(100, projectilePosition, projectileRadius);
                }
            }
        }
    }
    
    [Serializable]
    public class UnityEventPlankGroup : UnityEvent<PlankGroup> { }
}