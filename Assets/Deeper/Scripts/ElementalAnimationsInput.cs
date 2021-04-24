using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Deeper
{
    [RequireComponent(typeof(CharacterControllerInput))]
    public class ElementalAnimationsInput : MonoBehaviour
    {
        #region Inspector

        [SerializeField]
        private Animator animator = null;

        #endregion

        private void Start()
        {
            GetComponent<CharacterControllerInput>().onInput.AddListener(OnInput);
        }

        private void OnInput(Vector3 directedVelocity)
        {
            animator.SetBool("Moving", directedVelocity.sqrMagnitude > 0);
        }
    }
}