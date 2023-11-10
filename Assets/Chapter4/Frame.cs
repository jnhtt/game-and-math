using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Chapter4
{
    public class Frame : MonoBehaviour
    {
        [SerializeField] private List<Image> imageList;

        private void Start()
        {
            Reset();
        }

        public IEnumerator PlayCoroutine(float sp)
        {
            foreach (var i in imageList)
            {
                float t = 0;
                while (t < 1f)
                {
                    float delta = sp * Time.deltaTime;
                    t += delta;
                    i.fillAmount += delta;
                    yield return null;
                }
                i.fillAmount = 1f;
            }
        }

        public void Reset()
        {
            foreach (var i in imageList)
            {
                i.fillAmount = 0f;
            }
        }
    }
}
