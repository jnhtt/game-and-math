using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter3
{
    public class CircleMove : MonoBehaviour
    {
        [SerializeField] private float angleSpeed;
        [SerializeField] private float radius;

        private Vector3 origin;
        private Vector3 pos;
        private float timer;

        private void Awake()
        {
            origin = transform.position;
        }

        void Update()
        {
            timer += Time.deltaTime;
            pos.x = radius * Mathf.Cos(Mathf.Deg2Rad * angleSpeed * timer);
            pos.z = radius * Mathf.Sin(Mathf.Deg2Rad * angleSpeed * timer);
            transform.position = origin + pos;
        }
    }
}
