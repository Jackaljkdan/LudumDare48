using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Deeper
{
    [RequireComponent(typeof(SphereCollider), typeof(Rigidbody))]
    public class ProjectileHitEffect : MonoBehaviour
    {
        #region Inspector

        [SerializeField]
        private float initialRadius = 1;

        [SerializeField]
        private float finalRadius = 2;

        private void OnValidate()
        {
            var collider = GetComponent<SphereCollider>();
            collider.isTrigger = true;
            collider.radius = initialRadius;
        }

        #endregion

        public float CurrentRadius => GetComponent<SphereCollider>().radius;

        private void Start()
        {
            var collider = GetComponent<SphereCollider>();

            collider.radius = initialRadius;

            float computedDuration = 0;

            foreach (var particles in GetComponentsInChildren<ParticleSystem>())
            {
                float duration = particles.duration + particles.startLifetime;
                if (duration > computedDuration)
                    computedDuration = duration;
                particles.Play();
            }

            StartCoroutine(ExpandRadiusCoroutine(computedDuration));
        }

        private IEnumerator ExpandRadiusCoroutine(float duration)
        {
            var sphereCollider = GetComponent<SphereCollider>();

            float initialTime = Time.time;

            while (initialTime + duration > Time.time)
            {
                sphereCollider.radius = Mathf.Lerp(initialRadius, finalRadius, (Time.time - initialTime) / duration);
                yield return null;
            }

            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log($"hitting {other.name}");
        }
    }
    
    [Serializable]
    public class UnityEventProjectileHitEffect : UnityEvent<ProjectileHitEffect> { }
}