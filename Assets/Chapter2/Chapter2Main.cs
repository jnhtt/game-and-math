using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter2
{
    public class Chapter2Main : MonoBehaviour
    {
        private const float SPEED = 4f;

        [SerializeField] private Chapter2UI ui;

        [SerializeField] private Color positiveSideColor;
        [SerializeField] private Color negativeSideColor;

        [SerializeField] private Transform target;
        [SerializeField] private Transform mover;

        [SerializeField] private Material hyperPlaneMat;

        private float dot;
        private float signedDistance;
        private bool targetRotateFlag = false;

        //y=0のXZ平面
        private Plane groundPlane = new Plane(Vector3.up, 0f);

        private void Update()
        {
            UpdateMove();
            UpdateDir();

            Calculate();
            UpdateUI();
        }

        private void UpdateMove()
        {
            Vector3 dir = Vector3.zero;
            if (Input.GetKey(KeyCode.W))
            {
                dir.z += 1f;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                dir.z -= 1f;
            }
            if (Input.GetKey(KeyCode.A))
            {
                dir.x -= 1f;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                dir.x += 1f;
            }
            mover.position += Time.deltaTime * SPEED * dir;
        }

        private void UpdateDir()
        {
            Vector3 mousePos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            if (groundPlane.Raycast(ray, out float d))
            {
                Vector3 posOnGround = ray.origin + d * ray.direction;
                Vector3 dir = posOnGround - mover.position;
                dir.Normalize();
                Quaternion rot = LookAtY(dir);
                mover.rotation = rot;
            }

            if (targetRotateFlag)
            {
                target.Rotate(Vector3.up, 5f * Time.deltaTime);
            }
        }

        public void SwitchTargetRotate()
        {
            targetRotateFlag = !targetRotateFlag;
        }


        private Quaternion LookAtY(Vector3 lookAtDir)
        {
            lookAtDir.y = 0f;
            Quaternion rotation = Quaternion.LookRotation(lookAtDir);
            return Quaternion.Euler(0f, rotation.eulerAngles.y, 0f);

        }

        private void Calculate()
        {
            //UnityではZ方向が正面. Transform.forwardで正面の向きを取得
            dot = Vector3.Dot(mover.forward, target.forward);

            //targetのforwardを法線、targetのpositionを超平面上の点とする
            Vector3 planeNormal = target.forward;
            Vector3 pointOnPlane = target.position;

            //超平面からの符号距離
            signedDistance = Vector3.Dot(planeNormal, mover.position - pointOnPlane);

            hyperPlaneMat.SetColor("_BaseColor", signedDistance < 0f ? negativeSideColor : positiveSideColor);
        }

        private void UpdateUI()
        {
            ui.SetDotValueText(dot.ToString("F2"));

            bool isSameDirection = 0f <= dot;
            ui.SetDirectionText(isSameDirection ? "Same direction" : "Different direction");

            bool isNegative = signedDistance < 0f;
            ui.SetSideText(isNegative ? "Negative" : "Positive");

            ui.SetBackAttackText(isSameDirection && isNegative ? "Available back attack" : "Unavailable back attack");
        }
    }
}
