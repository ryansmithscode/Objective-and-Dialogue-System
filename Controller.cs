using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;

// Ryan Smith Objective and Dialogue Smaller Script

public class Controller : MonoBehaviour
{
    [Header("Third Person Controller")]
    public CharacterController characterController;
    public float speed;
    public float gravity;
    public float jumpHeight;
    Vector3 velocity;

    [Header("Ground")]
    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;
    private bool isGrounded;

    [Header("Camera")]
    public GameObject playerCamera;
    public float mouseSensitivity;
    public Transform playerBody;
    private float xRotation = 0;


    //-----------------------------------Start is called once upon creation-------------------------
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Since camera uses the mouse, it stops it from moving outside the application which could cause issues + annoying
    }

    //-----------------------------------Update is called once per frame----------------------------
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); // Checks on collision
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Defined In Unity's new input system
        float x = Input.GetAxis("Horizontal"); 
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        characterController.Move(move * speed * Time.deltaTime); // From direction player model facing

        if (Input.GetButtonDown("Jump") && isGrounded) // Cannot jump mid air
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // speed needed to reach height. moves up force
        }

        velocity.y += gravity * Time.deltaTime; // Just simply pulling down

        characterController.Move(velocity * Time.deltaTime); // Uses charactercontroller instead of rigid

        // Mouse rotations for looking around, multiplying by sensitivity 
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY; // Y axis so moving up/down

        xRotation = Mathf.Clamp(xRotation, -90, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX); // Not visible due to temporary model but rotates body

    }
}