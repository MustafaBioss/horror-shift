using UnityEngine;
using UnityEngine.InputSystem;
using Yarn.Unity;

public class PlayerMovement : MonoBehaviour
{

    [Header("Movement Settings")]
    [SerializeField] float playerSpeed = 5f;
    [SerializeField] float sprintSpeed = 8f;
    [SerializeField] float gravity = -20f;
    bool canMove = true;


    [Header("Camera Settings")]
    [SerializeField] Transform cameraTransform;
    [SerializeField] float mouseSensitivity = 0.1f;
    [SerializeField] float maxLookAngle = 85f;

    CharacterController controller;
    InputAction move;
    InputAction look;
    InputAction sprint;

    public DialogueRunner dialogueRunner;

    float verticalVelocity;
    float pitch;

    void Awake()
    {

        dialogueRunner.AddCommandHandler<bool>(
            "allow_movement",
            AllowMovement
        );
        
        controller = GetComponent<CharacterController>();

        move = InputSystem.actions.FindAction("Player/Move");
        look = InputSystem.actions.FindAction("Player/Look");
        sprint = InputSystem.actions.FindAction("Player/Sprint");

        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            Look();
            Move();
        }
    }

    void Look()
    {
        Vector2 delta = look.ReadValue<Vector2>() * mouseSensitivity;

        transform.Rotate(0f, delta.x, 0f);
        pitch = Mathf.Clamp(pitch - delta.y, -maxLookAngle, maxLookAngle);
        cameraTransform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }

    void Move()
    {
        Vector2 input = move.ReadValue<Vector2>();
        
        bool sprinting = sprint != null && sprint.IsPressed();
        float speed = sprinting ? sprintSpeed : playerSpeed;

        Vector3 velocity = (transform.right * input.x + transform.forward * input.y) * speed;

        if (controller.isGrounded && verticalVelocity < 0f)
            verticalVelocity = -2f;
        verticalVelocity += gravity * Time.deltaTime;
        velocity.y = verticalVelocity;

        controller.Move(velocity * Time.deltaTime);
    }

    private void AllowMovement(bool allow)
    {
        canMove = allow ;
    }

}
