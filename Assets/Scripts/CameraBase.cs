using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBase : MonoBehaviour {
    [Header("Camera")]
    public float _CameraSpeed;
    public float _MaxZoomHeight;
    public float _MinZoomHeight;
    //public float _ClampOffset;
    public GameObject _TileManager;

    private float _minXClamp, _maxXClamp, _minZClamp, _maxZClamp;



    private Vector3 _globalCenter;
    private Vector2 _screenCenter;
    // Use this for initialization
    void Start ()
    {
        var tileManager = _TileManager.GetComponent<TileManager>();
        _screenCenter.Set(Screen.width/2, Screen.height/2);
        _minXClamp = -tileManager.GetMapSize() / 2;
        _maxXClamp = tileManager.GetMapSize() / 2;
        _minZClamp = -tileManager.GetMapSize() / 2;
        _maxZClamp = tileManager.GetMapSize() / 2;
    }
	
	// Update is called once per frame
	void Update ()
    {
        CheckKey();
        CheckRotate();
        CheckMouse();


        Vector3 pos = transform.position;
        pos.z = Mathf.Clamp(pos.z, _minZClamp, _maxZClamp);
        pos.x = Mathf.Clamp(pos.x, _minXClamp, _maxXClamp);
        pos.y = Mathf.Clamp(pos.y, _MinZoomHeight, _MaxZoomHeight);
        transform.position = pos;
    }

    public void SetCameraPosition(Vector3 pos)
    {
        transform.position = pos;
    }

    private void CheckKey()
    {
        Vector3 forward = transform.forward;
       // forward = transform.InverseTransformDirection(forward);
        forward.y = 0.0f;
        forward.Normalize();

        Vector3 right = transform.right;
       // right = transform.InverseTransformDirection(right);
        right.y = 0.0f;
        right.Normalize();


        if (Input.GetKey(KeyCode.W))
        {
            Vector3 pos = transform.position;
            pos += forward * _CameraSpeed;
            transform.position = pos;
        }

        if (Input.GetKey(KeyCode.A))
        {
            Vector3 pos = transform.position;
            pos -= right * _CameraSpeed;
            transform.position = pos;
        }

        if (Input.GetKey(KeyCode.S))
        {
            Vector3 pos = transform.position;
            pos -= forward * _CameraSpeed;
            transform.position = pos;
        }

        if (Input.GetKey(KeyCode.D))
        {
            Vector3 pos = transform.position;
            pos += right * _CameraSpeed;
            transform.position = pos;
        }
    }

    private void CheckRotate()
    {
        var lenghtToGround = transform.position.y / Mathf.Sin(90.0f - transform.rotation.x);
        var midPoint = transform.position + (transform.forward * lenghtToGround);
        if (Input.GetKey(KeyCode.Q))
        {
            transform.RotateAround(midPoint, Vector3.up, _CameraSpeed);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.RotateAround(midPoint, Vector3.up, -_CameraSpeed);
        }
    }

    private void CheckMouse()
    {
        Vector2 mouseVector = GetMouseVectorNormalized();
        mouseVector = RotateVector2(mouseVector, transform.eulerAngles.y);
        if (Input.mousePosition.y >= Screen.height || Input.mousePosition.x <= 0.0f
            || Input.mousePosition.y <= 0.0f || Input.mousePosition.x >= Screen.width)
        {
            Vector3 pos = transform.position;
            pos.z += mouseVector.y * _CameraSpeed;
            pos.x += mouseVector.x * _CameraSpeed;
            transform.position = pos;
        }

        //1. zoom out; 2. zoom in
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && transform.position.y < _MaxZoomHeight)
        {
            Vector3 pos = transform.position;
            pos -= transform.forward;
            transform.position = pos;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0 && transform.position.y > _MinZoomHeight)
        {
            Vector3 pos = transform.position;
            pos += transform.forward;
            transform.position = pos;
        }
    }

    private Vector2 RotateVector2(Vector2 v, float angle)
    {
        float radian = -angle * Mathf.Deg2Rad;
        float _x = v.x * Mathf.Cos(radian) - v.y * Mathf.Sin(radian);
        float _y = v.x * Mathf.Sin(radian) + v.y * Mathf.Cos(radian);
        return new Vector2(_x, _y);
    }

    private Vector3 GetMouseVectorNormalized()
    {
        Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 returnVector = mousePos - _screenCenter;
        returnVector.Normalize();
        return returnVector;
    }
}
