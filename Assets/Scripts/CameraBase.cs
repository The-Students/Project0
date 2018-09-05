using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBase : MonoBehaviour {
    [Header("Camera")]
    public float _CameraSpeed;

    private Vector3 _globalCenter;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        CheckKey();
        CheckMouse();
    }

    public void SetCameraPosition(Vector3 pos)
    {
        transform.position = pos;
    }

    private void CheckKey()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Vector3 pos = transform.position;
            pos.z += _CameraSpeed;
            transform.position = pos;
        }

        if (Input.GetKey(KeyCode.A))
        {
            Vector3 pos = transform.position;
            pos.x -= _CameraSpeed;
            transform.position = pos;
        }

        if (Input.GetKey(KeyCode.S))
        {
            Vector3 pos = transform.position;
            pos.z -= _CameraSpeed;
            transform.position = pos;
        }

        if (Input.GetKey(KeyCode.D))
        {
            Vector3 pos = transform.position;
            pos.x += _CameraSpeed;
            transform.position = pos;
        }
    }

    private void CheckMouse()
    {
        if (Input.mousePosition.y >= Screen.height)
        {
            Vector3 pos = transform.position;
            pos.z += _CameraSpeed;
            transform.position = pos;
        }

        if (Input.mousePosition.x <= 0.0f)
        {
            Vector3 pos = transform.position;
            pos.x -= _CameraSpeed;
            transform.position = pos;
        }

        if (Input.mousePosition.y <= 0.0f)
        {
            Vector3 pos = transform.position;
            pos.z -= _CameraSpeed;
            transform.position = pos;
        }

        if (Input.mousePosition.x >= Screen.width)
        {
            Vector3 pos = transform.position;
            pos.x += _CameraSpeed;
            transform.position = pos;
        }
    }
}
