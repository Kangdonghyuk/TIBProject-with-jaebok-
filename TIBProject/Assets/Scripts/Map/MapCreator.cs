using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockType {
    AIR = 0,
    DIRT = 1,
    LENGTH
}

public class BlockStructure {
    public Transform transtorm;
    public BlockType blockType = BlockType.AIR;
    public bool isVisual = false;
    public BlockStructure(Transform transform = null, BlockType blockType = BlockType.AIR, bool isVisual = false, Transform parent = null) {
        this.transtorm = transform;
        this.blockType = blockType;
        this.isVisual = isVisual;
        if(parent != null)
            transform.SetParent(parent);
    }
    public void SetParent(Transform parent) {
        transtorm.SetParent(parent);
    }
    public GameObject GetGameObject() {
        return transtorm.gameObject;
    }
}

public class MapCreator : MonoBehaviour
{
    public static MapCreator I;
    public vec3 mapSize;
    public GameObject[] blockPrefabList = new GameObject[(int)BlockType.LENGTH];
    public BlockStructure[,,] blockList;
    public int createOffsetY = 10;

    public float waveLength, amplitude;

    void Awake() {
        I = this;
    }

    void Start()
    {
        blockList = new BlockStructure[mapSize.x, mapSize.y, mapSize.z];

        float seedX = 50f;
        float seedZ = 50f;
        
        for(int x = 0; x < mapSize.x; x++) {
            for(int z = 0; z < mapSize.z; z++) {
                float coordX = (x + seedX) / waveLength;
                float coordZ = (z + seedZ) / waveLength;
                int y = (int)(Mathf.PerlinNoise(coordX, coordZ) * amplitude + createOffsetY);
                //int y = 5;

                CreateBlock(x, y, z, true, BlockType.DIRT);
                
                int offsetY = y;
                while(y > 0) {
                    y--;
                    CreateBlock(x, y, z, false, BlockType.DIRT);
                }
            }
        }
    }

    public bool IsVisual(int x, int y, int z) {
        if(blockList[x, y, z] == null)
            return false;
        return blockList[x, y, z].isVisual;
    }

    public void CreateBlock(int x, int y, int z, bool isVisual = false, BlockType blockType = BlockType.AIR) {

        if(isVisual == true) {
            blockList[x, y, z] = new BlockStructure(
                ((GameObject)Instantiate(blockPrefabList[(int)blockType],
            new Vector3(x, y, z), Quaternion.Euler(-90f, 0f, 0f))).transform, blockType, isVisual, transform);
        }
        else {
            blockList[x, y, z] = new BlockStructure(null, BlockType.DIRT);
            //blockList[(int)position.x, (int)position.y, (int)position.z].transform.parent = transform;
        }
    }

    bool ValidPos(int x, int y, int z) {
        if(x < 0 || x >= mapSize.x)
            return false;
        if(y < 0 || y >= mapSize.y)
            return false;
        if(z < 0 || z >= mapSize.z)
            return false;

        return true;
    }
    bool ValidPos(Vector3 pos) {
        return ValidPos((int)pos.x, (int)pos.y, (int)pos.z);
    }
    public void RemoveBlock(Vector3 pos, bool isCreateAround) {
        if(ValidPos(pos) == true) {
            int x = (int)pos.x, y = (int)pos.y, z = (int)pos.z;
            if(blockList[x, y, z].isVisual == true) {
                Destroy(blockList[x, y, z].transtorm.gameObject);
                blockList[x, y, z] = null;

                if(isCreateAround == true)
                    CreateAroundBlock(pos);
            }
        }
    }
    public void CreateAroundBlock(Vector3 pos) {
        for(int offsetX = -1; offsetX <= 1; offsetX++) {
            for(int offsetY = -1; offsetY <= 1; offsetY++) {
                for(int offsetZ = -1; offsetZ <= 1; offsetZ++) {
                    int x = offsetX + (int)pos.x, y = offsetY + (int)pos.y, z = offsetZ + (int)pos.z;
                    if(offsetX == 0 && offsetY == 0 && offsetZ == 0)
                        continue ;
                    if(Mathf.Abs(offsetX) + Mathf.Abs(offsetY) + Mathf.Abs(offsetZ) > 1)
                        continue ;
                    if(ValidPos(x, y, z) == false || blockList[x, y, z] == null || blockList[x, y, z].isVisual == true)
                        continue ;
                    CreateBlock(x, y, z, true, BlockType.DIRT);
                }
            }
        }
    }
}
