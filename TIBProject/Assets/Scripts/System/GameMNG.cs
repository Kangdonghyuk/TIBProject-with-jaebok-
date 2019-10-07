using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMNG : MonoBehaviour
{
    public static GameMNG I;
    public BulletMNG bulletMNG;
    public ItemMNG itemMNG;
    public UIMNG uiMNG;

    ServerOrClient serverOrClient;

    bool isServer;

    public GameObject enemy;
    public Player player;

    void Awake() {
        I = this;
        isServer = SystemMNG.I.isServer;
        serverOrClient = null;
        if(isServer) {
            serverOrClient = new ServerMNG();
            //serverOrClient = GetComponent<ServerMNG>();
        }
        else {
            serverOrClient = new ClientMNG();
            //serverOrClient = GetComponent<ClientMNG>();
        }
    }

    void Start() {
        serverOrClient.On(SystemMNG.I.ip, SystemMNG.I.port);
        if(isServer == true) {
            player.ChangeTurn();
        }
        else {
            player.transform.position = enemy.transform.position;
        }
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.T))
            ChangeTurn();
    }

    public void ChangeTurn() {
        player.ChangeTurn();
    }

    public void SetServerOrClient(bool isServer) {
        if(isServer)
            serverOrClient = GetComponent<ServerMNG>();
        else
            serverOrClient = GetComponent<ClientMNG>();
        //serverOrClient.On("sdf", 8000);
    }
    public void Dis() {
        if(serverOrClient != null)
            serverOrClient.Dis();
    }

    public void RequestPlayerInfo(Vector3 position, Quaternion rotation) {
        if(serverOrClient != null)
            serverOrClient.RequestTankInfo(position, rotation);
    }

    public void ResponseEnemyInfo(Vector3 position, Quaternion rotation) {
        enemy.transform.position = position;
        enemy.transform.rotation = rotation;
    }

    public void RequestBulletInfo(Vector3 position, Quaternion rotation, int power, string bulletName) {
        BulletMNG.I.Shoot(bulletName, position, rotation, power, true);
        if(serverOrClient != null)
            serverOrClient.RequestBulletInfo(position, rotation, power, bulletName);
    }
    
    public void ResponseBulletInfo(Vector3 position, Quaternion rotation, int power, string bulletName) {
        BulletMNG.I.Shoot(bulletName, position, rotation, power);
    }

    public void RequestDamage(int damage) {
        UIMNG.I.DamageMe(damage);
        if(serverOrClient != null)
            serverOrClient.RequestDamage(damage);
        if(UIMNG.I.lifeBar.myLife == 0)
            UIMNG.I.Finish(false);
    }

    public void ResponseDamage(int damage) {
        UIMNG.I.DamageEnemy(damage);
        if(UIMNG.I.lifeBar.enLife == 0)
            UIMNG.I.Finish(true);
    }

    public void RequestCreateItem(Vector3 pos) {
        int itemID = itemMNG.CreateItemBox(pos);
        if(serverOrClient != null)
            serverOrClient.RequestCreateItem(pos, itemID);
    }
    public void ResponseCreateItem(Vector3 pos, int itemID) {
        itemMNG.CreatedItemBox(pos, itemID);
    }
    public void RequestRemoveItem(int itemID) {
        itemMNG.RemoveItemBox(itemID);
        if(serverOrClient != null)
            serverOrClient.RequestRemoveItem(itemID);
    }
    public void ResponseRemoveItem(int itemID) {
        itemMNG.RemoveItemBox(itemID);
    }

}
