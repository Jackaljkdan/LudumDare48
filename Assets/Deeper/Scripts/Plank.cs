using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Deeper
{
    public class Plank : MonoBehaviour
    {
        #region Inspector



        #endregion

        private void Start()
        {
            Invoke(nameof(DestroySelf), 3);
        }

        private void DestroySelf()
        {
            Destroy(gameObject);
        }

        private void OnBecameInvisible()
        {
            if (enabled)
                Destroy(gameObject);
        }
    }
    
    [Serializable]
    public class UnityEventPlank : UnityEvent<Plank> { }
}