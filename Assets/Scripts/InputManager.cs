using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerControl playercontrol;
    private AnimatorManager animatorManager;
    private float moveAmount;
    [SerializeField] private Vector2 movementInput;
    [SerializeField] private Vector2 cameraInput;
        
    public float verticalInput;
    public float horizontalInput;
    public float cameraInputX;
    public float cameraInputY;

    private void Awake()
    {
        animatorManager =GetComponent<AnimatorManager>();
    }
    private void OnEnable()
    {
        if (playercontrol == null)
        {
            playercontrol = new PlayerControl();
            playercontrol.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playercontrol.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
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
    }
    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        cameraInputY = cameraInput.y;
        cameraInputX = cameraInput.x;
        
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animatorManager.UpdateAnimatorValues(0, moveAmount); 
    }
}
