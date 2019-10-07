using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerGauge : MonoBehaviour
{
    Image[] powerGaugeList = new Image[13];
    Color[] powerGuageColorList = new Color[13];
    Color powerGaugeNon;
    
    public int power;
    bool isPowerUp;

    void Start()
    {
        powerGaugeNon = Color.white;
        powerGaugeNon.a = 0.3f;
        for(int i=0; i<13; i++) {
            powerGaugeList[i] = transform.GetChild(i).GetComponent<Image>();
            powerGuageColorList[i] = powerGaugeList[i].color;
            powerGaugeList[i].color = powerGaugeNon;
        }

        power = 0;
        isPowerUp = true;
    }

    public void PowerUp() {
        if(isPowerUp) {
            powerGaugeList[power].color = powerGuageColorList[power];
            power += 1;
            if(power == 13)
                isPowerUp = false;
        }
        else {
            power -= 1;
            powerGaugeList[power].color = powerGaugeNon;
            if(power == 0)
                isPowerUp = true;
        }
    }

    public void Clear() {
        for(int i=0; i<13; i++)
            powerGaugeList[i].color = powerGaugeNon;
        power = 0;
        isPowerUp = true;
    }
}
