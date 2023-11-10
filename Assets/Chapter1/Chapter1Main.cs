using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Chapter1
{
    public class Chapter1Main : MonoBehaviour
    {
        [SerializeField] private float cubeSpeed;
        [SerializeField] private Vector3 direction;
        [SerializeField] private Transform cubeTransform;
        [SerializeField] private Transform originTransform;
        [SerializeField] private Transform redTransform;
        [SerializeField] private Transform greenTransform;
        [SerializeField] private Transform blueTransform;
        [SerializeField] private Transform yellowTransform;

        private Vector3 goalPos;

        private void SetPos(Transform target, Vector3 p)
        {
            direction = Vector3.zero;
            target.position = p;
        }

        private void SetDirection(Transform target, Vector3 p)
        {
            direction = p - target.position;
            direction.Normalize();
            goalPos = p;
        }

        private bool ShouldStop()
        {
            return Vector3.Distance(goalPos, cubeTransform.position) < 0.1f;
        }

        public void SetOriginPos()
        {
            SetPos(cubeTransform, originTransform.position);
        }
        public void SetRedPos()
        {
            SetPos(cubeTransform, redTransform.position);
        }
        public void SetGreenPos()
        {
            SetPos(cubeTransform, greenTransform.position);
        }
        public void SetBluePos()
        {
            SetPos(cubeTransform, blueTransform.position);
        }
        public void SetYellowPos()
        {
            SetPos(cubeTransform, yellowTransform.position);
        }

        public void SetOriginDir()
        {
            SetDirection(cubeTransform, originTransform.position);
        }
        public void SetRedDir()
        {
            SetDirection(cubeTransform, redTransform.position);
        }
        public void SetGreenDir()
        {
            SetDirection(cubeTransform, greenTransform.position);
        }
        public void SetBlueDir()
        {
            SetDirection(cubeTransform, blueTransform.position);
        }
        public void SetYellowDir()
        {
            SetDirection(cubeTransform, yellowTransform.position);
        }

        private void Update()
        {
            if (ShouldStop())
            {
                direction = Vector3.zero;
            }
            cubeTransform.position += Time.deltaTime * cubeSpeed * direction;
        }
    }
}
