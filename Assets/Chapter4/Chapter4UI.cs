using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter4
{
    public class Chapter4UI : MonoBehaviour
    {
        [SerializeField] private List<RectTransform> nodeList;
        [SerializeField] private List<RectTransform> nameList;
        [SerializeField] private List<Edge> edgeList;

        private Dictionary<int, List<int>> adjacencyDict;
        private List<int> tournamentPosList;

        public void PlayEdge(int s, int e, Action onFinish)
        {
            foreach (var edge in edgeList)
            {
                if (edge.start == s && edge.end == e)
                {
                    edge.Play(onFinish);
                }
            }
        }

        public void Start()
        {
            //ABCDEの勝ち数
            tournamentPosList = new List<int>() { 0, 0, 0, 0, 0, 0 };

            //隣接リスト
            adjacencyDict = new Dictionary<int, List<int>>();
            adjacencyDict.Add(0, new List<int>() { 0, 6, 10 });
            adjacencyDict.Add(1, new List<int>() { 1, 6, 10 });
            adjacencyDict.Add(2, new List<int>() { 2, 7, 9, 10 });
            adjacencyDict.Add(3, new List<int>() { 3, 7, 9, 10 });
            adjacencyDict.Add(4, new List<int>() { 4, 8, 9, 10 });
            adjacencyDict.Add(5, new List<int>() { 5, 8, 9, 10 });
        }

        public void Win(int index)
        {
            if (0 <= index && index < tournamentPosList.Count)
            {
                var winCount = tournamentPosList[index];
                if (!TryGetPos(index, winCount, out int prevPos))
                {
                    prevPos = index;
                }
                winCount++;
                tournamentPosList[index] = winCount;
                bool success = false;
                if (TryGetPos(index, winCount, out int pos))
                {
                    success = true;
                }
                PlayEdge(prevPos, pos, () =>
                {
                    if (success)
                    {
                        var node = nodeList[pos];
                        var name = nameList[index];
                        name.position = node.position;
                    }
                });
            }
        }

        private bool TryGetPos(int index, int winCount, out int pos)
        {
            pos = index;//初期位置
            if (adjacencyDict.TryGetValue(index, out var list))
            {
                if (list != null && 0 <= winCount && winCount < list.Count)
                {
                    pos = list[winCount];
                    return true;
                }
            }
            return false;
        }

        public void Reset()
        {
            tournamentPosList = new List<int>() { 0, 0, 0, 0, 0, 0 };
            for (int i = 0; i < nameList.Count; i++)
            {
                var name = nameList[i];
                var node = nodeList[i];
                name.position = node.position;
            }
            foreach (var edge in edgeList)
            {
                edge.Reset();
            }
        }

    }
}
