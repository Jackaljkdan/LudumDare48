using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Deeper
{
    [RequireComponent(typeof(Animator), typeof(CharacterController))]
    public class KnightMove : MonoBehaviour
    {
        #region Inspector

        public float stopDistanceSquared = 4;

        public float speed = 4;

        #endregion

        public Vector3? CurrentDestination { get; private set; }

        public bool HasReachedDestination
        {
            get
            {
                if (!CurrentDestination.HasValue)
                    return true;

                return (CurrentDestination.Value - transform.position).sqrMagnitude <= stopDistanceSquared;
            }
        }

        /// <summary>
        /// Returns whether movement is needed to reach the destination
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="stopDistanceSquared"></param>
        /// <returns></returns>
        public bool SetDestination(Vector3 destination, float? stopDistanceSquared = null)
        {
            CurrentDestination = destination;

            if (stopDistanceSquared.HasValue)
                this.stopDistanceSquared = stopDistanceSquared.Value;

            bool reached = HasReachedDestination;

            if (!reached && !GetComponent<Animator>().GetBool("Move"))
                StartCoroutine(ReachDestinationCoroutine());

            return !reached;
        }

        public void Stop()
        {
            CurrentDestination = null;
            StopAllCoroutines();
        }

        private void Start()
        {

        }

        private IEnumerator ReachDestinationCoroutine()
        {
            GetComponent<Animator>().SetBool("Move", true);
            var character = GetComponent<CharacterController>();

            while (CurrentDestination.HasValue)
            {
                Vector3 direction = (CurrentDestination.Value - transform.position).normalized;
                character.SimpleMove(direction * speed);
                transform.forward = Vector3.Lerp(transform.forward, direction, 0.5f);

                if (HasReachedDestination)
                    break;

                yield return null;
            }

            CurrentDestination = null;
            GetComponent<Animator>().SetBool("Move", false);
        }
    }
    
    [Serializable]
    public class UnityEventKnightMove : UnityEvent<KnightMove> { }
}