using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMNG : MonoBehaviour
{
    public static UIMNG I;

    public PowerGauge powerGauge;
    public LifeBar lifeBar;
    public Text turnText;
    public Text finishText;
    public ItemShow itemShow;

    void Awake() {
        I = this;
    }

    public void PowerUp() {
        powerGauge.PowerUp();
    }

    public int GetPower() {
        int power = powerGauge.power;
        powerGauge.Clear();
        return power;
    }

    public void DamageMe(int damage) {
        lifeBar.DamageMe(damage);
    }
    public void DamageEnemy(int damage) {
        lifeBar.DamageEnemy(damage);
    }
    public void SetTurn(bool turn) {
        if(turn == true)
            turnText.text = "Me";
        else
            turnText.text = "En";
    }
    public void Finish(bool isWin) {
        if(isWin == true)
            finishText.text = "YOU WIN!";
        else
            finishText.text = "YOU LOSE";
        finishText.gameObject.SetActive(true);
    }
    public void MenuBtn() {
        GameMNG.I.Dis();
        SystemMNG.I.LoadScene("MenuScene");
    }
    public void ReStartBtn() {
        GameMNG.I.Dis();
        SystemMNG.I.LoadScene("GameScene");
    }
    public void ItemShow(string item) {
        itemShow.Show(item);
    }

    void Update() {
        /*if(Input.GetKey(KeyCode.Alpha1))
            lifeBar.DamageMe(1);
        if(Input.GetKey(KeyCode.Alpha2))
            lifeBar.DamageEnemy(1);
        if(Input.GetKey(KeyCode.Alpha3))
            lifeBar.HealMe(1);
        if(Input.GetKey(KeyCode.Alpha4))
            lifeBar.HealEnemy(1);*/
    }
}
