using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Deeper
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        #region Inspector

        

        #endregion

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
        }
    }
    
    [Serializable]
    public class UnityEventProjectile : UnityEvent<Projectile> { }
}