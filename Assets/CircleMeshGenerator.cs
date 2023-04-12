using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class CircleMeshGenerator : MonoBehaviour
{
    public float radius = 1f;
    public int numSegments = 32;

    private Mesh mesh;

    private void Awake()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    private void Start()
    {
        GenerateMesh();
    }

    private void GenerateMesh()
    {
        Vector3[] vertices = new Vector3[numSegments + 1];
        int[] triangles = new int[numSegments * 3];

        vertices[0] = Vector3.zero;
        for (int i = 1; i < vertices.Length; i++)
        {
            float angle = (float)(i - 1) / numSegments * 2 * Mathf.PI;
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;
            vertices[i] = new Vector3(x, 0f, z);
        }

        for (int i = 0; i < numSegments; i++)
        {
            triangles[i * 3] = 0;
            triangles[i * 3 + 1] = i + 1;
            triangles[i * 3 + 2] = i + 2 <= numSegments ? i + 2 : 1;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }
}
