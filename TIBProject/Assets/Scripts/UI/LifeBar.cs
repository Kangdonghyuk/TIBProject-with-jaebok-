using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    public Image myBar;
    public Image enBar;
    Vector3 myBarPos;
    Vector3 enBarPos;
    Vector3 myBarSize;
    Vector3 enBarSize;

    float offsetPos = 1.5f;
    int offsetSize = 3;

    public int _myLife = 100;
    public int myLife {
        get { return _myLife; }
        set {
            _myLife = value;
            if(_myLife > 100)
                _myLife = 100;
            if(_myLife < 0)
                _myLife = 0;
        }
    }
    public int _enLife = 100;
    public int enLife {
        get { return _enLife; }
        set {
            _enLife = value;
            if(_enLife > 100)
                _enLife = 100;
            if(_enLife < 0)
                _enLife = 0;
        }
    }

    void Start() {
        myBarPos = myBar.rectTransform.localPosition;
        enBarPos = enBar.rectTransform.localPosition;
        myBarSize = myBar.rectTransform.sizeDelta;
        enBarSize = enBar.rectTransform.sizeDelta;
        myLife = 100;
        enLife = 100;
    }

    public void DamageMe(int damage) {
        myLife -= damage;
        myBarSize.x = 10f + ((100 - myLife) * offsetSize);
        myBarPos.x = 155f - ((100 - myLife) * offsetPos);
        myBar.rectTransform.localPosition = myBarPos;
        myBar.rectTransform.sizeDelta = myBarSize;
    }
    public void HealMe(int heal) {
        myLife += heal;
        myBarSize.x = 10f + ((100 - myLife) * offsetSize);
        myBarPos.x = 155f - ((100 - myLife) * offsetPos);
        myBar.rectTransform.localPosition = myBarPos;
        myBar.rectTransform.sizeDelta = myBarSize;
    }

    public void DamageEnemy(int damage) {
        enLife -= damage;
        enBarSize.x = 10f + ((100 - enLife) * offsetSize);
        enBarPos.x = 155f - ((100 - enLife) * offsetPos);
        enBar.rectTransform.localPosition = enBarPos;
        enBar.rectTransform.sizeDelta = enBarSize;
    }
    public void HealEnemy(int heal) {
        enLife += heal;
        enBarSize.x = 10f + ((100 - enLife) * offsetSize);
        enBarPos.x = 155f - ((100 - enLife) * offsetPos);
        enBar.rectTransform.localPosition = enBarPos;
        enBar.rectTransform.sizeDelta = enBarSize;
    }
}
