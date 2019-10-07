using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SystemMNG : MonoBehaviour
{
    public static SystemMNG I;
    public bool isServer;
    public string ip;
    public int port;

    void Awake() {
        I = this;
        DontDestroyOnLoad(this);
    }

    void Start() {
        Application.runInBackground = true;

        SceneManager.LoadScene("MenuScene");
    }

    public void Join(bool isServer, string ip, int port) {
        this.isServer = isServer;
        this.ip = ip;
        this.port = port;
        LoadScene("GameScene");
    }

    public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

}
