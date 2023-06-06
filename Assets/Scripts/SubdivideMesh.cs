using UnityEngine;

public class SubdivideMesh : MonoBehaviour
{
    public int subdivisionLevel = 1; // Number of subdivisions to apply
    public float caveIntensity = 0.5f; // Intensity of vertex deformation

    void Start()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter != null)
        {
            Mesh originalMesh = meshFilter.sharedMesh;
            if (originalMesh != null)
            {
                Mesh subdividedMesh = subdivideMesh(originalMesh, subdivisionLevel);
                DeformMesh(subdividedMesh, caveIntensity);
                meshFilter.sharedMesh = subdividedMesh;
            }
        }
    }

    Mesh subdivideMesh(Mesh originalMesh, int subdivisions)
    {
        Mesh subdividedMesh = originalMesh;
        for (int i = 0; i < subdivisions; i++)
        {
            subdividedMesh = SubdivideOnce(subdividedMesh);
        }
        return subdividedMesh;
    }

    Mesh SubdivideOnce(Mesh mesh)
    {
        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;

        int originalTriangleCount = triangles.Length / 3;
        int newTriangleCount = originalTriangleCount * 4;
        int[] newTriangles = new int[newTriangleCount * 3];

        Vector3[] newVertices = new Vector3[vertices.Length + originalTriangleCount * 3];
        System.Array.Copy(vertices, newVertices, vertices.Length);

        int currentIndex = vertices.Length;

        for (int i = 0; i < originalTriangleCount; i++)
        {
            int baseIndex = i * 3;

            int v0 = triangles[baseIndex];
            int v1 = triangles[baseIndex + 1];
            int v2 = triangles[baseIndex + 2];

            int mid01 = GetMidpointIndex(v0, v1, ref newVertices, ref currentIndex);
            int mid12 = GetMidpointIndex(v1, v2, ref newVertices, ref currentIndex);
            int mid20 = GetMidpointIndex(v2, v0, ref newVertices, ref currentIndex);

            int newIndex = i * 12;

            // Triangle 1
            newTriangles[newIndex] = v0;
            newTriangles[newIndex + 1] = mid01;
            newTriangles[newIndex + 2] = mid20;

            // Triangle 2
            newTriangles[newIndex + 3] = mid01;
            newTriangles[newIndex + 4] = v1;
            newTriangles[newIndex + 5] = mid12;

            // Triangle 3
            newTriangles[newIndex + 6] = mid20;
            newTriangles[newIndex + 7] = mid12;
            newTriangles[newIndex + 8] = v2;

            // Triangle 4
            newTriangles[newIndex + 9] = mid01;
            newTriangles[newIndex + 10] = mid12;
            newTriangles[newIndex + 11] = mid20;
        }

        Mesh subdividedMesh = new Mesh();
        subdividedMesh.vertices = newVertices;
        subdividedMesh.triangles = newTriangles;
        subdividedMesh.RecalculateNormals();

        return subdividedMesh;
    }

    void DeformMesh(Mesh mesh, float intensity)
    {
        Vector3[] vertices = mesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 vertex = vertices[i];
            //Random.InitState(Mathf.CeilToInt(vertex.x * vertex.y * vertex.z));
            Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * intensity; // Generate a random offset within a sphere

            vertices[i] = vertex + randomOffset;
        }

        mesh.vertices = vertices;
        mesh.RecalculateNormals();
    }
    int GetMidpointIndex(int indexA, int indexB, ref Vector3[] vertices, ref int currentIndex)
    {
        Vector3 pointA = vertices[indexA];
        Vector3 pointB = vertices[indexB];
        Vector3 midpoint = (pointA + pointB) * 0.5f;

        vertices[currentIndex] = midpoint;

        return currentIndex++;
    }
}
