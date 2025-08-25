using System;
using UnityEngine;

public class CameraControls : MonoBehaviour
{

    public float XSensitivity = 400;
    public float YSensitivity = 400;

    private float xRotation;
    private float yRotation;

    public Transform playerOrientation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Look(Vector2 lookDirection)
    {
        yRotation += lookDirection.x * XSensitivity * Time.deltaTime;
        xRotation -= lookDirection.y * YSensitivity * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        playerOrientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
