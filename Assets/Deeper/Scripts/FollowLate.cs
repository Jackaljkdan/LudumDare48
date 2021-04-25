using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Deeper
{
    public class FollowLate : MonoBehaviour
    {
        #region Inspector

        public Transform target;

        #endregion

        private void LateUpdate()
        {
            if (target)
            {
                transform.position = target.position;
                transform.rotation = target.rotation;
            }
        }
    }
    
    [Serializable]
    public class UnityEventFollowLate : UnityEvent<FollowLate> { }
}