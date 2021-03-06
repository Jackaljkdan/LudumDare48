using JK.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Deeper
{
    [RequireComponent(typeof(CharacterController))]
    public class CharacterControllerInput : MonoBehaviour
    {
        #region Inspector

        public float speed = 1;

        public Transform directionReference;

        public UnityEventVector3 onInput = new UnityEventVector3();

        private void Reset()
        {
            directionReference = transform;
        }

        #endregion

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            var cc = GetComponent<CharacterController>();

            Vector3 velocity = new Vector3(
                Input.GetAxis("Horizontal"),
                0,
                Input.GetAxis("Vertical")
            );

            //Debug.Log($"vel {velocity}");
            //Debug.Log($"input {Input.GetAxis("Horizontal")} {Input.GetAxis("Vertical")}");
            //Debug.Log($"trans dir: {directionReference.TransformDirection(velocity)} trans vect: {directionReference.TransformVector(velocity)}");

            Vector3 directedVelocity = directionReference.TransformDirection(velocity) * speed;
            cc.SimpleMove(directedVelocity);

            if (velocity.sqrMagnitude > 0)
            {
                Vector3 direction = directionReference.forward;
                direction.y = 0;
                transform.forward = Vector3.Lerp(transform.forward, direction, 0.2f);
            }

            onInput.Invoke(directedVelocity);
        }
    }
}