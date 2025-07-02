using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private CinemachineImpulseSource cinemachineImpulseSource;
    private static CameraShake instance;

    private void Awake()
    {
        instance = this;

        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    }
    public static CameraShake Instance()
    {
        return instance;
    }
    public void Shake(float strenth = 1f)
    {
        cinemachineImpulseSource.GenerateImpulse(strenth);
    }
}
