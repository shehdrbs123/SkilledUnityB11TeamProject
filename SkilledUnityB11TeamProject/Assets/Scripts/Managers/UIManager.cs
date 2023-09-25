using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    private HashSet<GameObject> _uiCounter;

    private List<InputAction> _inputs;
    private void Awake()
    {
        _uiCounter = new HashSet<GameObject>();
    }

    public void EnablePanel(GameObject o)
    {
        _uiCounter.Add(o);
        CheckInputAction();
    }

    public void DisablePanel(GameObject o)
    {
        _uiCounter.Remove(o);
        CheckInputAction();
    }

    private void CheckInputAction()
    {
        if (_inputs == null)
        {
            _inputs = new List<InputAction>(10);
            GameObject player = GameManager.Instance.GetPlayer();
            if (player)
            {
                PlayerInput playerInput = player.GetComponent<PlayerInput>();
                _inputs.Add(playerInput.actions.FindAction("Move"));
                _inputs.Add(playerInput.actions.FindAction("Look"));
                _inputs.Add(playerInput.actions.FindAction("Fire"));
            }
        }

        IgnoreInput(_uiCounter.Count>0);
    }

    private void IgnoreInput(bool ignore)
    {
        foreach (var input in _inputs)
        {
            if (ignore)
            {
                input.Disable();
            }
            else
            {
                input.Enable();
            }
        }
    }
}
