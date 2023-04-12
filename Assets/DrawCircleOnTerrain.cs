using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCircleOnTerrain : MonoBehaviour
{
    public float radius = 5f;
    public float lineWidth = 0.2f;
    public int numSegments = 32;

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.useWorldSpace = false;
        lineRenderer.loop = true;
        DrawCircle();
    }

    void DrawCircle()
    {
        Vector3[] points = new Vector3[numSegments + 1];
        for (int i = 0; i <= numSegments; i++)
        {
            float angle = (float)i / (float)numSegments * 360f;
            float x = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;
            float y = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
            points[i] = new Vector3(x, y, 0f);
        }
        lineRenderer.positionCount = points.Length;
        lineRenderer.SetPositions(points);
    }

    void Update()
    {
        // Set circle position to character position
        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
    }
}