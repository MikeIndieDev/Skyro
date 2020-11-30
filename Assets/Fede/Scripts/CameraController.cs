using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    float _sensitivity;

    [SerializeField]
    Transform _player;

    [SerializeField]
    float _minRotationY;

    [SerializeField]
    float _maxRotationY;

    float _Yrotation;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float rotateX = Input.GetAxis("Mouse X") * _sensitivity * Time.deltaTime;
        float rotateY = Input.GetAxis("Mouse Y") * _sensitivity * Time.deltaTime;

        _Yrotation -= rotateY;
        _Yrotation = Mathf.Clamp(_Yrotation, _minRotationY, _maxRotationY);

        _player.Rotate(Vector3.up * rotateX);

        transform.localRotation = Quaternion.Euler(_Yrotation, 0f, 0f);

    }
}




