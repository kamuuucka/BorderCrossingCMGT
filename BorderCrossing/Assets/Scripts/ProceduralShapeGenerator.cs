using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ProceduralShapeGenerator : MonoBehaviour
{
    private void OnEnable()
    {
        var mesh = new Mesh
        {
            name = "Procedural Mesh",
        };

        mesh.vertices = new Vector3[]
        {
            Vector3.zero, Vector3.right, Vector3.up,
            new Vector3(1.1f, 0f), new Vector3(0f, 1.1f), new Vector3(1.1f, 1.1f)
        };
        
        
        //Lighting
        mesh.normals = new Vector3[] {
            Vector3.back, Vector3.back, Vector3.back,
            Vector3.back, Vector3.back, Vector3.back
        };
        
        //Texture
        mesh.uv = new Vector2[] {
            Vector2.zero, Vector2.right, Vector2.up,
            Vector2.right, Vector2.up, Vector2.one
        };
        
        //Normal mapping
        mesh.tangents = new Vector4[] {
            new Vector4(1f, 0f, 0f, -1f),
            new Vector4(1f, 0f, 0f, -1f),
            new Vector4(1f, 0f, 0f, -1f),
            new Vector4(1f, 0f, 0f, -1f),
            new Vector4(1f, 0f, 0f, -1f),
            new Vector4(1f, 0f, 0f, -1f)
        };

        //Sequence of drawing
        mesh.triangles = new int[] {
            0, 2, 1, 3, 4, 5
        };
        
        GetComponent<MeshFilter>().mesh = mesh;
    }
}
