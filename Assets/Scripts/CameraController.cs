using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class CameraController : MonoBehaviour
{
    float speed = 20f;
    public NewInputSystem controls;

    public const float sensitivity = 5f;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        controls = new NewInputSystem();
    }

    private void OnEnable()
    {
        controls.Camera.Enable();
    }

    private void OnDisable()
    {
        controls.Camera.Disable();
    }

    void Update()
    {
        Vector2 delta = controls.Camera.Rotation.ReadValue<Vector2>() * sensitivity;
        if (delta!= Vector2.zero)
        {
            transform.eulerAngles += new Vector3(-delta.y, delta.x, 0) * Time.deltaTime;
        }

        Vector2 value = controls.Camera.Moving.ReadValue<Vector2>();
        if (value != Vector2.zero)
        {
            transform.Translate(new Vector3(value.x, 0, value.y) * Time.deltaTime * speed, Space.Self);
        }
    }
}
