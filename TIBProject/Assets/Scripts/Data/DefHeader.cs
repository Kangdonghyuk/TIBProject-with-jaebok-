using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct vec3 {
    public int x, y, z;
}

public static class VoxelData {
    public static readonly int textureAtlasSizeInBlocks = 4;
    public static float normalizedBlockTextureSize {
        get { return 1f / (float)textureAtlasSizeInBlocks; }
    }

    public static readonly Vector3[] voxelVerts = new Vector3[8] { 
        new Vector3(-0.5f, -0.5f, -0.5f),   // 0
        new Vector3(0.5f, -0.5f, -0.5f),   // 1
        new Vector3(0.5f, 0.5f, -0.5f),   // 2
        new Vector3(-0.5f, 0.5f, -0.5f),   // 3
        new Vector3(-0.5f, -0.5f, 0.5f),   // 4
        new Vector3(0.5f, -0.5f, 0.5f),   // 5
        new Vector3(0.5f, 0.5f, 0.5f),   // 6
        new Vector3(-0.5f, 0.5f, 0.5f)   // 7
    };

    public static readonly int[,] voxelTris = new int[6,4] {
        //Back, Front, Top, Bottom, Left, Right

        {0, 3, 1, 2}, // Back
        {5, 6, 4, 7}, // Front
        {3, 7, 2, 6}, // Top
        {1, 5, 0, 4}, // Bottom
        {4, 7, 0, 3}, // Left
        {1, 2, 5, 6}  // Right
    };

    public static readonly Vector3[] faceChecks = new Vector3[6] {
        new Vector3(0f, 0f, -1f),
        new Vector3(0f, 0f, 1f),
        new Vector3(0f, 1f, 0f),
        new Vector3(0f, -1f, 0f),
        new Vector3(-1f, 0f, 0f),
        new Vector3(1f, 0f, 0f)
    };

    public static readonly Vector2[] voxelUvs = new Vector2[4] {
        new Vector2(0f, 0f),
        new Vector2(0f, 1f),
        new Vector2(1f, 0f),
        new Vector2(1f, 1f)
    };
}
