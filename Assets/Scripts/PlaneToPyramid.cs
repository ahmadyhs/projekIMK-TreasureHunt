using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class PlaneToPyramid : MonoBehaviour
{
    public float vertexY = -100f; // Y position of the added vertex

    void Start()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

        if (meshFilter != null && meshRenderer != null)
        {
            Mesh planeMesh = meshFilter.sharedMesh;
            if (planeMesh != null)
            {
                Mesh pyramidMesh = CreatePyramidMesh(planeMesh.bounds.size.x, planeMesh.bounds.size.z, vertexY);
                meshFilter.sharedMesh = pyramidMesh;

                // Enable backface culling
                meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                meshRenderer.receiveShadows = true;
                //meshRenderer.material.SetFloat("_CullMode", (float)UnityEngine.Rendering.CullMode.Back);
            }
        }
    }

    Mesh CreatePyramidMesh(float sizeX, float sizeZ, float yPosition)
    {
        Mesh mesh = new Mesh();

        // Calculate half sizes
        float halfSizeX = sizeX * 0.5f;
        float halfSizeZ = sizeZ * 0.5f;

        // Define the vertices of the pyramid
        Vector3[] vertices =
        {
            new Vector3(-halfSizeX, 0, -halfSizeZ),
            new Vector3(halfSizeX, 0, -halfSizeZ),
            new Vector3(halfSizeX, 0, halfSizeZ),
            new Vector3(-halfSizeX, 0, halfSizeZ),
            new Vector3(0, yPosition, 0), // Vertex at the specified Y position (upside down)
            new Vector3(-halfSizeX, 0, -halfSizeZ), // Duplicate of base vertex for the side
            new Vector3(halfSizeX, 0, -halfSizeZ), // Duplicate of base vertex for the side
            new Vector3(halfSizeX, 0, halfSizeZ), // Duplicate of base vertex for the side
            new Vector3(-halfSizeX, 0, halfSizeZ) // Duplicate of base vertex for the side
        };

        // Define the triangles of the pyramid
        int[] triangles =
        {
            1, 0, 2, // Base triangle 1
            0, 3, 2, // Base triangle 2
            5, 6, 4, // Side triangle 1
            6, 7, 4, // Side triangle 2
            7, 8, 4, // Side triangle 3
            8, 5, 4, // Side triangle 4
            //5, 6, 7, // Side triangle 1
            //7, 8, 5  // Side triangle 2
        };

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        return mesh;
    }
}
