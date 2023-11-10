using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter4
{
    public class Edge : MonoBehaviour
    {
        public int start;
        public int end;
        public List<Frame> frameList;

        private GameObject parent;
        public void SetParent(GameObject g)
        {
            parent = g;
        }

        public void Play(Action onFinish)
        {
            StartCoroutine(PlayCoroutine(onFinish));
        }

        private IEnumerator PlayCoroutine(Action onFinish)
        {
            foreach (var frame in frameList)
            {
                yield return frame.PlayCoroutine(2f);
            }
            onFinish?.Invoke();
        }

        public void Reset()
        {
            foreach (var frame in frameList)
            {
                frame.Reset();
            }
        }
    }
}
