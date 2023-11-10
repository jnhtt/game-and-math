using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolarCoodinatesCircle : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private float angleSpeed;

    private Vector3 center;
    private float angle;

    private void Awake()
    {
        center = transform.position;
    }

    private void Update()
    {
        angle += angleSpeed * Time.deltaTime;
        var p = Vector3.zero;
        p.x = center.x + radius * Mathf.Cos(Mathf.Deg2Rad * angle);
        p.z = center.z + radius * Mathf.Sin(Mathf.Deg2Rad * angle);
        transform.position = p;
    }
}
