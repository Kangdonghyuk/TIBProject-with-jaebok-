using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public interface ServerOrClient {
    void On(string ip, int port);
    void RequestTankInfo(Vector3 position, Quaternion rotation);
    void ResponseTankInfo(NetworkMessage msg);
    void RequestBulletInfo(Vector3 position, Quaternion rotation, int power, string bulletName);
    void ResponseBulletInfo(NetworkMessage msg);
    void RequestDamage(int damage);
    void ResponseDamage(NetworkMessage msg);
    void RequestCreateItem(Vector3 position, int itemID);
    void ResponseCreateItem(NetworkMessage msg);
    void RequestRemoveItem(int itemID);
    void ResponseRemoveItem(NetworkMessage msg);
    void Dis();
}

public class ServerMNG : ServerOrClient
{
    public bool isOnServer;
    public int port;

    int otherPlayerID;

    public void On(string ip, int port) {
        this.port = port;

        NetworkServer.RegisterHandler(MsgType.Ready, OnMessage);
        NetworkServer.RegisterHandler(NetData.CTS_TANK_INFO, ResponseTankInfo);
        NetworkServer.RegisterHandler(NetData.CTS_BULLET_INFO, ResponseBulletInfo);
        NetworkServer.RegisterHandler(NetData.CTS_DAMAGE, ResponseDamage);
        NetworkServer.RegisterHandler(NetData.CTS_CREATE_ITEM, ResponseCreateItem);
        NetworkServer.RegisterHandler(NetData.CTS_REMOVE_ITEM, ResponseRemoveItem);
        
        NetworkServer.Listen(8000);
    }

    public void OnMessage(NetworkMessage msg) {
        Debug.Log(msg);
    }

    public void RequestTankInfo(Vector3 position, Quaternion rotation) {
        NetData.TANKINFO tankInfo = new NetData.TANKINFO();
        tankInfo.position = position;
        tankInfo.rotation = rotation;
        tankInfo.isPlayer = false;
        NetworkServer.SendToAll(NetData.STC_TANK_INFO, tankInfo);
    }
    public void ResponseTankInfo(NetworkMessage msg) {
        var tankInfo = msg.ReadMessage<NetData.TANKINFO>();

        GameMNG.I.ResponseEnemyInfo(tankInfo.position, tankInfo.rotation);
    }
    public void RequestBulletInfo(Vector3 position, Quaternion rotation, int power, string bulletName) {
        NetData.BULLETINFO bulletInfo = new NetData.BULLETINFO();
        bulletInfo.position = position;
        bulletInfo.rotation = rotation;
        bulletInfo.power = power;
        bulletInfo.bulletName = bulletName;
        NetworkServer.SendToAll(NetData.STC_BULLET_INFO, bulletInfo);
    }
    public void ResponseBulletInfo(NetworkMessage msg) {
        var bulletInfo = msg.ReadMessage<NetData.BULLETINFO>();

        GameMNG.I.ResponseBulletInfo(bulletInfo.position, bulletInfo.rotation, bulletInfo.power, bulletInfo.bulletName);
    }
    public void RequestDamage(int damage) {
        NetData.DAMAGE dmg = new NetData.DAMAGE();
        dmg.damage = damage;
        NetworkServer.SendToAll(NetData.STC_DAMAGE, dmg);
    }
    public void ResponseDamage(NetworkMessage msg) {
        var dmg = msg.ReadMessage<NetData.DAMAGE>();

        GameMNG.I.ResponseDamage(dmg.damage);
    }
    public void RequestCreateItem(Vector3 position, int itemID) {
        NetData.ITEM item = new NetData.ITEM();
        item.position = position;
        item.itemID = itemID;
        NetworkServer.SendToAll(NetData.STC_CREATE_ITEM, item);
    }
    public void ResponseCreateItem(NetworkMessage msg) {
        var item = msg.ReadMessage<NetData.ITEM>();

        GameMNG.I.ResponseCreateItem(item.position, item.itemID);
    }
    public void RequestRemoveItem(int itemID) {
        NetData.ITEM item = new NetData.ITEM();
        item.position = Vector3.zero;
        item.itemID = itemID;
        NetworkServer.SendToAll(NetData.STC_REMOVE_ITEM, item);
    }
    public void ResponseRemoveItem(NetworkMessage msg) {
         var item = msg.ReadMessage<NetData.ITEM>();

        GameMNG.I.ResponseRemoveItem(item.itemID);
    }
    public void Dis() {
        OnDisable();
    }
    void OnDisable() {
         NetworkServer.ClearHandlers();
         NetworkServer.ClearLocalObjects();
         NetworkServer.ClearSpawners();
         NetworkServer.DisconnectAll();
         NetworkServer.Shutdown();
        Debug.Log("close server");
    }

}
