using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Deeper
{
    public class Projectile : MonoBehaviour
    {
        #region Inspector

        public ProjectileHitEffect hitEffectPrefab = null;

        [SerializeField]
        private float destroyAfterHitSeconds = 1f;

        #endregion

        public bool IsHurling { get; private set; }

        public void PrepareForHurl()
        {
            var body = GetComponent<Rigidbody>();
            body.isKinematic = true;
            body.useGravity = false;
        }

        public void Hurl(Vector3 impulse)
        {
            var body = GetComponent<Rigidbody>();
            body.isKinematic = false;
            body.useGravity = true;

            body.AddForce(impulse, ForceMode.Impulse);
            IsHurling = true;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (hitEffectPrefab != null)
                Instantiate(hitEffectPrefab, transform.position, transform.rotation, transform.parent);

            foreach (var particles in GetComponentsInChildren<ParticleSystem>())
                particles.Stop();

            Destroy(GetComponent<Rigidbody>());
            Destroy(GetComponent<Collider>());

            Invoke(nameof(DestroyMyself), destroyAfterHitSeconds);
        }

        private void DestroyMyself()
        {
            Destroy(gameObject);
        }
    }
    
    [Serializable]
    public class UnityEventProjectile : UnityEvent<Projectile> { }
}