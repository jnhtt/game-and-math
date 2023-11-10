using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter3
{
    public class Missile : MonoBehaviour
    {
        public enum InterpolationType
        {
            BSpline,
            Linear,
        }
        private float speed;
        private float time;
        private bool launchFlag;

        private Vector3 start;
        private Vector3 mid;
        private Vector3 end;
        private Transform endTransform;
        private InterpolationType interpolationType;

        private Action<Vector3> onHit;

        public void SetInterpolationType(InterpolationType type)
        {
            interpolationType = type;
        }

        public void Launch(float sp, Vector3 p0, Vector3 p1, Vector3 p2, Action<Vector3> onHit)
        {
            this.onHit = onHit;
            speed = sp;
            time = 0f;
            launchFlag = true;

            start = p0;
            mid = p1;
            end = p2;
            endTransform = null;
        }

        public void Launch(float sp, Vector3 p0, Vector3 p1, Transform p2, Action<Vector3> onHit)
        {
            this.onHit = onHit;
            speed = sp;
            time = 0f;
            launchFlag = true;

            start = p0;
            mid = p1;
            end = p2.position;
            endTransform = p2;
        }

        private Vector3 GetPositionBSpline(Vector3 p0, Vector3 p1, Vector3 p2, float t)
        {
            t = Mathf.Clamp01(t);
            float t1 = (1f - t);
            float param1 = t1 * t1;
            float param2 = 2f * t1 * t;
            float param3 = t * t;

            return param1 * p0 + param2 * p1 + param3 * p2;
        }

        private Vector3 GetPositionLiear(Vector3 p0, Vector3 p1, Vector3 p2, float t)
        {
            float p0p1 = Vector3.Distance(p0, p1);
            float p1p2 = Vector3.Distance(p1, p2);
            float total = p0p1 + p1p2;
            float tt = t * total;
            if (0 <= tt && tt < p0p1)
            {
                t = Mathf.Clamp01(tt / p0p1);
                return (1f - t) * p0 + t * p1;
            }
            else
            {
                t = Mathf.Clamp01(tt / total);
                return (1f - t) * p1 + t * p2;
            }
        }

        private Vector3 GetPosition(Vector3 p0, Vector3 p1, Vector3 p2, float t)
        {
            switch (interpolationType)
            {
                case InterpolationType.BSpline:
                    return GetPositionBSpline(p0, p1, p2, t);
                case InterpolationType.Linear:
                    return GetPositionLiear(p0, p1, p2, t);
            }
            return GetPositionBSpline(p0, p1, p2, t);
        }

        private void Update()
        {
            if (launchFlag)
            {
                time = Mathf.Clamp01(time + speed * Time.deltaTime);
                //endTransformが設定されていたら追尾する
                end = endTransform ? endTransform.position : end;
                var pos = GetPosition(start, mid, end, time);
                UpdateTransform(pos);
                if (1f <= time)
                {
                    onHit?.Invoke(pos);
                    onHit = null;
                    launchFlag = false;
                    GameObject.Destroy(gameObject);
                }
            }
        }

        private void UpdateTransform(Vector3 nextPos)
        {
            var dir = nextPos - transform.position;
            if (dir.sqrMagnitude > 0f)
            {
                transform.rotation = Quaternion.LookRotation(dir.normalized);
            }
            transform.position = nextPos;
        }
    }
}
