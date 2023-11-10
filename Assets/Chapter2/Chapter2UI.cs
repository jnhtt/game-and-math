using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Chapter2
{
    public class Chapter2UI : MonoBehaviour
    {
        [SerializeField] private TMPro.TextMeshProUGUI dotValueText;
        [SerializeField] private TMPro.TextMeshProUGUI directionText;
        [SerializeField] private TMPro.TextMeshProUGUI sideText;
        [SerializeField] private TMPro.TextMeshProUGUI backAttackText;

        public void SetDotValueText(string t) => dotValueText?.SetText(t);
        public void SetDirectionText(string t) => directionText?.SetText(t);
        public void SetSideText(string t) => sideText?.SetText(t);
        public void SetBackAttackText(string t) => backAttackText?.SetText(t);
    }
}
