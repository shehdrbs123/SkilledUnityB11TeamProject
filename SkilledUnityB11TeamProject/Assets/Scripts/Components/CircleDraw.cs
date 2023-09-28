using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleDraw : MonoBehaviour
{
    public int vertexCount = 50; // 원을 구성하는 꼭짓점(버텍스)의 수
    public float radius = 5f; // 원의 반지름
    public LineRenderer lineRenderer; // Line Renderer 컴포넌트 참조

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = vertexCount+1;
    }

    private void Update()
    {
        // 원 위의 점들 계산 및 설정
        for (int i = 0; i < vertexCount+1; i++)
        {
            float angle = i * (360f / vertexCount); // 원 전체 각도를 나눠서 꼭짓점의 각도 계산
            float x = transform.position.x + Mathf.Sin(Mathf.Deg2Rad * angle) * radius; // x 좌표 계산
            float z = transform.position.z + Mathf.Cos(Mathf.Deg2Rad * angle) * radius; // z 좌표 계산
            lineRenderer.SetPosition(i, new Vector3(x, transform.position.y, z)); // 라인 렌더러의 꼭짓점 위치 설정
        }
    }
}
