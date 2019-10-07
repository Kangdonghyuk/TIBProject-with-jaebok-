using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetData {

    public const short CREATE_TANK = 100;
    public const short CREATE_ENEMY = 200;

    public const short CTS_TANK_INFO = 100;
    public const short STC_TANK_INFO = 110;

    public const short CTS_BULLET_INFO = 200;
    public const short STC_BULLET_INFO = 210;

    public const short CTS_DAMAGE = 300;
    public const short STC_DAMAGE = 310;

    public const short CTS_CREATE_ITEM = 400;
    public const short STC_CREATE_ITEM = 410;
    public const short CTS_REMOVE_ITEM = 420;
    public const short STC_REMOVE_ITEM = 430;

    public class TANKOBJ : MessageBase {
        public Vector3 position;
        public Quaternion rotation;
        public bool isPlayer;
        public GameObject tankObj;
    }

    public class ENEMYOBJ : MessageBase {
        public Vector3 position;
        public Quaternion rotation;
        public GameObject enemyObj;
    }

    public class TANKINFO : MessageBase {
        public Vector3 position;
        public Quaternion rotation;
        public bool isPlayer;
    }

    public class BULLETINFO : MessageBase {
        public Vector3 position;
        public Quaternion rotation;
        public int power;
        public string bulletName;
    }

    public class DAMAGE : MessageBase {
        public int damage;
    }

    public class ITEM : MessageBase {
        public Vector3 position;
        public int itemID;
    }
  
}
