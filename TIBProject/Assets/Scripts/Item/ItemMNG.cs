using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMNG : MonoBehaviour
{
    public GameObject item;
    public Vector3 pos;
    int itemCount = 0;

    List<Item> itemList = new List<Item>();

    public int CreateItemBox(Vector3 pos)
    {
        itemList.Add(Instantiate(item, pos, Quaternion.identity).GetComponent<Item>());
        itemList[itemList.Count-1].itemID = itemCount;
        itemCount+=1;
        return itemCount-1;
    }
    public void CreatedItemBox(Vector3 pos, int itemID) {
        itemCount = itemID;
        CreateItemBox(pos);
    }

    public void RemoveItemBox(int itemID) {
        for(int i=0; i<itemList.Count; i++) {
            if(itemList[i].itemID == itemID) {
                Destroy(itemList[i].gameObject);
                break;
            }
        }
    }
}
