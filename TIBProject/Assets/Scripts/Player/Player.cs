using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{   
    public float walkSpeed = 10f;
    public float jumpPower = 10f;
    public float tempJumpPower = 1f;
    bool isJumpAble, isJump;

    Rigidbody rigid;
    Vector3 position;   

    public Transform playerCamera;
    public Transform playerLook; 

    bool isKeepMouseDown;
    bool isTurn = false;

    string bulletName = "BaseBullet";

    void Awake() {
        rigid = GetComponent<Rigidbody>();
    }

    void Start() {
        CameraMNG.I.SetPlayer(transform);
        UIMNG.I.SetTurn(isTurn);
    }

    public void ChangeTurn() {
        isTurn = !isTurn;
        UIMNG.I.SetTurn(isTurn);
    }

    // Update is called once per frame
    void Update()
    {
        GameMNG.I.RequestPlayerInfo(transform.position, playerLook.rotation);

        playerLook.rotation = Quaternion.Euler(0, playerCamera.localRotation.eulerAngles.y, 0);
        position = playerLook.TransformDirection(position);

        if(transform.position.y <= -3)
            GameMNG.I.RequestDamage(100);

        if(!isTurn)
            return ;
            
        position = Vector3.zero;

        if(Input.GetKey(KeyCode.W))
            position += Vector3.forward;
        if(Input.GetKey(KeyCode.S))
            position += Vector3.back;
        if(Input.GetKey(KeyCode.A))
            position += Vector3.left;
        if(Input.GetKey(KeyCode.D))
            position += Vector3.right;

        playerLook.rotation = Quaternion.Euler(0, playerCamera.localRotation.eulerAngles.y, 0);
        position = playerLook.TransformDirection(position);

        if(Input.GetMouseButtonDown(0))
            StartCoroutine(KeepMouseDown());
        if(Input.GetMouseButtonUp(0)) {
            isKeepMouseDown = false;
            StopCoroutine(KeepMouseDown());
            //BulletMNG.I.Shoot("BaseBullet", transform.position + Vector3.up, playerCamera.rotation, UIMNG.I.GetPower() * 400);
            GameMNG.I.RequestBulletInfo(transform.position + Vector3.up + Vector3.forward, playerCamera.rotation, UIMNG.I.GetPower() * 400, bulletName);
            bulletName = "BaseBullet";
        }
        //   BulletMNG.I.Shoot("BaseBullet", transform.position + Vector3.up, playerCamera.rotation, 3600);
        
        if(Input.GetKeyDown(KeyCode.Space) && isJumpAble) {
            isJump = true;
            isJumpAble = false;
        }

        transform.Translate(position * walkSpeed * Time.deltaTime);
    }

    void FixedUpdate() {
        if(isJump) {
            rigid.velocity = Vector3.zero;
            rigid.AddForce(Vector3.up * jumpPower * tempJumpPower);
            isJump = false;
        }
        else if(rigid.velocity.y == 0 && !isJumpAble)
            isJumpAble = true;

        if(tempJumpPower != 1f)
            tempJumpPower = 1f;
    }

    IEnumerator KeepMouseDown() {
        isKeepMouseDown = true;
        while(isKeepMouseDown) {
            UIMNG.I.PowerUp();
            yield return new WaitForSeconds(0.05f);
        }
    }

    void OnTriggerEnter(Collider coll) {
        if(coll.transform.tag == "Boom") {
            if(coll.transform.localScale.x == 7)
                GameMNG.I.RequestDamage(10);
            GameMNG.I.RequestDamage(10);
        }  
    }
    void OnCollisionEnter(Collision coll) {
        if(coll.transform.tag == "Item") {
            isJump = true;
            isJumpAble = false;
            tempJumpPower = 3f;
            bulletName = "PowerBullet";
            GameMNG.I.RequestRemoveItem(coll.gameObject.GetComponent<Item>().itemID);
            UIMNG.I.ItemShow("Power UP");
            GameMNG.I.RequestDamage(-10);
        }     
    }
}
