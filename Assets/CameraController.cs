using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    private InputAction cameraControls;
    private Vector2 mouseDelta;
    private float rotationSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        cameraControls = new InputAction("CameraControls");
        cameraControls.AddBinding("<Mouse>/delta");
        cameraControls.performed += ctx => {
            mouseDelta = ctx.ReadValue<Vector2>();
        };
        cameraControls.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (mouseDelta != Vector2.zero)
        {
            transform.Rotate(Vector3.up, mouseDelta.x * Time.deltaTime * rotationSpeed, Space.World);
            transform.Rotate(Vector3.right, -mouseDelta.y * Time.deltaTime * rotationSpeed, Space.Self);
        }
    }
}
