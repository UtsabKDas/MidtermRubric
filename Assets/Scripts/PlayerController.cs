using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckDistance = 0.3f;
    [SerializeField] private ScoreManager score;

    public InputSystem_Actions InputSystemActions;
    private InputSystem_Actions.PlayerActions playerActions;

    private Rigidbody rb;
    private bool isGrounded;
    private float horizontal;
    private float vertical;
    private PlayerHealth playerHealth;
    
    private void Awake()
    {
        InputSystemActions = new InputSystem_Actions();
        playerActions = InputSystemActions.Player;

        playerHealth = GetComponent<PlayerHealth>();
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    private void OnEnable()
    {
        playerActions.Move.performed += HandleMovementInput;
        playerActions.Move.canceled += HandleMovementInput;
        playerActions.Pause.performed += GameManager.Instance.HUDManager.OnPauseInputPressed;
        playerActions.Enable();
    }

    private void OnDisable()
    {
        playerActions.Move.performed -= HandleMovementInput;
        playerActions.Move.canceled -= HandleMovementInput;
        playerActions.Pause.performed -= GameManager.Instance.HUDManager.OnPauseInputPressed;
        playerActions.Disable();
    }

    private void Update()
    {
        if (GameManager.Instance.IsGameOver)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            enabled = false;
            return;
        }
        //horizontal = Input.GetAxis("Horizontal");
        //vertical = Input.GetAxis("Vertical");
        CheckGrounded();
        HandleJump();

       //if (Input.GetButtonDown("Pause"))
       //{
       //    HUDManager hudManager = GameManager.Instance.HUDManager;
       //    if (hudManager.IsPaused)
       //    {
       //        GameManager.Instance.HUDManager.Unpause();
       //    }
       //    else
       //    {
       //        GameManager.Instance.HUDManager.Pause();
       //    }
       //}
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void CheckGrounded()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
    }

    private void HandleMovementInput(InputAction.CallbackContext value)
    {
        Vector2 moveInput = value.ReadValue<Vector2>();
        horizontal = moveInput.x;
        vertical = moveInput.y;
    }

    private void HandleMovement()
    {
        Vector3 forward = cameraTransform.forward;
        forward.y = 0f;
        forward.Normalize();
        Vector3 right = cameraTransform.right;
        right.y = 0f;
        right.Normalize();
        Vector3 direction = (forward * vertical + right * horizontal).normalized * moveSpeed;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), 5f); 
        rb.linearVelocity = new Vector3(direction.x, rb.linearVelocity.y, direction.z);
    }

    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
        }
    }
}
