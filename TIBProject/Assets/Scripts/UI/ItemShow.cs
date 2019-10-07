using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemShow : MonoBehaviour
{
    Text text;

    bool isShow = false;

    void Awake() {
        text = GetComponent<Text>();
    }

    void Update()
    {
        if(isShow == true) {
            text.rectTransform.Translate(-300f * Time.deltaTime, 0f, 0f);
            if(text.rectTransform.localPosition.x <= -750f)
                isShow = false;
        }
    }

    public void Show(string item) {
        text.text = item;
        isShow = true;
        text.rectTransform.localPosition = new Vector3(750f, 0f, 0f);
    }
}
