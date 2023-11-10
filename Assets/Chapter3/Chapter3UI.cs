using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Chapter3
{
    public class Chapter3UI : MonoBehaviour
    {
        [SerializeField] private TMPro.TextMeshProUGUI interpolationValueText;
        public void SetInterpolation(string t) => interpolationValueText?.SetText(t);

        [SerializeField] private TMPro.TextMeshProUGUI targetValueText;
        public void SetTarget(string t) => targetValueText?.SetText(t);
    }
}
