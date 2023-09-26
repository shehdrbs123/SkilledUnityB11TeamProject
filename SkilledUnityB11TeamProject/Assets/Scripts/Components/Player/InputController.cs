using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    public event Action<Vector2> OnMoved;
    public event Action<Vector2> OnLookRotation;
    public event Action OnFire1;
    public event Action OnFire2;
    public event Action OnJump;

    private void Awake()
    {
        if (TryGetComponent(out PlayerInput playerInput))
        {
            InputAction action = playerInput.actions.FindAction("Move");
            action.performed += CallOnMove;
            action.canceled += CallOnMove;
            action = playerInput.actions.FindAction("Look");
            action.performed += CallOnLookRotation;
            action = playerInput.actions.FindAction("Fire1");
            action.started += CallOnFire1;
            action = playerInput.actions.FindAction("Fire2");
            action.canceled += CallOnFire2;    
            action = playerInput.actions.FindAction("Jump");
            action.started += CallOnJump;
        }
    }
    
    public void CallOnMove(InputAction.CallbackContext callbackContext)
    {
        Vector2 direction = callbackContext.ReadValue<Vector2>();
        direction.Normalize();
        OnMoved?.Invoke(direction);
    }

    public void CallOnLookRotation(InputAction.CallbackContext callbackContext)
    {
        Vector2 mouseDelta = callbackContext.ReadValue<Vector2>();
        OnLookRotation?.Invoke(mouseDelta);
    }

    public void CallOnFire1(InputAction.CallbackContext callbackContext)
    {
        OnFire1?.Invoke();    
    }

    public void CallOnFire2(InputAction.CallbackContext callbackContext)
    {
        OnFire2?.Invoke();
    }

    public void CallOnJump(InputAction.CallbackContext callbackContext)
    {
        OnJump?.Invoke();
    }
}
