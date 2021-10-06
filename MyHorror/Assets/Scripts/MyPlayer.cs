using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayer : MonoBehaviour
{
    public bool Grounded = true;
    
    // cinemachine
    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;

    private Animator _anim;
    private Rigidbody _rb;
    private GameObject _mainCamera;

    private bool _isJump;
    private bool _isForce;
    private Vector3 _direction;

    private void Awake()
    {
        // get a reference to our main camera
        if (_mainCamera == null)
        {
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _direction.x = Input.GetAxis("Horizontal");
        _direction.z = Input.GetAxis("Vertical");

        _isForce = Input.GetButton("Force");
        _isJump = Input.GetButton("Jump");
    }

    private void Move()
    {

    }

}
