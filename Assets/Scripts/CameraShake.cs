using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    Vector3 cameraInitialPosition;
    public float killShakeDuration;
    public float killShakeMagnitude;
    public float hitShakeDuration;
    public float hitShakeMagnitude;
    private int shakeType;
    private float x, y, z;
    public Camera MainCamera;

    public void ShakeKill()
    {
        shakeType = 1;
        cameraInitialPosition = MainCamera.transform.position;
        InvokeRepeating("StartCameraShaking", 0f, 0.005f);
        Invoke("StopCameraShaking", killShakeDuration);
    }
    public void ShakeHit()
    {
        shakeType = 2;
        cameraInitialPosition = MainCamera.transform.position;
        InvokeRepeating("StartCameraShaking", 0f, 0.005f);
        Invoke("StopCameraShaking", hitShakeDuration);
    }
    public void StartCameraShaking()
    {
        if (shakeType == 1)
        {
            x = Random.Range(-1f, 1f) * killShakeMagnitude;
            y = Random.Range(-1f, 1f) * killShakeMagnitude;
            z = Random.Range(-1f, 1f) * killShakeMagnitude;
        }
        if (shakeType == 2)
        {
            x = Random.Range(-1f, 1f) * hitShakeMagnitude;
            y = Random.Range(-1f, 1f) * hitShakeMagnitude;
            z = Random.Range(-1f, 1f) * hitShakeMagnitude;
        }
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
