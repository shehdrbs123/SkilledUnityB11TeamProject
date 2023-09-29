using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeDraw : MonoBehaviour
{
    public int vertexCount = 50; 
    public float radius = 5f;
    public LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = vertexCount+1;
    }

    private void Update()
    {
        for (int i = 0; i < vertexCount+1; i++)
        {
            float angle = i * (360f / vertexCount);
            float x = transform.position.x + Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            float z = transform.position.z + Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
            lineRenderer.SetPosition(i, new Vector3(x, transform.position.y, z));
        }
    }
}
