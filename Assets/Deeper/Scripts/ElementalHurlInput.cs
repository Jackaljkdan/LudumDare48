using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Deeper
{
    [RequireComponent(typeof(Elemental))]
    public class ElementalHurlInput : MonoBehaviour
    {
        #region Inspector

        [SerializeField]
        private Transform projectileParent = null;

        [SerializeField]
        private Transform directionReference = null;

        #endregion

        private void Update()
        {
            var elemental = GetComponent<Elemental>();

            if (Input.GetAxis("Fire1") != 0)
                elemental.Hurl(projectileParent, pos => directionReference.forward);
        }
    }
    
    [Serializable]
    public class UnityEventElementalHurlInput : UnityEvent<ElementalHurlInput> { }
}