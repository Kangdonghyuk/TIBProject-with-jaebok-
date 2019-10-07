using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBoom : MonoBehaviour
{
    public GameObject sound;

    void Start()
    {
        Instantiate(sound, transform.position, transform.rotation);
        StartCoroutine(WillDestory());
    }

    void OnTriggerEnter(Collider coll) {
        if(coll.tag == "Block") {
            MapCreator.I.RemoveBlock(coll.transform.position, true);
            //Destroy(coll.gameObject);
        }
    }

    IEnumerator WillDestory() {
        yield return new WaitForSeconds(0.3f);

        Destroy(gameObject);
    }
}
