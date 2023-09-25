using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(Rigidbody),typeof(InputController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]private float speed;
    [SerializeField]private float JumpForce;
    [Header("회전 옵션")]
    [SerializeField][Range(0,1)]private float LookSensitivity;

    [SerializeField] private float xRotMax;
    [SerializeField] private float xRotMin;
    [SerializeField]private Transform _cameras;
    
    private InputController _controller;
    private Rigidbody _rigid;


    private Vector3 _curdirection;
    private Vector2 mouseDelta;
    private void Awake()
    {
        _controller = GetComponent<InputController>();
        _rigid = GetComponent<Rigidbody>();
        Camera cam = Camera.main;
        
        cam.transform.SetParent(_cameras,false);
        cam.transform.localPosition = Vector3.zero;
       
    }

    private void FixedUpdate()
    {
        if (_curdirection != Vector3.zero || _rigid.velocity != Vector3.zero)
        {
            Move();
        }
    }

    private void LateUpdate()
    {
        if (isCanLook())
        {
            LookRotation();
        }
    }

    private void LookRotation()
    {
        float yRot = mouseDelta.y * LookSensitivity;
        yRot = Math.Clamp(yRot, xRotMin, xRotMax);
        _cameras.localEulerAngles += new Vector3(-yRot,0,0);

        float xRot = mouseDelta.x * LookSensitivity;
        transform.eulerAngles += new Vector3(0, xRot, 0);
        mouseDelta = Vector3.zero;
    }

    private bool isCanLook()
    {
        return true;
    }

    private void Start()
    {
        _controller.OnMoved += OnMoveInput;
        _controller.OnJump += OnJumpInput;
        _controller.OnLookRotation += OnLookInput;
    }

    private void Move()
    {
        Vector3 dir = transform.forward * _curdirection.y + transform.right * _curdirection.x;
        dir *= speed;
        dir.y = _rigid.velocity.y;

        _rigid.velocity = dir;
    }

    private void OnMoveInput(Vector2 direction)
    {
        _curdirection = direction;
    }

    private void OnJumpInput()
    {
        if(isGrounded())
            _rigid.AddForce(Vector3.up * JumpForce,ForceMode.Impulse);
    }

    private void OnLookInput(Vector2 mouseDelta)
    {
        this.mouseDelta = mouseDelta;
    }

    private bool isGrounded()
    {
        return true;
    }
}
