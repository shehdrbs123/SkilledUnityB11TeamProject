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
    [SerializeField] private AudioClip[] footStep;
    private InputController _controller;
    private Rigidbody _rigid;
    private Animator _animator;

    public float camCurXRot;
    private Vector3 _curdirection;
    private Vector2 mouseDelta;
    private int isMoveHash;
    private int isJumpHash;

    private bool isJump;
        
    public int AnimatorDump;
    private void Awake()
    {
        _controller = GetComponent<InputController>();
        _rigid = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        Camera cam = Camera.main;
        
        cam.transform.SetParent(_cameras,false);
        cam.transform.localPosition = Vector3.zero;

        isJumpHash = Animator.StringToHash("IsJump");
        isMoveHash = Animator.StringToHash("IsWalk");
    }

    private void FixedUpdate()
    {
        
        if (_curdirection != Vector3.zero || _rigid.velocity != Vector3.zero)
        {
            Move();
        }

        if (isJump && isGrounded())
        {
            isJump = false;
            _animator.SetBool(isJumpHash,false);
        }
    }

    private void SetJump()
    {
        isJump = true;
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
        UIManager.PopupText("아아아아아아");
    }

    private void Move()
    {
        Vector3 dir = transform.forward * _curdirection.y + transform.right * _curdirection.x;
        dir *= speed;
        dir.y = _rigid.velocity.y;

        _rigid.velocity = dir;
        Vector3 planeSpeed = new Vector3(_rigid.velocity.x, 0, _rigid.velocity.z);
        _animator.SetBool(isMoveHash,planeSpeed.sqrMagnitude > 1.0f);
    }

    private void OnMoveInput(Vector2 direction)
    {
        _curdirection = direction;
    }

    private void OnJumpInput()
    {
        if (isGrounded())
        {
            _rigid.AddForce(Vector3.up * JumpForce,ForceMode.Impulse);
            Invoke("SetJump",0.1f);
            _animator.SetBool(isJumpHash,true);
        }
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

    public void PlayFootStep()
    {
        SoundManager.PlayRandomClip(footStep,transform.position);
    }

    public AudioClip[] GetFootStepSounds()
    {
        return footStep;
    }

    public void ChangeFootstepSound(AudioClip[] change)
    {
        footStep = change;
    }
}
