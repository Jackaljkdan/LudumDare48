using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Deeper
{
    [RequireComponent(typeof(KnightMove), typeof(Animator))]
    public class KnightAttackCloseRange : MonoBehaviour
    {
        #region Inspector

        public float attackRangeSquared = 4;

        #endregion

        public void Attack(Transform target)
        {
            if (target == this.target)
                return;

            this.target = target;

            StartCoroutine(AttackCoroutine());
        }

        public void StopAttacking()
        {
            target = null;
            StopAllCoroutines();
        }

        private Transform target;

        private void Start()
        {

        }

        private bool IsPlayingAttackAnim(Animator animator)
        {
            return animator.GetBool("Attack");
        }

        private IEnumerator AttackCoroutine()
        {
            var move = GetComponent<KnightMove>();
            var animator = GetComponent<Animator>();

            while (true)
            {
                if (target == null)
                    break;

                bool needsToMove = move.SetDestination(target.position, stopDistanceSquared: attackRangeSquared);

                if (!needsToMove && !IsPlayingAttackAnim(animator))
                    animator.SetTrigger("Attack");

                yield return null;
            }
        }

        public void OnDamageFrame()
        {

        }
    }
}