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

        public bool canExplode = false;

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (!canExplode)
                return;

            if (other.TryGetComponent(out ProjectileHitEffect projectileHit))
            {
                GetComponent<Collider>().enabled = false;

                var projectilePosition = projectileHit.transform.position;
                var projectileRadius = projectileHit.CurrentRadius;

                List<Transform> children = new List<Transform>(transform.childCount);

                foreach (Transform child in transform)
                    children.Add(child);

                foreach (Transform child in children)
                {
                    child.GetComponent<Plank>().enabled = true;
                    child.GetComponent<Collider>().enabled = true;
                    var body = child.gameObject.AddComponent<Rigidbody>();
                    body.AddExplosionForce(100, projectilePosition, projectileRadius);
                    child.SetParent(transform.parent);
                }

                Destroy(gameObject);
            }
        }
    }
    
    [Serializable]
    public class UnityEventPlankGroup : UnityEvent<PlankGroup> { }
}