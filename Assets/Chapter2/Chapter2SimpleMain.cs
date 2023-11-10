using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter2
{
    public class Chapter2SimpleMain : MonoBehaviour
    {
        private const float SPEED = 4f;

        [SerializeField] private Chapter2UI ui;

        [SerializeField] private Transform target;
        [SerializeField] private Transform mover;

        [SerializeField, Range(0f, 180f)] private float backAttackAngle;

        private float forwardDot;
        private float dot;
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
            Vector3 mover2targetDir = target.position - mover.position;
            mover2targetDir.Normalize();
            dot = Vector3.Dot(mover2targetDir, target.forward);

            forwardDot = Vector3.Dot(target.forward, mover.forward);
        }

        private bool IsBackAttack(float dot, float fdot)
        {
            //単位ベクトル同士の内積前提
            //dotとfdotをチェックして初めて現実的なバックアタック判定になる。
            //円状に広がる攻撃の場合はfdotを削除してもOK
            float cos = Mathf.Cos(Mathf.Deg2Rad * backAttackAngle);
            Debug.Log(cos);
            return  cos <= dot && 0 <= fdot;
        }

        private void UpdateUI()
        {
            ui.SetDotValueText(dot.ToString("F2"));
            ui.SetDirectionText(0f <= forwardDot ? "Same direction" : "different direction");
            ui.SetBackAttackText(IsBackAttack(dot, forwardDot) ? "Available back attack" : "Unavailable back attack");
        }
    }
}
