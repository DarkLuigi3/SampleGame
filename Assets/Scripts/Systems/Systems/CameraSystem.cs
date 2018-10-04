using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

class CameraSystem : ComponentSystem
{
    private ComponentGroupArray<CameraOrientation> _cameraOrientation;
    private float _xRot, _yRot;

    protected override void OnStartRunning()
    {
        base.OnStartRunning();
        _cameraOrientation = GetEntities<CameraOrientation>();

        for (int i = 0; i < _cameraOrientation.Length; ++i)
        {
            _xRot = _cameraOrientation[i].transform.localEulerAngles.x;
            _yRot = _cameraOrientation[i].transform.localEulerAngles.y;
        }
    }

    protected override void OnUpdate()
    {
        //throw new System.NotImplementedException();
        for (int i = 0; i < _cameraOrientation.Length; ++i)
        {
            _xRot -= Input.GetAxis("Mouse Y") * _cameraOrientation[i].cameraParams.sensitivity * Time.deltaTime;
            _yRot += Input.GetAxis("Mouse X") * _cameraOrientation[i].cameraParams.sensitivity * Time.deltaTime;

            _xRot = Mathf.Clamp(_xRot, -90, 90);
            _yRot = _yRot < 0 ? _yRot + 360 : _yRot > 360 ? _yRot - 360 : _yRot;

            _cameraOrientation[i].transform.localRotation = Quaternion.Slerp(_cameraOrientation[i].transform.localRotation, Quaternion.Euler(_xRot, _yRot, 0), .6f);
        }
    }
}