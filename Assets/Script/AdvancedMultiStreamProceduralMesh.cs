using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using Unity.Rendering;
using UnityEngine.Rendering;
using Unity.Mathematics;

using static Unity.Mathematics.math;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class AdvancedMultiStreamProceduralMesh : MonoBehaviour
{
    private void OnEnable()
    {
        int vertexArrtributeCount = 4;//顶点属性数量
        int vertexCount = 4;//顶点数量
        int triangleIndexCount = 6;//三角形面片数量
        
        //可写的网格数据数组
        Mesh.MeshDataArray meshDataArray = Mesh.AllocateWritableMeshData(1);
        Mesh.MeshData meshData = meshDataArray[0];

        //原生数组来描述每个顶点的属性（Allocator是分配器，第三个参数用于初始化）
        var vertexArrtributes = new NativeArray<VertexAttributeDescriptor>(
            vertexArrtributeCount, Allocator.Temp, NativeArrayOptions.UninitializedMemory
        );

        //顶点坐标
        vertexArrtributes[0] = new VertexAttributeDescriptor(dimension: 3);
        //法线
        vertexArrtributes[1] = new VertexAttributeDescriptor(
        VertexAttribute.Normal, dimension: 3, stream: 1);
        //切线
        vertexArrtributes[2] = new VertexAttributeDescriptor(
        VertexAttribute.Tangent, dimension: 4, stream: 2);
        //纹理坐标
        vertexArrtributes[3] = new VertexAttributeDescriptor(
        VertexAttribute.TexCoord0, dimension: 2, stream: 3);

        //将顶点数量和顶点属性描述符传递给网格数据
        meshData.SetVertexBufferParams(vertexCount, vertexArrtributes);
        vertexArrtributes.Dispose();//释放内存


        //获取顶点位置
        NativeArray<float3> positions = meshData.GetVertexData<float3>();
        positions[0] = 0f;
        positions[1] = right();
        positions[2] = up();
        positions[3] = float3(1f, 1f, 0f);

        //设置法线
        NativeArray<float3> normals = meshData.GetVertexData<float3>(1);
        normals[0] = normals[1] = normals[2] = normals[3] = back();

        //设置切线
        NativeArray<float4> tangents = meshData.GetVertexData<float4>(2);
        tangents[0] = tangents[1] = tangents[2] = tangents[3] = float4(1f, 0f, 0f, -1f);

        //设置纹理坐标
        NativeArray<float2> texCoords = meshData.GetVertexData<float2>(3);
        texCoords[0] = 0f;
        texCoords[1] = float2(1f, 0f);
        texCoords[2] = float2(0f, 1f);
        texCoords[3] = 1f;

        //设置三角形索引
        meshData.SetIndexBufferParams(triangleIndexCount, IndexFormat.UInt32);
        NativeArray<uint> triangleIndices = meshData.GetIndexData<uint>();
        triangleIndices[0] = 0;
        triangleIndices[1] = 2;
        triangleIndices[2] = 1;
        triangleIndices[3] = 1;
        triangleIndices[4] = 2;
        triangleIndices[5] = 3;

        //设置网格边界
        var bounds = new Bounds(new Vector3(0.5f, 0.5f), new Vector3(1f, 1f));

        meshData.subMeshCount = 1;
        meshData.SetSubMesh(0, new SubMeshDescriptor(0, triangleIndexCount) {
            bounds = bounds,
            vertexCount = vertexCount
        }, MeshUpdateFlags.DontRecalculateBounds);

        var mesh = new Mesh{
            name = "Procedural Mesh"
        };

        //将可写的网格数据应用到新的网格对象中，并释放临时数据
        Mesh.ApplyAndDisposeWritableMeshData(meshDataArray, mesh);
        GetComponent<MeshFilter>().mesh = mesh;
    }
}
