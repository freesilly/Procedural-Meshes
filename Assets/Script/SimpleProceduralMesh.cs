
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class SimpleProceduralMesh : MonoBehaviour
{
    private void OnEnable()
    {
        var mesh = new Mesh { 
            name = "Procedural Mesh"
        };//定义网格

        mesh.vertices = new Vector3[] {
            Vector3.zero, Vector3.right, Vector3.up, new Vector3(1f, 1f)
        };//定义网格的顶点

        mesh.triangles = new int[] {0, 2, 1, 1, 2, 3};//定义三角形索引（连线顺序）

        mesh.normals = new Vector3[]{
            Vector3.back, Vector3.back, Vector3.back, Vector3.back
        };//法线

        mesh.uv = new Vector2[] {
            Vector2.zero, Vector2.right, Vector2.up,Vector2.one
        };//纹理坐标

        mesh.tangents = new Vector4[] {
            new Vector4(1f, 0f, 0f, -1f),
            new Vector4(1f, 0f, 0f, -1f),
            new Vector4(1f, 0f, 0f, -1f),
            new Vector4(1f, 0f, 0f, -1f)
        };//切线

        GetComponent<MeshFilter>().mesh = mesh;
    }
}
