using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace JK.CanvasBrushes
{
    [RequireComponent(typeof(Text))]
    public class FpsText : MonoBehaviour
    {
        #region Inspector



        #endregion

        private void OnEnable()
        {
            StartCoroutine(CountFpsCoroutine());
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        private IEnumerator CountFpsCoroutine()
        {
            Text textUi = GetComponent<Text>();

            while (true)
            {
                textUi.text = $"{1 / Time.deltaTime:0}";
                yield return null;
            }
        }
    }
    
    [Serializable]
    public class UnityEventFpsText : UnityEvent<FpsText> { }
}