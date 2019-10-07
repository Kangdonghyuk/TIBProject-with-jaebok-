using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMNG : MonoBehaviour
{
    public static CameraMNG I;

    public Transform mainCamera;
    CameraLook cameraLook;

    Transform player;
    Vector3 cameraPosition;
    Quaternion cameraRotation;

    bool isParentPlayer;

    void Awake() {
        I = this;

        cameraLook = mainCamera.GetComponent<CameraLook>();
    }

    public void SetPlayer(Transform transform) {
        player = transform;

        mainCamera.SetParent(player);
        mainCamera.localPosition = Vector3.up * 0.8f;
        mainCamera.localRotation = Quaternion.identity;
        cameraLook.enabled = true;

        isParentPlayer = true;
    }

    public void ShowBullet(Transform transform) {
        if(isParentPlayer) {
            cameraPosition = mainCamera.localPosition;
            cameraRotation = mainCamera.localRotation;
        }
        isParentPlayer = false;
        mainCamera.SetParent(transform);
        mainCamera.localPosition = new Vector3(0, 0f, 0f);
        mainCamera.rotation = Quaternion.Euler(10f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        cameraLook.enabled = true;
    }

    public void RollBackCamera() {
        mainCamera.SetParent(transform);
        StartCoroutine(RollBack());
    }

    IEnumerator RollBack() {
        yield return new WaitForSeconds(2.5f);

        mainCamera.SetParent(player);
        mainCamera.localPosition = cameraPosition;
        mainCamera.localRotation = cameraRotation;
        cameraLook.enabled = true;
        isParentPlayer = true;

        StopCoroutine(RollBack());
    }
}
