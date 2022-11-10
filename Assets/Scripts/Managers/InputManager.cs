using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerControl playercontrol;
    private AnimatorManager animatorManager;
    private PlayerMovement playerMovement;
    
    [SerializeField] private Vector2 movementInput;
    [SerializeField] private Vector2 cameraInput;

    public float moveAmount;
    public float verticalInput;
    public float horizontalInput;
    public float cameraInputX;
    public float cameraInputY;
    public bool sprintInput;

    private void Awake()
    {
        animatorManager =GetComponent<AnimatorManager>();
        playerMovement = GetComponent<PlayerMovement>();
    }
    private void OnEnable()
    {
        if (playercontrol == null)
        {
            playercontrol = new PlayerControl();
            playercontrol.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playercontrol.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();

            playercontrol.PlayerActions.BSprint.performed += i => sprintInput = true;
            playercontrol.PlayerActions.BSprint.canceled += i => sprintInput = false;
        }
        playercontrol.Enable();
    }

    private void OnDisable()
    {
        playercontrol.Disable();
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
        HandleSprintingInput();  
    }
    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        cameraInputY = cameraInput.y;
        cameraInputX = cameraInput.x;
        
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animatorManager.UpdateAnimatorValues(0, moveAmount,playerMovement.isSprinting); 
    }

    private void HandleSprintingInput()
    {
        if (sprintInput)
        {
            playerMovement.isSprinting = true;
        }
        else
        {
            playerMovement.isSprinting = false;
        }
    }
}
