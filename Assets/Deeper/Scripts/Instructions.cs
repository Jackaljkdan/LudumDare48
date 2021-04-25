using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Deeper
{
    public class Instructions : MonoBehaviour
    {
        #region Inspector

        

        #endregion

        private void Start()
        {
            Invoke(nameof(DestroySelf), 6);
        }

        private void DestroySelf()
        {
            Destroy(gameObject);
        }
    }
    
    [Serializable]
    public class UnityEventInstructions : UnityEvent<Instructions> { }
}