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

        [SerializeField]
        private float aggroRangeSquared = 100;

        #endregion

        private Transform player;

        private void Start()
        {
            player = FindObjectOfType<CharacterControllerInput>().transform;
        }

        private void Update()
        {
            if (player == null)
                return;

            if ((player.position - transform.position).sqrMagnitude <= aggroRangeSquared)
                GetComponent<KnightAttackCloseRange>().Attack(player);
        }
    }
    
    [Serializable]
    public class UnityEventKnight : UnityEvent<Knight> { }
}