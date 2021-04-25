using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Deeper
{
    public class PlankFamily : MonoBehaviour
    {
        #region Inspector

        [SerializeField]
        private List<KnightExplosion> guardians = null;

        #endregion

        public void AllowExplosion()
        {
            foreach (var group in GetComponentsInChildren<PlankGroup>())
                group.canExplode = true;
        }

        private void Start()
        {
            if (guardians != null && guardians.Count > 0)
            {
                foreach (var knight in guardians)
                    knight.onExplosion.AddListener(OnKnightExploded);
            }
            else
            {
                AllowExplosion();
            }
        }

        private void OnKnightExploded(KnightExplosion knight)
        {
            guardians.Remove(knight);

            if (guardians.Count == 0)
                AllowExplosion();
        }
    }
    
    [Serializable]
    public class UnityEventPlankFamily : UnityEvent<PlankFamily> { }
}