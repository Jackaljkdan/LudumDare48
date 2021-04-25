using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Deeper
{
    [RequireComponent(typeof(Animator))]
    public class Elemental : MonoBehaviour
    {
        #region Inspector

        public float hurlForce = 10;

        [SerializeField]
        private Projectile projectilePrefab = null;

        [Header("Internal")]

        [SerializeField]
        private Transform projectileAnchor = null;

        #endregion

        public delegate Vector3 GetProjectileDirectionDelegate(Vector3 projectilePosition);

        public bool IsHurling { get => hurlingProjectile != null; }

        public void Hurl(Transform futureProjectileParent, GetProjectileDirectionDelegate getProjectileDirection)
        {
            if (IsHurling)
                return;

            GetComponent<Animator>().SetTrigger("Hurl");

            this.futureProjectileParent = futureProjectileParent;

            hurlingProjectile = Instantiate(projectilePrefab, projectileAnchor);
            hurlingProjectile.PrepareForHurl();

            getProjectileDirectionDelegate = getProjectileDirection;
        }

        public void OnReleaseProjectileFrame()
        {
            if (hurlingProjectile == null)
                return;

            hurlingProjectile.transform.SetParent(futureProjectileParent);
            hurlingProjectile.Hurl(getProjectileDirectionDelegate(projectileAnchor.position) * hurlForce);

            hurlingProjectile = null;
            futureProjectileParent = null;
            getProjectileDirectionDelegate = null;
        }

        private Projectile hurlingProjectile;
        private GetProjectileDirectionDelegate getProjectileDirectionDelegate;
        private Transform futureProjectileParent;
    }
    
    [Serializable]
    public class UnityEventElemental : UnityEvent<Elemental> { }
}