using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class CircleRenderer : MonoBehaviour
{
    public float radius = 1f;
    public int segments = 32;
    public float lineWidth = 0.1f;
    public Vector3 offset;
    public bool update = false;

    private LineRenderer lineRenderer;

    private void Update()
    {
        if (!update) return;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

        DrawCircle();
    }
    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

        DrawCircle();
    }

    private void DrawCircle()
    {
        lineRenderer.positionCount = segments + 1;

        float anglePerSegment = 360f / segments;
        float currentAngle = 0f;

        for (int i = 0; i <= segments; i++)
        {
            float x = Mathf.Sin(Mathf.Deg2Rad * currentAngle) * radius;
            float z = Mathf.Cos(Mathf.Deg2Rad * currentAngle) * radius;

            Vector3 point = transform.position + new Vector3(x, 0f, z) + offset;
            lineRenderer.SetPosition(i, point);

            currentAngle += anglePerSegment;
        }
    }
    public void enable()
    {
        lineRenderer.enabled = true;
    }
}
