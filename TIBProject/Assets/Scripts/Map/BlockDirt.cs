using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDirt : MonoBehaviour
{
    public Material material;
    MeshRenderer meshRenderer;
    MeshFilter meshFilter;

    int vertexIndex = 0;
    List<int> triangles = new List<int>();
    List<Vector3> vertices = new List<Vector3>();
    List<Vector2> uvs = new List<Vector2>();

    void Start()
    {
        meshFilter = gameObject.AddComponent<MeshFilter>();
        meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.material = material;

        for(int p = 0; p < 6; p++) {
            if(!CheckVoxel(transform.position + VoxelData.faceChecks[p])) {
                vertices.Add(VoxelData.voxelVerts[VoxelData.voxelTris[p, 0]]);
                vertices.Add(VoxelData.voxelVerts[VoxelData.voxelTris[p, 1]]);
                vertices.Add(VoxelData.voxelVerts[VoxelData.voxelTris[p, 2]]);
                vertices.Add(VoxelData.voxelVerts[VoxelData.voxelTris[p, 3]]);

                if(p == 2)
                    AddTexture(7);
                else if(p == 3)
                    AddTexture(1);
                else
                    AddTexture(2);

                triangles.Add(vertexIndex);
                triangles.Add(vertexIndex + 1);
                triangles.Add(vertexIndex + 2);
                triangles.Add(vertexIndex + 2);
                triangles.Add(vertexIndex + 1);
                triangles.Add(vertexIndex + 3);

                vertexIndex += 4;
            }
        }

        Mesh mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uvs.ToArray();

        mesh.RecalculateNormals();

        meshFilter.mesh = mesh;
    }

    bool IsVoxelInChunk(int x, int y, int z) {
        if(x < 0 || x >= MapCreator.I.mapSize.x ||
           y < 0 || y >= MapCreator.I.mapSize.y ||
           z < 0 || z >= MapCreator.I.mapSize.z)
           return false;
        else
            return true;
    }

    bool CheckVoxel(Vector3 pos) {
        int x = (int)pos.x;
        int y = (int)pos.y;
        int z = (int)pos.z;

        if(!IsVoxelInChunk(x, y, z))
            return false;

        return MapCreator.I.IsVisual(x, y, z);
    }

    void AddTexture(int textureID) {
        float y = textureID / VoxelData.textureAtlasSizeInBlocks;
        float x = textureID - (y * VoxelData.textureAtlasSizeInBlocks);

        x *= VoxelData.normalizedBlockTextureSize;
        y *= VoxelData.normalizedBlockTextureSize;

        y = 1f - y - VoxelData.normalizedBlockTextureSize;

        uvs.Add(new Vector2(x, y));
        uvs.Add(new Vector2(x, y + VoxelData.normalizedBlockTextureSize));
        uvs.Add(new Vector2(x + VoxelData.normalizedBlockTextureSize, y));
        uvs.Add(new Vector2(x + VoxelData.normalizedBlockTextureSize, y + VoxelData.normalizedBlockTextureSize));
    }
}
