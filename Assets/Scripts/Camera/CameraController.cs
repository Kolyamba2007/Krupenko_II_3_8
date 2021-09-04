using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

public class CameraController : MonoBehaviour
{
    private float speed = 20f;
    private float sensitivity = 10f;

    private NewInputSystem controls;

    public float Speed => speed;
    public float Sensivity => sensitivity;

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

    private void Lock(CallbackContext context)
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Unlock(CallbackContext context)
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update()
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

    public void SetSensivity(Slider slider)
    {
        sensitivity = slider.value;
    }
    public void SetSpeed(Slider slider)
    {
        speed = slider.value;
    }
}