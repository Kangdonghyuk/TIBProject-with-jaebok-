using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuJoin : MonoBehaviour
{
    public Toggle isServer;
    public InputField ip, port;

    public void OnClick() {
        SystemMNG.I.Join(isServer.isOn, ip.text, int.Parse(port.text));
    }
}
