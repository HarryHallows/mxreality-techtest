using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float mouseSensitivity;

    private float horizontal, vertical;
    private float mouseX, mouseY;

    private float verticalRotation;

    [SerializeField] private GameObject cam;
    public CharacterController controller;
    

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.gameObject;
        Cursor.lockState = CursorLockMode.Locked;

        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Inputs();
        Rotation();
        Movement();
    }

    private void Inputs()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

    }

    private void Rotation()
    {
        verticalRotation -= mouseY;
        cam.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        
        transform.Rotate(Vector3.up * mouseX);
    }

    private void Movement()
    {
        Vector3 _move = transform.right * horizontal + transform.forward * vertical;

        controller.Move(_move * moveSpeed * Time.deltaTime);
    }
}
