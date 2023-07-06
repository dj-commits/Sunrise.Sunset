using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Editor vars
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpHeight;
    [SerializeField] float groundRaycastLength;
    [SerializeField] Vector3 groundRaycastOffsetVamp;
    [SerializeField] Vector3 groundRaycastOffsetBat;
    // Input
    private PlayerInputActions playerInputActions;
    private InputAction move;
    private InputAction jump;
    private List<InputAction> actions;

    // Components
    private BoxCollider2D boxCollider;
    private SpriteRenderer childSpriteRenderer;

    // Private vars
    private const float GRAVITY = 9.82f;
    private Vector3 movement;

    private bool isBat;
    private bool isFalling;
    private bool canDblJump;

    private void Start()
    {
        playerInputActions = new PlayerInputActions();
        childSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        move = playerInputActions.Player.Move;
        jump = playerInputActions.Player.Jump;
        actions = new List<InputAction>
        {
            move,
            jump
        };
        InputPauseOnGameStart(1000);
    }

    async void InputPauseOnGameStart(int msToPause)
    {
        await Task.Delay(msToPause);
        foreach (InputAction action in actions)
        {
            action.Enable();
        }
    }

    private void Update()
    {
        HandleLogic();
        HandleMovement();
    }

    private void HandleLogic()
    {
        isFalling = !CheckForGroundCollision();
    }

    private bool CheckForGroundCollision()
    {
        // returnable bool checking to see if we are hitting ground
        bool isHit = false;

        // if is vampire
        Vector2 raycastOriginOffset = transform.position - groundRaycastOffsetVamp;
        RaycastHit2D hit = Physics2D.Raycast(raycastOriginOffset, Vector2.down, groundRaycastLength);
        if (hit)
        {
            if (hit.collider.CompareTag("Ground"))
            {
                isHit = true;
            }
        }
        //Debug.DrawRay(raycastOriginOffset, Vector2.down * groundRaycastLength);
        return isHit;
       
    }

    private void HandleMovement()
    {
        // get initial movement variable
        movement = move.ReadValue<Vector2>();
        
        if (isFalling)
        {
            // need to disable W + D bindings if falling & vampire
            movement.y -= GRAVITY * Time.deltaTime;
        }
        else
        {
            // essentially "isGrounded" because "!isFalling"
            movement.y = 0;
        }

        if (jump.IsPressed())
        {
            // Change to Event?
            movement.y += jumpHeight;
        }

        // move it
        transform.Translate(movement * moveSpeed * Time.deltaTime);

       
       
    }


}
