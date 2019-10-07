using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float speed = 10f;
    public int itemID;
 
    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.Rotate(Vector3.up * step);
    }

    void OnDestroy() {
        
    }
}
