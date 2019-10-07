using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ClientMNG : ServerOrClient
{
    NetworkClient client;

    // Start is called before the first frame update
    public void On(string ip, int port)
    {
        client = new NetworkClient();

        client.RegisterHandler(MsgType.Connect, OnConnect);
        client.RegisterHandler(NetData.STC_TANK_INFO, ResponseTankInfo);
        client.RegisterHandler(NetData.STC_BULLET_INFO, ResponseBulletInfo);
        client.RegisterHandler(NetData.STC_DAMAGE, ResponseDamage);
        client.RegisterHandler(NetData.STC_CREATE_ITEM, ResponseCreateItem);
        client.RegisterHandler(NetData.STC_REMOVE_ITEM, ResponseRemoveItem);

        client.Connect(ip, port);
    }

    public void RequestTankInfo(Vector3 position, Quaternion rotation) {
        NetData.TANKINFO tankInfo = new NetData.TANKINFO();
        tankInfo.position = position;
        tankInfo.rotation = rotation;
        tankInfo.isPlayer = false;
        client.Send(NetData.CTS_TANK_INFO, tankInfo);
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
        client.Send(NetData.CTS_BULLET_INFO, bulletInfo);
    }
    public void ResponseBulletInfo(NetworkMessage msg) {
        var bulletInfo = msg.ReadMessage<NetData.BULLETINFO>();

        GameMNG.I.ResponseBulletInfo(bulletInfo.position, bulletInfo.rotation, bulletInfo.power, bulletInfo.bulletName);
    }
    public void RequestDamage(int damage) {
        NetData.DAMAGE dmg = new NetData.DAMAGE();
        dmg.damage = damage;
        
        client.Send(NetData.CTS_DAMAGE, dmg);
    }
    public void ResponseDamage(NetworkMessage msg) {
        var dmg = msg.ReadMessage<NetData.DAMAGE>();

        GameMNG.I.ResponseDamage(dmg.damage);
    }
    public void RequestCreateItem(Vector3 position, int itemID) {
        NetData.ITEM item = new NetData.ITEM();
        item.position = position;
        item.itemID = itemID;
        client.Send(NetData.CTS_CREATE_ITEM, item);
    }
    public void ResponseCreateItem(NetworkMessage msg) {
        var item = msg.ReadMessage<NetData.ITEM>();

        GameMNG.I.ResponseCreateItem(item.position, item.itemID);
    }
    public void RequestRemoveItem(int itemID) {
        NetData.ITEM item = new NetData.ITEM();
        item.position = Vector3.zero;
        item.itemID = itemID;
        client.Send(NetData.CTS_REMOVE_ITEM, item);
    }
    public void ResponseRemoveItem(NetworkMessage msg) {
        var item = msg.ReadMessage<NetData.ITEM>();

        GameMNG.I.ResponseRemoveItem(item.itemID);
    }
    public void Dis() {
        client.Disconnect();
        client.Shutdown();
    }
    void OnConnect(NetworkMessage msg) {
        Debug.Log("Success Connect");
    }
}
