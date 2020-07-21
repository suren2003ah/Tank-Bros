using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    Vector3 cameraInitialPosition;
    public float killShakeDuration;
    public float killShakeMagnitude;
    public Camera MainCamera;

    public void ShakeIt()
    {
        cameraInitialPosition = MainCamera.transform.position;
        InvokeRepeating("StartCameraShaking", 0f, 0.005f);
        Invoke("StopCameraShaking", killShakeDuration);
    }
    public void StartCameraShaking()
    {
        float x = Random.Range(-1f, 1f) * killShakeMagnitude;
        float y = Random.Range(-1f, 1f) * killShakeMagnitude;
        float z = Random.Range(-1f, 1f) * killShakeMagnitude;
        Vector3 cameraIntermadiatePosition = MainCamera.transform.position;
        cameraIntermadiatePosition.x += x;
        cameraIntermadiatePosition.y += y;
        cameraIntermadiatePosition.z += z;
        MainCamera.transform.position = cameraIntermadiatePosition;
    }
    public void StopCameraShaking()
    {
        CancelInvoke("StartCameraShaking");
        MainCamera.transform.position = cameraInitialPosition;
    }
}
