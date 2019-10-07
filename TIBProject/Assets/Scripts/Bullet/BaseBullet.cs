using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    BulletType bulletType;

    Rigidbody rigid;

    Vector3 position;

    float power = 10;

    bool isLife;
    bool isCamera = false;

    void Awake() {
        rigid = GetComponent<Rigidbody>();
    }

    void Start()
    {
        rigid.AddForce(transform.TransformDirection(Vector3.forward) * power);

        //StartCoroutine(CameraShowMe());
    }

    void Update() {
        position = transform.position;

        if(Mathf.Abs(position.x - 64) - 64 > 10 || Mathf.Abs(position.z - 64) - 64 > 10 || position.y < -10) {
            CameraMNG.I.RollBackCamera();
            Destroy(gameObject);
        }
    }

    public void Create(BulletType bulletType, float power, bool isCamera = false) {
        this.bulletType = bulletType;
        isLife = true;
        this.power = power;
        this.isCamera = isCamera;
        if(isCamera)
            CameraMNG.I.ShowBullet(transform);
    }

    void OnTriggerEnter(Collider coll) {
        if(coll.transform.tag == "Block" && isLife) {
            isLife = false;
            Instantiate(bulletType.bulletBoomPrefab, transform.position, Quaternion.identity);
            if(isCamera)
                CameraMNG.I.RollBackCamera();
            Destroy(gameObject);
        }
    }

    IEnumerator CameraShowMe() {
        yield return new WaitForSeconds(0.3f);

        CameraMNG.I.ShowBullet(transform);

        StopCoroutine(CameraShowMe());
    }

    void OnDestroy() {
        if(Random.Range(0, 2) == 1)
            GameMNG.I.RequestCreateItem(transform.position);

        GameMNG.I.ChangeTurn();
    }
}
