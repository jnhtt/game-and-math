using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter3
{
    public class Chapter3Main : MonoBehaviour
    {
        private enum TargetType
        {
            Position,
            Transform,
        }
        [SerializeField] private Chapter3UI ui;

        [SerializeField] private Transform target;
        [SerializeField] private Transform launchPos;
        [SerializeField] private Transform aimMarker;

        [SerializeField] private Missile missilePrefab;
        [SerializeField] private GameObject explosionPrefab;

        //y=0のXZ平面
        private Plane groundPlane = new Plane(Vector3.up, 0f);

        private Missile.InterpolationType interpolationType = Missile.InterpolationType.BSpline;
        private TargetType targetType = TargetType.Position;

        private void Start()
        {
            interpolationType = Missile.InterpolationType.BSpline;
            ui.SetInterpolation("BSpline");

            targetType = TargetType.Position;
            ui.SetTarget("Position");
        }

        private void Update()
        {
            UpdateAimMark();
            UpdateLaunch();
        }

        private void UpdateLaunch()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Launch();
            }
        }

        private void Launch()
        {
            var startPos = launchPos.position;
            var endPos = aimMarker.position;
            var midPos = startPos + 6f * Vector3.up + new Vector3(UnityEngine.Random.Range(-2f, 2f), 0f, UnityEngine.Random.Range(-2f, 2f));

            var missile = GameObject.Instantiate(missilePrefab);
            missile.transform.position = launchPos.position;
            missile.SetInterpolationType(interpolationType);
            if (targetType == TargetType.Transform)
            {
                missile.Launch(2f, startPos, midPos, target, Explode);
            }
            else
            {
                missile.Launch(2f, startPos, midPos, endPos, Explode);
            }
        }

        private void Explode(Vector3 pos)
        {
            var explosion = GameObject.Instantiate(explosionPrefab);
            explosion.transform.position = pos;
        }

        private void UpdateAimMark()
        {
            Vector3 mousePos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            if (groundPlane.Raycast(ray, out float d))
            {
                Vector3 posOnGround = ray.origin + d * ray.direction;
                aimMarker.position = posOnGround;
            }
        }

        public void SwitchInterpolation()
        {
            switch (interpolationType)
            {
                case Missile.InterpolationType.BSpline:
                    interpolationType = Missile.InterpolationType.Linear;
                    ui.SetInterpolation("Linear");
                    break;
                case Missile.InterpolationType.Linear:
                    interpolationType = Missile.InterpolationType.BSpline;
                    ui.SetInterpolation("BSpline");
                    break;
            }
        }

        public void SwitchTarget()
        {
            switch (targetType)
            {
                case TargetType.Position:
                    targetType = TargetType.Transform;
                    ui.SetTarget("Transform");
                    break;
                case TargetType.Transform:
                    targetType = TargetType.Position;
                    ui.SetTarget("Position");
                    break;
            }
        }
    }
}
