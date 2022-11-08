using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerControl playercontrol;

    [SerializeField] private Vector2 movementInput;
        
    public float verticalInput;
    public float horizontalInput;
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
    }
}
