using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f; 
    [SerializeField] float zoomSpeed = 5f;

    Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        CameraMovement();
        ZoomCamera();
    }

    void CameraMovement()
    {
        Vector3 moveVector = Vector3.zero;

        if (Keyboard.current[Key.W].isPressed)
        {
            moveVector.y += 1;
        }
        if (Keyboard.current[Key.S].isPressed)
        {
            moveVector.y -= 1;
        }
        if (Keyboard.current[Key.A].isPressed)
        {
            moveVector.x -= 1;
        }
        if (Keyboard.current[Key.D].isPressed)
        {
            moveVector.x += 1;
        }

        moveVector = moveVector.normalized * moveSpeed * Time.deltaTime;
        transform.position += moveVector;
    }

    void ZoomCamera()
    {
        float scroll = Mouse.current.scroll.ReadValue().y;
        cam.orthographicSize -= scroll * zoomSpeed * Time.deltaTime;
    }
}

