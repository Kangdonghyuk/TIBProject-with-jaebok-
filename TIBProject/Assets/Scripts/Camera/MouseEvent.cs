using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseEvent : MonoBehaviour
{
    public GameObject boomPrefab;

    void Update() {
        return ;
        if(Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

            if(Physics.Raycast(ray, out hit, 1000.0f)) {
                Vector3 blockPos = hit.transform.position;

                if(blockPos.y <= 0)
                    return ;


                Instantiate(boomPrefab, blockPos, Quaternion.identity);
                //MapCreator.I.RemoveBlock(blockPos, true);
                //MapCreator.I.CreateBlock((int)blockPos.x, (int)blockPos.y+1, (int)blockPos.z, true);
            }
        }
    }
}
