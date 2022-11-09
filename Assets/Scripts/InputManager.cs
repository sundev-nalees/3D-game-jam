using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerControl playercontrol;
    private AnimatorManager animatorManager;
    private float moveAmount;
    [SerializeField] private Vector2 movementInput;
        
    public float verticalInput;
    public float horizontalInput;

    private void Awake()
    {
        animatorManager =GetComponent<AnimatorManager>();
    }
    private void OnEnable()
    {
        if (playercontrol == null)
        {
            playercontrol = new PlayerControl();
            playercontrol.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>()  ;
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
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animatorManager.UpdateAnimatorValues(0, moveAmount); 
    }
}
