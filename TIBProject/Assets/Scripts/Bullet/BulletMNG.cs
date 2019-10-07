using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMNG : MonoBehaviour
{
    public static BulletMNG I;

    public List<BulletType> bulletList = new List<BulletType>();
    public Hashtable bulletHash = new Hashtable();
    public Dictionary<string, BulletType> bulletDic = new Dictionary<string, BulletType>();

    void Awake() {
        I = this;
    }

    void Start() {
        for(int i=0; i < bulletList.Count; i++) {
            //bulletHash.Add(bulletList[i].bulletName, bulletList[i]);
            bulletDic.Add(bulletList[i].bulletName, bulletList[i]);
        }
    }

    public void Shoot(string bulletName, Vector3 position, Quaternion rotation, float power, bool isCamera = false) {
        //GameObject newBullet = Instantiate(bulletHash[bulletName])
        //Debug.Log(Quaternion.ToEulerAngles(rotation));
        GameObject newBullet = Instantiate(bulletDic[bulletName].bulletPrefab, position, rotation);
        //newBullet.transform.rotation = Camera.main.transform.rotation;
        newBullet.GetComponent<BaseBullet>().Create(bulletDic[bulletName], power, isCamera);
    }
}

[System.Serializable]
public struct BulletType {
    public string bulletName;
    public GameObject bulletPrefab;
    public GameObject bulletBoomPrefab;
}
