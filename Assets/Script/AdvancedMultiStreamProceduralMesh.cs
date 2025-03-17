using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using Unity.Rendering;
using UnityEngine.Rendering;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class AdvancedMultiStreamProceduralMesh : MonoBehaviour
{
    private void OnEnable()
    {
        int vertexArrtributeCount = 4;
        int vertexCount = 4;
        
        Mesh.MeshDataArray meshDataArray = Mesh.AllocateWritableMeshData(1);
        Mesh.MeshData meshData = meshDataArray[0];

        var vertexArrtributes = new NativeArray<VertexAttributeDescriptor>(
            vertexArrtributeCount, Allocator.Temp
        );

        vertexArrtributes[0] = new VertexAttributeDescriptor(dimension: 3);
        meshData.SetVertexBufferParams(vertexCount, vertexArrtributes);
        vertexArrtributes.Dispose();

        var mesh = new Mesh{
            name = "Procedural Mesh"
        };
        Mesh.ApplyAndDisposeWritableMeshData(meshDataArray, mesh);
        GetComponent<MeshFilter>().mesh = mesh;
    }
}
