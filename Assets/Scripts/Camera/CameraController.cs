using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class CameraController : MonoBehaviour
{
    float speed = 20f;
    public NewInputSystem controls;

    public const float sensitivity = 10f;

    private void Awake()
    {
        controls = new NewInputSystem();
    }

    private void OnEnable()
    {
        controls.Camera.Enable();
        controls.Camera.Lock.started += Lock;
        controls.Camera.Lock.canceled += Unlock;
    }

    private void OnDisable()
    {
        controls.Camera.Lock.started -= Lock;
        controls.Camera.Lock.canceled -= Unlock;
        controls.Camera.Disable();
    }

    void Lock(CallbackContext context)
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Unlock(CallbackContext context)
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Vector2 delta = controls.Camera.Rotation.ReadValue<Vector2>();
            if (delta != Vector2.zero)
            {
                transform.eulerAngles += new Vector3(-delta.y, delta.x, 0) * sensitivity * Time.deltaTime;
            }

            Vector2 value = controls.Camera.Moving.ReadValue<Vector2>();
            if (value != Vector2.zero)
            {
                transform.Translate(new Vector3(value.x, 0, value.y) * Time.deltaTime * speed, Space.Self);
            }
        }
    }
}