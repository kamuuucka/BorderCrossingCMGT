using System.Collections.Generic;
using UnityEngine;

public class ComplexShapeMesh : MonoBehaviour
{
    public List<Vector2> points; // List of 2D points to define the shape

    void Start()
    {
        Mesh mesh = new Mesh();

        // Convert 2D points to 3D vertices
        Vector3[] vertices = new Vector3[points.Count];
        for (int i = 0; i < points.Count; i++)
        {
            vertices[i] = new Vector3(points[i].x, points[i].y, 0);
        }

        // Triangulate the shape (This requires a specific triangulation algorithm)
        int[] triangles = TriangulateShape(points);

        // Create UVs (Just a basic UV map for texture mapping)
        Vector2[] uvs = new Vector2[points.Count];
        for (int i = 0; i < points.Count; i++)
        {
            uvs[i] = points[i]; // Assuming the shape fits in the UV space (0,0) to (1,1)
        }

        // Assign to mesh
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;

        // Create MeshFilter and MeshRenderer if not already attached
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();

        // Apply the mesh
        meshFilter.mesh = mesh;

        // Add a material to render the shape
        meshRenderer.material = new Material(Shader.Find("Sprites/Default"));
    }

    // Simple triangulation function for convex polygons or simple shapes
    // For complex polygons, you may need a more advanced triangulation algorithm
    int[] TriangulateShape(List<Vector2> points)
    {
        List<int> indices = new List<int>();
        for (int i = 1; i < points.Count - 1; i++)
        {
            indices.Add(0); // First vertex of the triangle
            indices.Add(i);
            indices.Add(i + 1);
        }
        return indices.ToArray();
    }
}