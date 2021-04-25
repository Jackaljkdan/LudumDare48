using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Deeper
{
    public class Knight : MonoBehaviour
    {
        #region Inspector

        [Header("Internals")]

        [SerializeField]
        private Transform weaponSlot = null;

        #endregion
    }
    
    [Serializable]
    public class UnityEventKnight : UnityEvent<Knight> { }
}