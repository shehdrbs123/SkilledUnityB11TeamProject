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
    [SerializeField] private Transform _cameras;
    [SerializeField] private LayerMask groundLayerMask;
    private InputController _controller;
    private Rigidbody _rigid;

    public float camCurXRot;
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
        //상하 회전
        camCurXRot += mouseDelta.y * LookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, xRotMin, xRotMax);
        _cameras.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        //좌우 회전
        transform.eulerAngles += new Vector3(0, mouseDelta.x * LookSensitivity, 0);
        //mouseDelta = Vector3.zero;
    }



    public bool isCanLook()
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
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * .2f) + (Vector3.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * .2f)+(Vector3.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * .2f)+ (Vector3.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * .2f)+ (Vector3.up * 0.01f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; ++i)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }
        return false;
    }
}
